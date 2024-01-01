using ExamonimyWeb.DTOs.ExamPaperDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ExamonimyWeb.Hubs.ExamPaperTimelineHub;

[Authorize]
public class ExamPaperTimelineHub : Hub<IExamPaperTimelineClient>
{
    
}
