using ExamonimyWeb.DTOs.ExamPaperDTO;
using ExamonimyWeb.DTOs.NotificationDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Enums;
using ExamonimyWeb.Hubs.ExamPaperTimelineHub;
using ExamonimyWeb.Hubs.NotificationHub;
using ExamonimyWeb.Managers.ExamManager;
using ExamonimyWeb.Managers.ExamPaperManager;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Repositories;
using ExamonimyWeb.Utilities;
using ExamonimyWeb.Utilities.NotificationMessages;
using Microsoft.AspNetCore.SignalR;
using System.Runtime.CompilerServices;

namespace ExamonimyWeb.Services.NotificationService;

public class InAppNotificationService : INotificationService
{
    private readonly IExamPaperManager _examPaperManager;
    private readonly IGenericRepository<Notification> _notificationRepository;
    private readonly IGenericRepository<NotificationReceiver> _notificationReceiverRepository;      
    private readonly IHubContext<NotificationHub, INotificationClient> _notificationHubContext;
    private readonly IUserManager _userManager;
    private readonly IHubContext<ExamPaperTimelineHub, IExamPaperTimelineClient> _examPaperTimelineHubContext;
    private readonly IGenericRepository<Student> _studentRepository;
    private readonly Managers.ExamManager.IExamManager _examManager;

    public InAppNotificationService(IExamPaperManager examPaperManager, IGenericRepository<Notification> notificationRepository, IGenericRepository<NotificationReceiver> notificationReceiverRepository, IHubContext<NotificationHub, INotificationClient> notificationHubContext, IUserManager userManager, IHubContext<ExamPaperTimelineHub, IExamPaperTimelineClient> examPaperTimelineHubContext, IGenericRepository<Student> studentRepository, IExamManager examManager)
    {
        _examPaperManager = examPaperManager;
        _notificationRepository = notificationRepository;
        _notificationReceiverRepository = notificationReceiverRepository;          
        _notificationHubContext = notificationHubContext;
        _userManager = userManager;
        _examPaperTimelineHubContext = examPaperTimelineHubContext;
        _studentRepository = studentRepository;
        _examManager = examManager;
    }

    public async Task DeleteNotificationsAsync(int entityId, List<Operation> operations)
    {
        var notificationsToDelete = (await _notificationRepository.GetRangeAsync(n => n.EntityId == entityId && operations.Contains(n.Operation))).ToList();
        var notificationsIdsToDelete = notificationsToDelete.Select(n => n.Id);
        _notificationReceiverRepository.DeleteRange(nR => notificationsIdsToDelete.Contains(nR.NotificationId));
        await _notificationReceiverRepository.SaveAsync();
        _notificationRepository.DeleteRange(notificationsToDelete);
        await _notificationRepository.SaveAsync();
    }

    private string GetHref(Notification notification)
    {
        switch (notification.Operation)
        {
            case Operation.AskForReviewForExamPaper:
            case Operation.CommentExamPaper:
            case Operation.EditExamPaper:
            case Operation.ApproveExamPaper:
            case Operation.RejectExamPaper:
                var examPaperId = notification.EntityId;
                return $"/exam-paper/{examPaperId}/review";
            case Operation.UpcomingExam:
            case Operation.ChangeExamSchedule:
                return $"/exam";
            default:
                throw new SwitchExpressionException(notification.Operation);
        }
    }

    private async Task<string> GetMessageMarkupAsync(Notification notification, bool isRead)
    {
        var actor = await _userManager.GetByIdAsync(notification.ActorId) ?? throw new ArgumentException(null, nameof(notification.ActorId));
        var actorFullName = actor.FullName;
        switch (notification.Operation)
        {
            case Operation.AskForReviewForExamPaper:
                var examPaperId = notification.EntityId;
                var course = await _examPaperManager.GetCourseAsync(examPaperId) ?? throw new ArgumentException(null, nameof(notification.EntityId));      
                return new AskForReviewForExamPaperNotiMessage(actorFullName, course.Name, isRead).ToVietnamese();
            case Operation.CommentExamPaper:
                var examPaper = await _examPaperManager.GetByIdAsync(notification.EntityId) ?? throw new ArgumentException(null, nameof(notification.EntityId));
                return new CommentExamPaperNotiMessage(actorFullName, examPaper.ExamPaperCode, isRead).ToVietnamese();
            case Operation.EditExamPaper:
                examPaper = await _examPaperManager.GetByIdAsync(notification.EntityId) ?? throw new ArgumentException(null, nameof(notification.EntityId));
                return new EditExamPaperNotiMessage(actorFullName, isRead, examPaper.ExamPaperCode).ToVietnamese();
            case Operation.ApproveExamPaper:
                examPaper = await _examPaperManager.GetByIdAsync(notification.EntityId) ?? throw new ArgumentException(null, nameof(notification.EntityId));
                return new ApproveExamPaperNotiMessage(actorFullName, examPaper.ExamPaperCode, isRead).ToVietnamese();
            case Operation.RejectExamPaper:
                examPaper = await _examPaperManager.GetByIdAsync(notification.EntityId) ?? throw new ArgumentException(null, nameof(notification.EntityId));
                return new RejectExamPaperNotiMessage(actorFullName, examPaper.ExamPaperCode, isRead).ToVietnamese();
            case Operation.UpcomingExam:
                var courseName = await _examManager.GetCourseName(notification.EntityId);
                return new UpcomingExamNotiMessage(actorFullName, isRead, courseName).ToVietnamese();
            case Operation.ChangeExamSchedule:
                courseName = await _examManager.GetCourseName(notification.EntityId);
                return new ChangedExamScheduleNotiMessage(actorFullName, isRead, courseName).ToVietnamese();
            default:
                throw new SwitchExpressionException(notification.Operation);  
        }
    }

    public async Task<IEnumerable<NotificationGetDto>> GetNotificationsAsync(int receiverId, RequestParams requestParams)
    {
        var notificationReceivers = await _notificationReceiverRepository.GetPagedListAsync(requestParams, nR => nR.ReceiverId == receiverId, new List<string> { "Notification", "Notification.Actor" }, q => q.OrderByDescending(nR => nR.Notification!.CreatedAt));     
        var notificationsToReturn = new List<NotificationGetDto>();
        if (notificationReceivers.Any())
        {
            foreach (var notificationReceiver in notificationReceivers)
            {
                notificationsToReturn.Add(new NotificationGetDto
                {
                    Id = notificationReceiver.NotificationId,
                    MessageMarkup = await GetMessageMarkupAsync(notificationReceiver.Notification!, notificationReceiver.IsRead),
                    ActorProfilePicture = notificationReceiver.Notification!.Actor!.ProfilePicture,
                    Href = GetHref(notificationReceiver.Notification!),                  
                    NotifiedAt = notificationReceiver.Notification.CreatedAt,
                    IsRead = notificationReceiver.IsRead,
                    Operation = (int)notificationReceiver.Notification.Operation
                });
            }
        }
        return notificationsToReturn;
    }

    public async Task NotifyUponAddingReviewersAsync(int examPaperId, List<ExamPaperReviewer> examPaperReviewers, int actorId)
    {
      
        var notification = await _notificationRepository.GetAsync(n => n.Operation == Operation.AskForReviewForExamPaper && n.EntityId == examPaperId, null);
        // if this noti does not exist, we persist it and create its receivers
        if (notification is null)
        {
            var notificationToCreate = new Notification
            {
                Operation = Operation.AskForReviewForExamPaper,
                EntityId = examPaperId,
                ActorId = actorId
            };

            await _notificationRepository.InsertAsync(notificationToCreate);
            await _notificationRepository.SaveAsync();

            var notificationReceiversToCreate = examPaperReviewers.Select(ePR => new NotificationReceiver
            {
                ReceiverId = ePR.ReviewerId,
                NotificationId = notificationToCreate.Id
            }).ToList();

            await _notificationReceiverRepository.InsertRangeAsync(notificationReceiversToCreate);
            await _notificationReceiverRepository.SaveAsync();

            // Send SignalR noti
            var ids = notificationReceiversToCreate.Select(nR => nR.ReceiverId);
            var usernames = (await _userManager.GetRangeAsync(u => ids.Contains(u.Id))).Select(u => u.Username).ToList();
            var actor = await _userManager.GetByIdAsync(actorId) ?? throw new ArgumentException(null, nameof(actorId));
            await _notificationHubContext.Clients.Users(usernames).ReceiveNotification(new NotificationGetDto
            {
                Id = notificationToCreate.Id,
                MessageMarkup = await GetMessageMarkupAsync(notificationToCreate, false),
                ActorProfilePicture = actor.ProfilePicture,
                Href = GetHref(notificationToCreate),              
                NotifiedAt = notificationToCreate.CreatedAt,
                IsRead = false,
                Operation = (int)notificationToCreate.Operation
            });

            return;
        }

        // else, we just need to update its receivers
        var existingReceivers = await _notificationReceiverRepository.GetRangeAsync(nR => nR.NotificationId == notification.Id);
        var existingReceiverIds = existingReceivers.Select(e => e.ReceiverId);
        var receiverIds = examPaperReviewers.Select(e => e.ReviewerId);
        var receiversToAdd = receiverIds
            .Where(id => !existingReceiverIds.Contains(id))
            .Select(receiverId => new NotificationReceiver { NotificationId = notification.Id, ReceiverId = receiverId })
            .ToList();
        var receiversToDelete = existingReceivers
            .Where(r => !receiverIds.Contains(r.ReceiverId))
            .ToList();
        _notificationReceiverRepository.DeleteRange(receiversToDelete);
        await _notificationReceiverRepository.InsertRangeAsync(receiversToAdd);
        await _notificationReceiverRepository.SaveAsync();          
    }

    public async Task NotifyAboutExamPaperCommentAsync(int examPaperId, int commenterId, int examPaperAuthorId, string comment)
    {
        var author = await _userManager.GetByIdAsync(examPaperAuthorId) ?? throw new ArgumentException(null, nameof(examPaperAuthorId));
        var authorUsername = author.Username;
        var actor = await _userManager.GetByIdAsync(commenterId) ?? throw new ArgumentException(null, nameof(commenterId));
        var createdAt = DateTime.UtcNow;

        if (commenterId != examPaperAuthorId)
        {
            // Create the notification for the author
            var notificationToCreate = new Notification
            {
                Operation = Operation.CommentExamPaper,
                EntityId = examPaperId,
                ActorId = commenterId
            };
            await _notificationRepository.InsertAsync(notificationToCreate);
            await _notificationRepository.SaveAsync();

            // Create the notificationReceiver for the author
            var notificationReceiverToCreate = new NotificationReceiver
            {
                NotificationId = notificationToCreate.Id,
                ReceiverId = examPaperAuthorId
            };
            await _notificationReceiverRepository.InsertAsync(notificationReceiverToCreate);
            await _notificationReceiverRepository.SaveAsync();

            // Send SignalR noti to author               
            await _notificationHubContext.Clients.User(authorUsername).ReceiveNotification(new NotificationGetDto
            {
                Id = notificationToCreate.Id,
                MessageMarkup = await GetMessageMarkupAsync(notificationToCreate, false),
                ActorProfilePicture = actor.ProfilePicture,
                Href = GetHref(notificationToCreate),               
                NotifiedAt = notificationToCreate.CreatedAt,
                IsRead = false,
                Operation = (int)notificationToCreate.Operation
            });

            createdAt = notificationToCreate.CreatedAt;
        }
        

        // Send the comment to author and reviewers via SignalR
        var usernames = (await _examPaperManager.GetReviewersAsync(examPaperId)).Select(r => r.Username).ToList();
        usernames.Add(authorUsername);
        await _examPaperTimelineHubContext.Clients.Users(usernames).ReceiveComment(new ExamPaperReviewHistoryCommentGetDto
        {
            OperationId = (int)Operation.CommentExamPaper,
            ActorName = actor.FullName,
            CreatedAt = createdAt,
            Comment = comment,
            IsAuthor = await _examPaperManager.IsAuthorAsync(examPaperId, commenterId),
            ActorProfilePicture = actor.ProfilePicture
        });

    }

    public async Task NotifyAboutEditedExamPaperAsync(int examPaperId, string commitMessage)
    {
        // Insert into database
        var examPaper = await _examPaperManager.GetByIdAsync(examPaperId) ?? throw new ArgumentException(null, nameof(examPaperId));
        var notificationToCreate = new Notification
        {
            EntityId = examPaperId,
            ActorId = examPaper.AuthorId,
            Operation = Operation.EditExamPaper
        };
        await _notificationRepository.InsertAsync(notificationToCreate);
        await _notificationRepository.SaveAsync();

        var reviewers = await _examPaperManager.GetReviewersAsync(examPaperId);
        var receiversToCreate = reviewers.Select(r => new NotificationReceiver
        {
            ReceiverId = r.Id,
            NotificationId = notificationToCreate.Id
        }).ToList();
        await _notificationReceiverRepository.InsertRangeAsync(receiversToCreate);
        await _notificationReceiverRepository.SaveAsync();


        // Send noti to the reviewers
        var actor = await _examPaperManager.GetAuthorAsync(examPaperId);
        var reviewerUsernames = reviewers.Select(r => r.Username).ToList();
        await _notificationHubContext.Clients.Users(reviewerUsernames).ReceiveNotification(new NotificationGetDto
        {
            MessageMarkup = await GetMessageMarkupAsync(notificationToCreate, false),           
            ActorProfilePicture = actor.ProfilePicture,
            Href = GetHref(notificationToCreate),
            NotifiedAt = notificationToCreate.CreatedAt,
            IsRead = false,
            Operation = (int)notificationToCreate.Operation
        });

        var usernames = new List<string>(reviewerUsernames.Count + 1);
        usernames.AddRange(reviewerUsernames);
        usernames.Add(actor.Username);
        // Insert timeline into client DOM
        await _examPaperTimelineHubContext.Clients.Users(usernames).ReceiveEdit(new ExamPaperReviewHistoryEditGetDto
        {
            ActorName = actor.FullName,
            CreatedAt = notificationToCreate.CreatedAt,
            OperationId = (int)Operation.EditExamPaper,
            CommitMessage = commitMessage
        });
    }

    public async Task NotifyAboutApprovedExamPaperAsync(int examPaperId, int reviewerId)
    {
        // insert notification and notificationReceiver into db
        var notificationToCreate = new Notification
        {
            EntityId = examPaperId,
            ActorId = reviewerId,
            Operation = Operation.ApproveExamPaper
        };
        await _notificationRepository.InsertAsync(notificationToCreate);
        await _notificationRepository.SaveAsync();

        var author = await _examPaperManager.GetAuthorAsync(examPaperId);
        var notificationReceiverToCreate = new NotificationReceiver
        {
            NotificationId = notificationToCreate.Id,
            ReceiverId = author.Id,
        };
        await _notificationReceiverRepository.InsertAsync(notificationReceiverToCreate);
        await _notificationReceiverRepository.SaveAsync();

        // send noti to exam paper's author
        var reviewer = await _userManager.GetByIdAsync(reviewerId) ?? throw new ArgumentException(null, nameof(reviewerId));
        await _notificationHubContext.Clients.User(author.Username).ReceiveNotification(new NotificationGetDto
        {
            MessageMarkup = await GetMessageMarkupAsync(notificationToCreate, false),          
            ActorProfilePicture = reviewer.ProfilePicture,
            Href = GetHref(notificationToCreate),
            NotifiedAt = notificationToCreate.CreatedAt,
            IsRead = false,
            Operation = (int)notificationToCreate.Operation
        });

        // insert timeline into client DOM
        var usernames = (await _examPaperManager.GetReviewersAsync(examPaperId)).Select(r => r.Username).ToList();
        usernames.Add(author.Username);
        await _examPaperTimelineHubContext.Clients.Users(usernames).ReceiveApprove(new ExamPaperReviewHistoryGetDto
        {
            ActorName = reviewer.FullName,
            CreatedAt = notificationToCreate.CreatedAt,
            OperationId = (int)notificationToCreate.Operation
        });
    }

    public async Task NotifyAboutRejectedExamPaperAsync(int examPaperId, int reviewerId)
    {
        // insert notification and notificationReceiver into db
        var notificationToCreate = new Notification
        {
            EntityId = examPaperId,
            ActorId = reviewerId,
            Operation = Operation.RejectExamPaper
        };
        await _notificationRepository.InsertAsync(notificationToCreate);
        await _notificationRepository.SaveAsync();

        var author = await _examPaperManager.GetAuthorAsync(examPaperId);
        var notificationReceiverToCreate = new NotificationReceiver
        {
            NotificationId = notificationToCreate.Id,
            ReceiverId = author.Id
        };
        await _notificationReceiverRepository.InsertAsync(notificationReceiverToCreate);
        await _notificationReceiverRepository.SaveAsync();

        // send noti to exam paper's author
        var reviewer = await _userManager.GetByIdAsync(reviewerId) ?? throw new ArgumentException(null, nameof(reviewerId));
        await _notificationHubContext.Clients.User(author.Username).ReceiveNotification(new NotificationGetDto
        {
            MessageMarkup = await GetMessageMarkupAsync(notificationToCreate, false),          
            ActorProfilePicture = reviewer.ProfilePicture,
            Href = GetHref(notificationToCreate),
            NotifiedAt = notificationToCreate.CreatedAt,
            IsRead = false,
            Operation = (int)notificationToCreate.Operation
        });

        // insert timeline into client DOM
        var usernames = (await _examPaperManager.GetReviewersAsync(examPaperId)).Select(r => r.Username).ToList();
        usernames.Add(author.Username);
        await _examPaperTimelineHubContext.Clients.Users(usernames).ReceiveReject(new ExamPaperReviewHistoryGetDto
        {
            ActorName = reviewer.FullName,
            CreatedAt = notificationToCreate.CreatedAt,
            OperationId = (int)notificationToCreate.Operation
        });
    }

    public async Task NotifyAboutUpcomingExamAsync(int teacherId, int examId, List<int> mainClassIds)
    {      
       // create the notification
       var notificationToCreate = new Notification
       {
           EntityId = examId,
           ActorId = teacherId,
           Operation = Operation.UpcomingExam
       };
        await _notificationRepository.InsertAsync(notificationToCreate);
        await _notificationRepository.SaveAsync();


        var (notificationReceivers, studentUsernames) = await ConstructReceiversAndStudentUsernames(notificationToCreate.Id, mainClassIds);

        // create the notificationReceiver      
        await _notificationReceiverRepository.InsertRangeAsync(notificationReceivers);
        await _notificationReceiverRepository.SaveAsync();


        // send signalR noti to students
        await _notificationHubContext.Clients.Users(studentUsernames).ReceiveNotification(new NotificationGetDto
        {
            MessageMarkup = await GetMessageMarkupAsync(notificationToCreate, false),           
            ActorProfilePicture = null,
            Href = GetHref(notificationToCreate),
            NotifiedAt =  notificationToCreate.CreatedAt,
            IsRead = false,
            Operation = (int)notificationToCreate.Operation
        });

    }

    private async Task<(List<NotificationReceiver> notificationReceivers, List<string> studentUsernames)> ConstructReceiversAndStudentUsernames(int notificationId, List<int> mainClassIds)
    {
        var notificationReceivers = new List<NotificationReceiver>();
        var studentUsernames = new List<string>();
        foreach (var mainClassId in mainClassIds)
        {
            var students = await _studentRepository.GetRangeAsync(s => s.MainClassId == mainClassId, new List<string> { "User" });
            var receivers = students.Select(s => new NotificationReceiver
            {
                ReceiverId = s.UserId,
                NotificationId = notificationId
            });
            notificationReceivers.AddRange(receivers);
            var usernames = students.Select(s => s.User!.Username);
            studentUsernames.AddRange(usernames);
        }
        return (notificationReceivers, studentUsernames);
    }

    public async Task NotifyAboutChangedExamSchedule(int examId, int actorId)
    {
        // create the notification
        var notification = new Notification
        {
            EntityId = examId,
            ActorId = actorId,
            Operation = Operation.ChangeExamSchedule
        };
        await _notificationRepository.InsertAsync(notification);
        await _notificationRepository.SaveAsync();

        var mainClassIds = (await _examManager.GetMainClassesByExam(examId)).Select(mc => mc.Id).ToList();

        var (notificationReceivers, studentUsernames) = await ConstructReceiversAndStudentUsernames(notification.Id, mainClassIds);

        // create the notificationReceivers
        await _notificationReceiverRepository.InsertRangeAsync(notificationReceivers);
        await _notificationReceiverRepository.SaveAsync();


        // send signalR noti to students
        await _notificationHubContext.Clients.Users(studentUsernames).ReceiveNotification(new NotificationGetDto
        {
            MessageMarkup = await GetMessageMarkupAsync(notification, false),
            Href = GetHref(notification),
            NotifiedAt = notification.CreatedAt,
            IsRead = false,
            Operation = (int)notification.Operation
        });
    }
}
