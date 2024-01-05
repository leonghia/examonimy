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
    }
}
