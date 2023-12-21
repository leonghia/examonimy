﻿using ExamonimyWeb.DTOs.ExamPaperDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.QuestionManager;
using ExamonimyWeb.Repositories.GenericRepository;

namespace ExamonimyWeb.Managers.ExamPaperManager
{
    public class ExamPaperManager : IExamPaperManager
    {
        private readonly IGenericRepository<ExamPaperQuestion> _examPaperQuestionRepository;
        private readonly IGenericRepository<Question> _questionRepository;
        private readonly IQuestionManager _questionManager;
        private readonly IGenericRepository<ExamPaper> _examPaperRepository;

        public ExamPaperManager(IGenericRepository<ExamPaperQuestion> examPaperQuestionRepository, IGenericRepository<Question> questionRepository, IQuestionManager questionManager, IGenericRepository<ExamPaper> examPaperRepository)
        {
            _examPaperQuestionRepository = examPaperQuestionRepository;
            _questionRepository = questionRepository;
            _questionManager = questionManager;
            _examPaperRepository = examPaperRepository;
        }

        public async Task<IEnumerable<ExamPaperQuestionGetDto>> GetExamPaperQuestionsAsync(int examPaperId)
        {
            var examPaperQuestionsToReturn = new List<ExamPaperQuestionGetDto>();
            var examPaperQuestions = await _examPaperQuestionRepository.GetAsync(ePQ => ePQ.ExamPaperId == examPaperId, null, null);
            foreach (var examPaperQuestion in examPaperQuestions)
            {
                var question = await _questionRepository.GetByIdAsync(examPaperQuestion.QuestionId) ?? throw new ArgumentException(null, nameof(examPaperId));
                var questionToReturn = await _questionManager.GetSpecificQuestionWithoutAnswerDtoAsync(question.Id);
                examPaperQuestionsToReturn.Add(new ExamPaperQuestionGetDto
                {
                    Number = examPaperQuestion.Number,
                    Question = questionToReturn
                });
            }
            examPaperQuestionsToReturn.Sort((a, b) => a.Number.CompareTo(b.Number));
            return examPaperQuestionsToReturn;
        }

        public async Task<bool> IsAuthorAsync(int examPaperId, int userId)
        {
            var examPaper = await _examPaperRepository.GetAsync(eP => eP.Id == examPaperId, new List<string> { "Author" }) ?? throw new ArgumentException(null, nameof(examPaperId));                       
            return examPaper.Author!.Id == userId;
        }
    }
}
