﻿using ExamonimyWeb.DTOs.QuestionDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Models;
using ExamonimyWeb.Repositories;

namespace ExamonimyWeb.Managers.QuestionManager
{
    public interface IQuestionManager
    {
        IGenericRepository<MultipleChoiceQuestionWithOneCorrectAnswer> MultipleChoiceQuestionWithOneCorrectAnswerRepository { get; }
        IGenericRepository<MultipleChoiceQuestionWithMultipleCorrectAnswers> MultipleChoiceQuestionWithMultipleCorrectAnswersRepository { get; }
        IGenericRepository<TrueFalseQuestion> TrueFalseQuestionRepository { get; }
        IGenericRepository<ShortAnswerQuestion> ShortAnswerQuestionRepository { get; }
        IGenericRepository<FillInBlankQuestion> FillInBlankQuestionRepository { get; }
        Task<IEnumerable<QuestionGetDto>> GetQuestionsAsync(List<Question> questions);
        Task<QuestionGetDto> GetSpecificQuestionWithAnswerDtoAsync(int questionId);
        Task<QuestionWithoutAnswerGetDto> GetSpecificQuestionWithoutAnswerDtoAsync(int questionId);
        Task<bool> DoesQuestionExistAsync(int questionId);
        Task<bool> IsAuthorAsync(int questionId, int userId);
        Task<QuestionViewModel> GetQuestionViewModelAsync(int questionId, User contextUser);
        Task<Tuple<int, T>> CreateQuestionAsync<T>(QuestionCreateDto questionCreateDto, IGenericRepository<T> specificQuestionRepository, int authorId) where T : class;
        Task UpdateThenSaveAsync(int id, QuestionUpdateDto questionUpdateDto);
        Task DeleteThenSaveAsync(int id);
        Task<Question?> GetSingleByIdAsync(object id);       
    }
}
