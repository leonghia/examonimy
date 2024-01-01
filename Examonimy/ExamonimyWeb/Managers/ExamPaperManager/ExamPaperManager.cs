using ExamonimyWeb.DTOs.ExamPaperDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Enums;
using ExamonimyWeb.Managers.QuestionManager;
using ExamonimyWeb.Repositories.GenericRepository;
using ExamonimyWeb.Utilities;
using LinqKit;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace ExamonimyWeb.Managers.ExamPaperManager
{
    public class ExamPaperManager : IExamPaperManager
    {
        private readonly IGenericRepository<ExamPaperQuestion> _examPaperQuestionRepository;      
        private readonly IQuestionManager _questionManager;
        private readonly IGenericRepository<ExamPaper> _examPaperRepository;
        private readonly IGenericRepository<ExamPaperReviewer> _examPaperReviewerRepository;
        private readonly IGenericRepository<ExamPaperComment> _examPaperCommentRepository;
        private readonly IGenericRepository<ExamPaperReviewHistory> _examPaperReviewHistoryRepository;
        private readonly IGenericRepository<ExamPaperCommit> _examPaperCommitRepository;

        public ExamPaperManager(IGenericRepository<ExamPaperQuestion> examPaperQuestionRepository, IQuestionManager questionManager, IGenericRepository<ExamPaper> examPaperRepository, IGenericRepository<ExamPaperReviewer> examPaperReviewerRepository, IGenericRepository<ExamPaperComment> examPaperCommentRepository, IGenericRepository<ExamPaperReviewHistory> examPaperReviewHistoryRepository, IGenericRepository<ExamPaperCommit> examPaperCommitRepository)
        {
            _examPaperQuestionRepository = examPaperQuestionRepository;         
            _questionManager = questionManager;
            _examPaperRepository = examPaperRepository;
            _examPaperReviewerRepository = examPaperReviewerRepository;
            _examPaperCommentRepository = examPaperCommentRepository;
            _examPaperReviewHistoryRepository = examPaperReviewHistoryRepository;
            _examPaperCommitRepository = examPaperCommitRepository;
        }

        public async Task AddReviewersAsync(int examPaperId, List<ExamPaperReviewer> examPaperReviewers)
        {
            _examPaperReviewerRepository.DeleteRange(ePR => ePR.ExamPaperId == examPaperId);
            await _examPaperReviewerRepository.InsertRangeAsync(examPaperReviewers);
            await _examPaperReviewerRepository.SaveAsync();

            var examPaper = await _examPaperRepository.GetByIdAsync(examPaperId) ?? throw new ArgumentException(null, nameof(examPaperId));
            var authorId = examPaper.AuthorId;

            // add to the review history
            _examPaperReviewHistoryRepository.DeleteRange(eprh => eprh.ExamPaperId == examPaperId && eprh.OperationId == (int)Operation.AskForReviewForExamPaper);
            var eprh = examPaperReviewers.Select(epr => new ExamPaperReviewHistory
            {
                OperationId = (int)Operation.AskForReviewForExamPaper,
                ExamPaperId = examPaperId,
                EntityId = epr.Id,
                CreatedAt = DateTime.UtcNow,
                ActorId = authorId
            }).ToList();
            await _examPaperReviewHistoryRepository.InsertRangeAsync(eprh);
            await _examPaperReviewHistoryRepository.SaveAsync();
        }

        public async Task CreateAsync(ExamPaper examPaper)
        {
            await _examPaperRepository.InsertAsync(examPaper);
            await _examPaperRepository.SaveAsync();

            // add to the review history
            await _examPaperReviewHistoryRepository.InsertAsync(new ExamPaperReviewHistory
            {
                ExamPaperId = examPaper.Id,
                ActorId = examPaper.AuthorId,
                OperationId = (int)Operation.CreateExamPaper,
                EntityId = examPaper.Id,
                CreatedAt = DateTime.UtcNow
            });
            await _examPaperReviewHistoryRepository.SaveAsync();
        }

        public async Task<int> CountNumbersOfQuestions(int examPaperId)
        {
            return await _examPaperQuestionRepository.CountAsync(examPaperQuestion => examPaperQuestion.ExamPaperId == examPaperId);
        }

        public async Task DeleteAsync(int examPaperId)
        {
            // Delete all the related examPaperQuestions
            _examPaperQuestionRepository.DeleteRange(ePQ => ePQ.ExamPaperId == examPaperId);
            await _examPaperQuestionRepository.SaveAsync();

            // Delete all the related examPaperReviewers
            _examPaperReviewerRepository.DeleteRange(ePR => ePR.ExamPaperId == examPaperId);
            await _examPaperReviewerRepository.SaveAsync();

            // Delete all the related examPaperComments
            _examPaperCommentRepository.DeleteRange(epc => epc.ExamPaperId == examPaperId);
            await _examPaperCommentRepository.SaveAsync();

            // Delete all the related examPaperReviewHistories
            _examPaperReviewHistoryRepository.DeleteRange(eprh => eprh.ExamPaperId == examPaperId);
            await _examPaperReviewHistoryRepository.SaveAsync();

            // Delete all the related examPaperCommits
            // this is already done by onCascade.delete

            // Delete the examPaper itself         
            await _examPaperRepository.DeleteAsync(examPaperId);
            await _examPaperRepository.SaveAsync();
        }

        public async Task<ExamPaper?> GetByIdAsync(int examPaperId)
        {
            return await _examPaperRepository.GetAsync(eP => eP.Id == examPaperId, new List<string> { "Author", "Course" });
        }

        public async Task<Course> GetCourseAsync(int examPaperId)
        {
            var examPaper = await _examPaperRepository.GetAsync(eP => eP.Id == examPaperId, new List<string> { "Course" }) ?? throw new ArgumentException(null, nameof(examPaperId));
            return examPaper.Course!;
        }

        public async Task<ExamPaperQuestion?> GetExamPaperQuestionAsync(int examPaperQuestionId)
        {
            return await _examPaperQuestionRepository.GetByIdAsync(examPaperQuestionId);
        }

        public async Task<IEnumerable<ExamPaperQuestionGetDto>> GetExamPaperQuestionsWithAnswersAsync(int examPaperId)
        {
            var examPaperQuestionsToReturn = new List<ExamPaperQuestionGetDto>();
            var examPaperQuestions = await _examPaperQuestionRepository.GetRangeAsync(ePQ => ePQ.ExamPaperId == examPaperId, new List<string> { "ExamPaperQuestionComments", "ExamPaperQuestionComments.Commenter" });
            foreach (var examPaperQuestion in examPaperQuestions)
            {
                var question = await _questionManager.GetSingleByIdAsync(examPaperQuestion.QuestionId) ?? throw new ArgumentException(null, nameof(examPaperId));
                var questionToReturn = await _questionManager.GetSpecificQuestionWithAnswerDtoAsync(question.Id);
                examPaperQuestionsToReturn.Add(new ExamPaperQuestionGetDto
                {
                    Id = examPaperQuestion.Id,
                    Number = examPaperQuestion.Number,
                    Question = questionToReturn                  
                });
            }           
            examPaperQuestionsToReturn.Sort((a, b) => a.Number.CompareTo(b.Number));
            return examPaperQuestionsToReturn;
        }

        public async Task<PagedList<ExamPaper>> GetPagedListAsync(RequestParamsForExamPaper requestParamsForExamPaper)
        {
            var predicate = PredicateBuilder.New<ExamPaper>(true);
            if (requestParamsForExamPaper.SearchQuery is not null)
            {
                predicate = predicate.And(eP => eP.ExamPaperCode.ToUpper().Contains(requestParamsForExamPaper.SearchQuery.ToUpper()));
            }
            
            if (requestParamsForExamPaper.CourseId is not null && requestParamsForExamPaper.CourseId > 0)
            {
                predicate = predicate.And(eP => eP.CourseId == requestParamsForExamPaper.CourseId);
            }
            if (requestParamsForExamPaper.Status is not null)
            {
                predicate = predicate.And(eP => eP.Status == requestParamsForExamPaper.Status);
            }
            return await _examPaperRepository.GetPagedListAsync(requestParamsForExamPaper, predicate, new List<string> { "Author", "Course", "Reviewers" });           
        }

        public async Task<int> GetReviewerIdAsync(int examPaperReviewerId)
        {
            var examPaperReviewer = await _examPaperReviewerRepository.GetByIdAsync(examPaperReviewerId) ?? throw new ArgumentException(null, nameof(examPaperReviewerId));
            return examPaperReviewer.ReviewerId;
        }

        public async Task<List<ExamPaperReviewHistoryGetDto>> GetReviewHistories(int examPaperId)
        {
            var examPaperReviewHistories = await _examPaperReviewHistoryRepository.GetRangeAsync(e => e.ExamPaperId == examPaperId, new List<string> { "Actor" });
            var results = new List<ExamPaperReviewHistoryGetDto>();
            foreach (var h in examPaperReviewHistories)
            {
                switch (h.OperationId)
                {
                    case (int)Operation.ApproveExamPaper:
                        results.Add(new ExamPaperReviewHistoryGetDto
                        {
                            ActorName = h.Actor!.FullName,
                            Id = h.Id,
                            CreatedAt = h.CreatedAt,
                            OperationId = h.OperationId
                        });
                        break;
                    case (int)Operation.AskForReviewForExamPaper:                       
                        results.Add(new ExamPaperReviewHistoryAddReviewerGetDto
                        {
                            ActorName = h.Actor!.FullName,
                            Id = h.Id,
                            CreatedAt = h.CreatedAt,
                            OperationId = h.OperationId,
                            ReviewerName = (await _examPaperReviewerRepository.GetAsync(e => e.Id == h.EntityId, new List<string> { "Reviewer" }))!.Reviewer!.FullName
                        });
                        break;
                    case (int)Operation.RejectExamPaper:
                        results.Add(new ExamPaperReviewHistoryGetDto
                        {
                            ActorName = h.Actor!.FullName,
                            Id = h.Id,
                            CreatedAt = h.CreatedAt,
                            OperationId = h.OperationId
                        });
                        break;
                    case (int)Operation.CreateExamPaper:
                        results.Add(new ExamPaperReviewHistoryGetDto
                        {
                            ActorName = h.Actor!.FullName,
                            Id = h.Id,
                            CreatedAt = h.CreatedAt,
                            OperationId = h.OperationId
                        });
                        break;
                    case (int)Operation.CommentExamPaper:
                        var examPaperComment = await _examPaperCommentRepository.GetByIdAsync(h.EntityId) ?? throw new ArgumentException(null, nameof(h.EntityId));
                        results.Add(new ExamPaperReviewHistoryCommentGetDto
                        {
                            ActorName = h.Actor!.FullName,
                            Id = h.Id,
                            CreatedAt = h.CreatedAt,
                            OperationId = h.OperationId,
                            ActorProfilePicture = h.Actor!.ProfilePicture,
                            Comment = examPaperComment.Comment,
                            IsAuthor = await IsAuthorAsync(examPaperId, h.ActorId) 
                        });
                        break;
                    case (int)Operation.EditExamPaper:
                        var examPaperCommit = await _examPaperCommitRepository.GetByIdAsync(h.EntityId) ?? throw new ArgumentException(null, nameof(h.EntityId));
                        results.Add(new ExamPaperReviewHistoryEditGetDto
                        {
                            ActorName = h.Actor!.FullName,
                            CreatedAt = h.CreatedAt,
                            OperationId = h.OperationId,
                            CommitMessage = examPaperCommit.Message
                        });
                        break;
                    default:
                        throw new SwitchExpressionException(h.OperationId);
                }
            }
            results.Sort((a, b) => a.CreatedAt.CompareTo(b.CreatedAt));
            return results;
        }

        public async Task<bool> IsAuthorAsync(int examPaperId, int userId)
        {
            var examPaper = await _examPaperRepository.GetAsync(eP => eP.Id == examPaperId, new List<string> { "Author" }) ?? throw new ArgumentException(null, nameof(examPaperId));                       
            return examPaper.Author!.Id == userId;
        }

        public async Task<bool> IsReviewerAsync(int examPaperId, int userId)
        {
            var examPaperReviewers = await _examPaperReviewerRepository.GetRangeAsync(ePR => ePR.ExamPaperId == examPaperId);
            var reviewerIds = examPaperReviewers.Select(ePR => ePR.ReviewerId);
            return reviewerIds.Contains(userId);
        }

        public async Task UpdateAsync(int examPaperId, List<ExamPaperQuestion> examPaperQuestionsToUpdate, string commitMessage)
        {
            _examPaperQuestionRepository.DeleteRange(ePQ => ePQ.ExamPaperId == examPaperId);
            await _examPaperQuestionRepository.InsertRangeAsync(examPaperQuestionsToUpdate);
            await _examPaperQuestionRepository.SaveAsync();

            // insert the commit
            var examPaperCommitToCreate = new ExamPaperCommit
            {
                ExamPaperId = examPaperId,
                Message = commitMessage
            };
            await _examPaperCommitRepository.InsertAsync(examPaperCommitToCreate);
            await _examPaperCommitRepository.SaveAsync();

            // insert the history timeline
            var examPaper = await GetByIdAsync(examPaperId) ?? throw new ArgumentException(null, nameof(examPaperId));
            await _examPaperReviewHistoryRepository.InsertAsync(new ExamPaperReviewHistory
            {
                ExamPaperId = examPaperId,
                ActorId = examPaper.AuthorId,
                OperationId = (int)Operation.EditExamPaper,
                EntityId = examPaperCommitToCreate.Id,
                CreatedAt = examPaperCommitToCreate.CommitedAt
            });
            await _examPaperReviewHistoryRepository.SaveAsync();
        }

        public async Task<ExamPaperReviewHistoryCommentGetDto> CommentOnExamPaperReviewAsync(int examPaperId, string comment, User commenter)
        {
            // create examPaperComment
            var commentToCreate = new ExamPaperComment
            {
                ExamPaperId = examPaperId,
                CommenterId = commenter.Id,
                Comment = comment,               
            };
            await _examPaperCommentRepository.InsertAsync(commentToCreate);
            await _examPaperCommentRepository.SaveAsync();

            // add to the review history
            var historyToCreate = new ExamPaperReviewHistory
            {
                ExamPaperId = examPaperId,
                ActorId = commentToCreate.CommenterId,
                OperationId = (int)Operation.CommentExamPaper,
                EntityId = commentToCreate.Id,
                CreatedAt = commentToCreate.CommentedAt
            };
            await _examPaperReviewHistoryRepository.InsertAsync(historyToCreate);
            await _examPaperReviewHistoryRepository.SaveAsync();

            var resultToReturn = new ExamPaperReviewHistoryCommentGetDto
            {
                Id = historyToCreate.Id,
                ActorName = commenter.FullName,
                CreatedAt = historyToCreate.CreatedAt,
                OperationId = historyToCreate.OperationId,
                Comment = commentToCreate.Comment,
                IsAuthor = await IsAuthorAsync(examPaperId, commenter.Id),
                ActorProfilePicture = commenter.ProfilePicture
            };

            return resultToReturn;
        }

        public async Task<IEnumerable<User>> GetReviewersAsync(int examPaperId)
        {
            return (await _examPaperReviewerRepository.GetRangeAsync(epr => epr.ExamPaperId == examPaperId, new List<string> { "Reviewer" })).Select(epr => epr.Reviewer!);
        }

        public async Task<ExamPaper?> GetAsync(Expression<Func<ExamPaper, bool>> predicate, List<string>? includedProps = null)
        {
            return await _examPaperRepository.GetAsync(predicate, includedProps);
        }

        public async Task<User> GetAuthorAsync(int examPaperId)
        {
            var examPaper = await _examPaperRepository.GetAsync(ep => ep.Id == examPaperId, new List<string> { "Author" }) ?? throw new ArgumentException(null, nameof(examPaperId));
            return examPaper.Author!;
        }

        public async Task ApproveExamPaperReviewAsync(int examPaperId, int reviewerId)
        {
            var examPaperReviewer = await _examPaperReviewerRepository.GetAsync(epr => epr.ExamPaperId == examPaperId && epr.ReviewerId == reviewerId) ?? throw new ArgumentException();
            if (examPaperReviewer.ReviewStatus == ExamPaperStatus.Approved)
                return;
            examPaperReviewer.ReviewStatus = ExamPaperStatus.Approved;
            _examPaperReviewerRepository.Update(examPaperReviewer);
            await _examPaperReviewerRepository.SaveAsync();

            var examPaperReviewers = await _examPaperReviewerRepository.GetRangeAsync(epr => epr.ExamPaperId == examPaperId);
            if (examPaperReviewers.All(epr => epr.ReviewStatus == ExamPaperStatus.Approved))
            {
                var examPaper = await _examPaperRepository.GetByIdAsync(examPaperId) ?? throw new ArgumentException(null, nameof(examPaperId));
                examPaper.Status = ExamPaperStatus.Approved;
                _examPaperRepository.Update(examPaper);
                await _examPaperRepository.SaveAsync();
            }

            // add to the history timeline
            var examPaperReviewHistoryToCreate = new ExamPaperReviewHistory
            {
                ExamPaperId = examPaperId,
                ActorId = reviewerId,
                OperationId = (int)Operation.ApproveExamPaper,
                EntityId = examPaperId,
                CreatedAt = DateTime.UtcNow
            };
            await _examPaperReviewHistoryRepository.InsertAsync(examPaperReviewHistoryToCreate);
            await _examPaperReviewHistoryRepository.SaveAsync();
        }

        public async Task RejectExamPaperReviewAsync(int examPaperId, int reviewerId)
        {
            var examPaperReviewer = await _examPaperReviewerRepository.GetAsync(epr => epr.ExamPaperId == examPaperId && epr.ReviewerId == reviewerId) ?? throw new ArgumentException();
            if (examPaperReviewer.ReviewStatus == ExamPaperStatus.Rejected)
                return;
            examPaperReviewer.ReviewStatus = ExamPaperStatus.Rejected;
            _examPaperReviewerRepository.Update(examPaperReviewer);
            await _examPaperReviewerRepository.SaveAsync();

            var examPaperReviewers = await _examPaperReviewerRepository.GetRangeAsync(epr => epr.ExamPaperId == examPaperId);
            if (examPaperReviewers.All(epr => epr.ReviewStatus == ExamPaperStatus.Rejected))
            {
                var examPaper = await _examPaperRepository.GetByIdAsync(examPaperId) ?? throw new ArgumentException(null, nameof(examPaperId));
                examPaper.Status = ExamPaperStatus.Rejected;
                _examPaperRepository.Update(examPaper);
                await _examPaperRepository.SaveAsync();
            }

            // add to the history timeline
            var examPaperReviewHistory = new ExamPaperReviewHistory
            {
                ExamPaperId = examPaperId,
                ActorId = reviewerId,
                OperationId = (int)Operation.RejectExamPaper,
                EntityId = examPaperId,
                CreatedAt  = DateTime.UtcNow
            };
            await _examPaperReviewHistoryRepository.InsertAsync(examPaperReviewHistory);
            await _examPaperReviewHistoryRepository.SaveAsync();
        }
    }
}
