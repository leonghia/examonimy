using Microsoft.AspNetCore.Mvc;

namespace ExamonimyWeb.Models
{
    public class CustomProblemDetails : ProblemDetails
    {
        public CustomProblemDetails(string instance, string type, int status, string title)
        {
            Detail = "See the errors field for details.";
            Instance = instance;
            Type = type;
            Status = status;
            Title = $"One or more {title} errors occured.";
        }
    }
}
