namespace ExamonimyWeb.ViewModels
{
    public class SectionHeadingViewModel
    {
        public SectionHeadingViewModel(string? title, string? description)
        {
            Title = title;
            Description = description;
        }

        public string? Title { get; set; }
        public string? Description { get; set; }


    }
}
