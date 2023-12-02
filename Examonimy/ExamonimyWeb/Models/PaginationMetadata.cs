namespace ExamonimyWeb.Models
{
    public class PaginationMetadata
    {
        public required int TotalCount { get; set; }
        public required int PageSize { get; set; }
        public required int CurrentPage { get; set;}
        public required int TotalPages { get; set; }
    }
}
