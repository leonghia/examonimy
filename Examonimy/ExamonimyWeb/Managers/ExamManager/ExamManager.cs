using ExamonimyWeb.Entities;
using ExamonimyWeb.Repositories;
using ExamonimyWeb.Utilities;
using System.Linq.Expressions;

namespace ExamonimyWeb.Managers.ExamManager
{
    public class ExamManager : IExamManager
    {
        private readonly IGenericRepository<Exam> _examRepository;
        private readonly IGenericRepository<ExamMainClass> _examMainClassRepository;

        public ExamManager(IGenericRepository<Exam> examRepository, IGenericRepository<ExamMainClass> examMainClassRepository)
        {
            _examRepository = examRepository;
            _examMainClassRepository = examMainClassRepository;
        }

        public async Task CreateExamAsync(Exam examToCreate, List<int> mainClassIds)
        {
            await _examRepository.InsertAsync(examToCreate);
            await _examRepository.SaveAsync();

            var examMainClassesToCreate = mainClassIds.Select(mainClassId => new ExamMainClass
            {
                MainClassId = mainClassId,
                ExamId = examToCreate.Id
            }).ToList();
            await _examMainClassRepository.InsertRangeAsync(examMainClassesToCreate);
            await _examMainClassRepository.SaveAsync();
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
