using ExamonimyWeb.Entities;
using ExamonimyWeb.Repositories.GenericRepository;
using ExamonimyWeb.Utilities;
using System.Linq.Expressions;

namespace ExamonimyWeb.Managers.ExamManager
{
    public class ExamManager : IExamManager
    {
        private readonly IGenericRepository<Exam> _examRepository;

        public ExamManager(IGenericRepository<Exam> examRepository)
        {
            _examRepository = examRepository;
        }

        public async Task<PagedList<Exam>> GetPagedListAsync(RequestParams? requestParams = null, Expression<Func<Exam, bool>>? predicate = null, Func<IQueryable<Exam>, IOrderedQueryable<Exam>>? orderBy = null)
        {
            return await _examRepository.GetPagedListAsync(requestParams, predicate, new List<string> { "MainClass", "ExamPaper", "ExamPaper.Course" }, orderBy);          
        }

        public async Task<IEnumerable<Exam>> GetRangeAsync(Expression<Func<Exam, bool>>? predicate = null, Func<IQueryable<Exam>, IOrderedQueryable<Exam>>? orderBy = null)
        {
            return await _examRepository.GetRangeAsync(predicate, new List<string> { "MainClass", "ExamPaper", "ExamPaper.Course" }, orderBy);
        }
    }
}
