namespace ExamonimyWeb.DTOs.CourseDTO;

public class CourseGetDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string CourseCode { get; set; }
}

public class CourseWithNumbersOfExamPapersGetDto : CourseGetDto
{
    
    public required int NumbersOfExamPapers { get; set; }
}
