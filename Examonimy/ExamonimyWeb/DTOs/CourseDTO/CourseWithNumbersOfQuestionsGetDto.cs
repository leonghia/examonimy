namespace ExamonimyWeb.DTOs.CourseDTO;

public class CourseWithNumbersOfQuestionsGetDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string CourseCode { get; set; }
    public required int NumbersOfQuestions { get; set; }
}
