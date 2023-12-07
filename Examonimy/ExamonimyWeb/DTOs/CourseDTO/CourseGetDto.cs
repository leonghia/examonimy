namespace ExamonimyWeb.DTOs.CourseDTO
{
    public class CourseGetDto
    {

        public int Id { get; set; }

        public required string Name { get; set; }

        public required string CourseCode { get; set; }
    }
}
