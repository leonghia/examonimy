using ExamonimyWeb.DTOs.ExamDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Utilities;
using System.Linq.Expressions;

namespace ExamonimyWeb.Managers.ExamManager
{
    public interface IExamManager
    {
        Task<PagedList<Exam>> GetExamsByTeacherAsync(int teacherId, RequestParams? requestParams = null);
        Task<IEnumerable<Exam>> GetRangeAsync(Expression<Func<Exam, bool>>? predicate = null, Func<IQueryable<Exam>, IOrderedQueryable<Exam>>? orderBy = null);
        Task CreateExamAsync(Exam examToCreate, List<int> mainClassIds);
        Task<string> GetCourseName(int examId);
        Task<PagedList<Exam>> GetPagedListAsync(RequestParams? requestParams = null, Expression<Func<Exam, bool>>? predicate = null, Func<IQueryable<Exam>, IOrderedQueryable<Exam>>? orderBy = null);
        Task<PagedList<Exam>> GetExamsByUserAsync(RequestParams? requestParams, User user);
        Task<Exam?> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(int examId, ExamUpdateDto examUpdateDto);
        Task<IEnumerable<MainClass>> GetMainClassesByExam(int examId);
        Task<Exam?> GetAsync(int examId, List<string>? includedProps = null);
    }
}
