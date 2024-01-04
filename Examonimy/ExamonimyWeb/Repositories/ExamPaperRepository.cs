using ExamonimyWeb.DatabaseContexts;
using ExamonimyWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamonimyWeb.Repositories;

public class ExamPaperRepository : GenericRepository<ExamPaper>
{
    
   
    public ExamPaperRepository(ExamonimyContext context) : base(context)
    {
        
        
    }

    public async Task<IDictionary<object, int>> CountGroupByAsync()
    {
        IQueryable<ExamPaper> query = base._dbSet;
        var temp = await query
            .GroupBy(ep => ep.CourseId)
            .Select(e => new { CourseId = e.Key, Count = e.Count() })
            .ToListAsync();
        var dict = new Dictionary<object, int>(temp.Count);
        foreach (var t in temp)
        {
            dict.Add(t.CourseId, t.Count);
        }
        return dict;
    }
}
