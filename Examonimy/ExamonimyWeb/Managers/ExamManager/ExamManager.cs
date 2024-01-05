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
        private readonly IGenericRepository<MainClass> _mainClassRepository;
        private readonly List<string> _includedProps = new() { "MainClasses", "ExamPaper", "ExamPaper.Course" };

        public ExamManager(IGenericRepository<Exam> examRepository, IGenericRepository<ExamMainClass> examMainClassRepository, IGenericRepository<MainClass> mainClassRepository)
        {
            _examRepository = examRepository;
            _examMainClassRepository = examMainClassRepository;
            _mainClassRepository = mainClassRepository;
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

        public async Task<string> GetCourseName(int examId)
        {
            var exam = await _examRepository.GetAsync(e => e.Id == examId, new List<string> { "ExamPaper.Course" }) ?? throw new ArgumentException(null, nameof(examId));
            return exam.ExamPaper!.Course!.Name;
        }

        public async Task<PagedList<Exam>> GetExamsByTeacherAsync(int teacherId, RequestParams? requestParams = null)
        {
            var mainClassIds = (await _mainClassRepository.GetRangeAsync(mc => mc.TeacherId == teacherId)).Select(mc => mc.Id);
            var examMainClasses = await _examMainClassRepository.GetRangeAsync(emc => mainClassIds.Contains(emc.MainClassId));
            var examIds = examMainClasses.GroupBy(emc => emc.ExamId).Select(ig => ig.Key);
            return await _examRepository.GetPagedListAsync(requestParams, e => examIds.Contains(e.Id), _includedProps);
        }

        public async Task<IEnumerable<Exam>> GetRangeAsync(Expression<Func<Exam, bool>>? predicate = null, Func<IQueryable<Exam>, IOrderedQueryable<Exam>>? orderBy = null)
        {
            return await _examRepository.GetRangeAsync(predicate, _includedProps, orderBy);
        }

        public async Task<PagedList<Exam>> GetPagedListAsync(RequestParams? requestParams = null, Expression<Func<Exam, bool>>? predicate = null, Func<IQueryable<Exam>, IOrderedQueryable<Exam>>? orderBy = null)
        {
            return await _examRepository.GetPagedListAsync(requestParams, predicate, _includedProps, orderBy);
        }
    }
}
