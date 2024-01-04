using ExamonimyWeb.DTOs.ExamPaperDTO;
using ExamonimyWeb.DTOs.NotificationDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Enums;
using ExamonimyWeb.Hubs.ExamPaperTimelineHub;
using ExamonimyWeb.Hubs.NotificationHub;
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

    public InAppNotificationService(IExamPaperManager examPaperManager, IGenericRepository<Notification> notificationRepository, IGenericRepository<NotificationReceiver> notificationReceiverRepository, IHubContext<NotificationHub, INotificationClient> notificationHubContext, IUserManager userManager, IHubContext<ExamPaperTimelineHub, IExamPaperTimelineClient> examPaperTimelineHubContext, IGenericRepository<Student> studentRepository)
    {
        _examPaperManager = examPaperManager;
        _notificationRepository = notificationRepository;
        _notificationReceiverRepository = notificationReceiverRepository;          
        _notificationHubContext = notificationHubContext;
        _userManager = userManager;
        _examPaperTimelineHubContext = examPaperTimelineHubContext;
        _studentRepository = studentRepository;
    }

    public async Task DeleteThenSaveAsync(int entityId, List<Operation> operations)
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
                return $"/exam";
            default:
                throw new SwitchExpressionException(notification.Operation);
        }
    }

    private string GetIconMarkup(Operation operation)
    {
        return operation switch
        {
            Operation.AskForReviewForExamPaper => @"<div class=""absolute flex items-center justify-center w-5 h-5 ms-6 -mt-5 bg-yellow-600 border border-white rounded-full""><svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 24 24"" fill=""currentColor"" class=""w-2 h-2 text-white""><path d=""M5.625 1.5c-1.036 0-1.875.84-1.875 1.875v17.25c0 1.035.84 1.875 1.875 1.875h12.75c1.035 0 1.875-.84 1.875-1.875V12.75A3.75 3.75 0 0 0 16.5 9h-1.875a1.875 1.875 0 0 1-1.875-1.875V5.25A3.75 3.75 0 0 0 9 1.5H5.625Z"" /><path d=""M12.971 1.816A5.23 5.23 0 0 1 14.25 5.25v1.875c0 .207.168.375.375.375H16.5a5.23 5.23 0 0 1 3.434 1.279 9.768 9.768 0 0 0-6.963-6.963Z"" /></svg></div>",

            Operation.CommentExamPaper => @"<div class=""absolute flex items-center justify-center w-5 h-5 ms-6 -mt-5 bg-green-500 border border-white rounded-full""><svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 20 20"" fill=""currentColor"" class=""w-2 h-2 text-white""><path fill-rule=""evenodd"" d=""M3.43 2.524A41.29 41.29 0 0 1 10 2c2.236 0 4.43.18 6.57.524 1.437.231 2.43 1.49 2.43 2.902v5.148c0 1.413-.993 2.67-2.43 2.902a41.102 41.102 0 0 1-3.55.414c-.28.02-.521.18-.643.413l-1.712 3.293a.75.75 0 0 1-1.33 0l-1.713-3.293a.783.783 0 0 0-.642-.413 41.108 41.108 0 0 1-3.55-.414C1.993 13.245 1 11.986 1 10.574V5.426c0-1.413.993-2.67 2.43-2.902Z"" clip-rule=""evenodd"" /></svg></div>",

            Operation.EditExamPaper => @"<div class=""absolute flex items-center justify-center w-5 h-5 ms-6 -mt-5 bg-blue-500 border border-white rounded-full""><svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 20 20"" fill=""currentColor"" class=""w-2 h-2 text-white""><path d=""m5.433 13.917 1.262-3.155A4 4 0 0 1 7.58 9.42l6.92-6.918a2.121 2.121 0 0 1 3 3l-6.92 6.918c-.383.383-.84.685-1.343.886l-3.154 1.262a.5.5 0 0 1-.65-.65Z"" /><path d=""M3.5 5.75c0-.69.56-1.25 1.25-1.25H10A.75.75 0 0 0 10 3H4.75A2.75 2.75 0 0 0 2 5.75v9.5A2.75 2.75 0 0 0 4.75 18h9.5A2.75 2.75 0 0 0 17 15.25V10a.75.75 0 0 0-1.5 0v5.25c0 .69-.56 1.25-1.25 1.25h-9.5c-.69 0-1.25-.56-1.25-1.25v-9.5Z"" /></svg></div>",

            Operation.ApproveExamPaper => @"<div class=""absolute flex items-center justify-center w-5 h-5 ms-6 -mt-5 bg-purple-500 border border-white rounded-full""><svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 20 20"" fill=""currentColor"" class=""w-2 h-2 text-white""><path fill-rule=""evenodd"" d=""M10 18a8 8 0 1 0 0-16 8 8 0 0 0 0 16Zm3.857-9.809a.75.75 0 0 0-1.214-.882l-3.483 4.79-1.88-1.88a.75.75 0 1 0-1.06 1.061l2.5 2.5a.75.75 0 0 0 1.137-.089l4-5.5Z"" clip-rule=""evenodd"" /></svg></div>",

            Operation.RejectExamPaper => @"<div class=""absolute flex items-center justify-center w-5 h-5 ms-6 -mt-5 bg-red-500 border border-white rounded-full""><svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 20 20"" fill=""currentColor"" class=""w-3 h-3 text-white""><path d=""M6.28 5.22a.75.75 0 0 0-1.06 1.06L8.94 10l-3.72 3.72a.75.75 0 1 0 1.06 1.06L10 11.06l3.72 3.72a.75.75 0 1 0 1.06-1.06L11.06 10l3.72-3.72a.75.75 0 0 0-1.06-1.06L10 8.94 6.28 5.22Z"" /></svg></div>",          

            _ => throw new SwitchExpressionException(operation)
        };
    }

    private async Task<string> GetMessageMarkupAsync(Notification notification, bool isRead, string? relatedProp = null)
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
                if (relatedProp is null) throw new ArgumentNullException("courseName");
                return new UpcomingExamNotiMessage(actorFullName, isRead, relatedProp).ToVietnamese();
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
                    IconMarkup = GetIconMarkup(notificationReceiver.Notification!.Operation),
                    NotifiedAt = notificationReceiver.Notification.CreatedAt,
                    IsRead = notificationReceiver.IsRead,
                    Operation = (int)notificationReceiver.Notification.Operation
                });
            }
        }
        return notificationsToReturn;
    }

    public async Task RequestReviewerForExamPaperAsync(int examPaperId, List<ExamPaperReviewer> examPaperReviewers, int actorId)
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
                IconMarkup = GetIconMarkup(notificationToCreate.Operation),
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

    public async Task CommentOnExamPaperReviewAsync(int examPaperId, int commenterId, int examPaperAuthorId, string comment)
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
                IconMarkup = GetIconMarkup(notificationToCreate.Operation),
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

    public async Task EditExamPaperAsync(int examPaperId, string commitMessage)
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
            IconMarkup = GetIconMarkup(notificationToCreate.Operation),
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

    public async Task ApproveExamPaperReviewAsync(int examPaperId, int reviewerId)
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
            IconMarkup = GetIconMarkup(Operation.ApproveExamPaper),
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

    public async Task RejectExamPaperReviewAsync(int examPaperId, int reviewerId)
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
            IconMarkup = GetIconMarkup(notificationToCreate.Operation),
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

    public async Task NotifyAboutUpcomingExamAsync(int teacherId, int examId, List<int> mainClassIds, string courseName)
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


        // create the notificationReceiver
        var notificationReceiversToCreate = new List<NotificationReceiver>();
        var studentUsernames = new List<string>();
        foreach (var mainClassId in mainClassIds)
        {
            var students = await _studentRepository.GetRangeAsync(s => s.MainClassId == mainClassId, new List<string> { "User.Username" });
            var receivers = students.Select(s => new NotificationReceiver
            {
                ReceiverId = s.UserId,
                NotificationId = notificationToCreate.Id
            });
            notificationReceiversToCreate.AddRange(receivers);
            var usernames = students.Select(s => s.User!.Username);
            studentUsernames.AddRange(usernames);
        }
        await _notificationReceiverRepository.InsertRangeAsync(notificationReceiversToCreate);
        await _notificationReceiverRepository.SaveAsync();


        // send signalR noti to students
        await _notificationHubContext.Clients.Users(studentUsernames).ReceiveNotification(new NotificationGetDto
        {
            MessageMarkup = await GetMessageMarkupAsync(notificationToCreate, false, courseName),
            IconMarkup = null,
            ActorProfilePicture = null,
            Href = GetHref(notificationToCreate),
            NotifiedAt =  notificationToCreate.CreatedAt,
            IsRead = false,
            Operation = (int)notificationToCreate.Operation
        });

    }
}
