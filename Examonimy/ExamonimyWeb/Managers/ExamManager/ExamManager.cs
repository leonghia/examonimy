using ExamonimyWeb.DTOs.ExamDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Repositories;
using ExamonimyWeb.Utilities;
using System.Linq.Expressions;

namespace ExamonimyWeb.Managers.ExamManager;

public class ExamManager : IExamManager
{
    private readonly IGenericRepository<Exam> _examRepository;
    private readonly IGenericRepository<ExamMainClass> _examMainClassRepository;
    private readonly IGenericRepository<MainClass> _mainClassRepository;
    private readonly IGenericRepository<Student> _studentRepository;       

    public ExamManager(IGenericRepository<Exam> examRepository, IGenericRepository<ExamMainClass> examMainClassRepository, IGenericRepository<MainClass> mainClassRepository, IGenericRepository<Student> studentRepository)
    {
        _examRepository = examRepository;
        _examMainClassRepository = examMainClassRepository;
        _mainClassRepository = mainClassRepository;
        _studentRepository = studentRepository;
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
        return await _examRepository.GetPagedListAsync(requestParams, e => examIds.Contains(e.Id), new List<string>{ "MainClasses", "ExamPaper", "ExamPaper.Course" });
    }

    public async Task<IEnumerable<Exam>> GetRangeAsync(Expression<Func<Exam, bool>>? predicate = null, Func<IQueryable<Exam>, IOrderedQueryable<Exam>>? orderBy = null)
    {
        return await _examRepository.GetRangeAsync(predicate, new List<string>{ "MainClasses", "ExamPaper", "ExamPaper.Course" }, orderBy);
    }

    public async Task<PagedList<Exam>> GetPagedListAsync(RequestParams? requestParams = null, Expression<Func<Exam, bool>>? predicate = null, Func<IQueryable<Exam>, IOrderedQueryable<Exam>>? orderBy = null)
    {
        return await _examRepository.GetPagedListAsync(requestParams, predicate, new List<string> { "MainClasses", "ExamPaper", "ExamPaper.Course" }, orderBy);
    }

    public async Task<PagedList<Exam>> GetExamsByUserAsync(RequestParams? requestParams, User user)
    {
        if (user.RoleId == (int)Enums.Role.Admin)
        {
            return await _examRepository.GetPagedListAsync(requestParams, null, new List<string> { "MainClasses", "ExamPaper", "ExamPaper.Course" });
        }
        if (user.RoleId == (int)Enums.Role.Student)
        {
            var student = await _studentRepository.GetByIdAsync(user.Id) ?? throw new ArgumentException(null, nameof(user.Id));
            var examIds = (await _examMainClassRepository.GetRangeAsync(emc => emc.MainClassId == student.MainClassId && DateTime.Compare(emc.Exam!.To, DateTime.UtcNow) > 0)).Select(emc => emc.ExamId);
            return await _examRepository.GetPagedListAsync(requestParams, e => examIds.Contains(e.Id), new List<string> { "ExamPaper", "ExamPaper.Course" });
        }
        throw new ArgumentException(null, nameof(user.RoleId));
    }

    public async Task<Exam?> GetByIdAsync(int id)
    {
        return await _examRepository.GetByIdAsync(id);
    }

    public async Task DeleteAsync(int id)
    {
        await _examRepository.DeleteAsync(id);
        await _examRepository.SaveAsync();
    }

    public async Task UpdateAsync(int examId, ExamUpdateDto examUpdateDto)
    {
        var exam = await _examRepository.GetByIdAsync(examId) ?? throw new ArgumentException(null, nameof(examId));
        exam.From = examUpdateDto.From;
        exam.To = examUpdateDto.To;
        exam.ExamPaperId = examUpdateDto.ExamPaperId;
        _examRepository.Update(exam);
        await _examRepository.SaveAsync();
    }

    public async Task<IEnumerable<MainClass>> GetMainClassesByExam(int examId)
    {
        return (await _examMainClassRepository.GetRangeAsync(emc => emc.ExamId == examId, new List<string> { "MainClass" })).Select(emc => emc.MainClass!);
    }

    public async Task<Exam?> GetAsync(int examId, List<string>? includedProps = null)
    {
        return await _examRepository.GetAsync(e => e.Id == examId, includedProps);
    }
}
