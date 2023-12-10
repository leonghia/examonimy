using System.Text.Json.Serialization;

namespace ExamonimyWeb.Utilities
{
    public class PaginationMetadata
    {
        [JsonPropertyName("totalCount")]
        public required int TotalCount { get; set; }
        [JsonPropertyName("pageSize")]
        public required int PageSize { get; set; }
        [JsonPropertyName("currentPage")]
        public required int CurrentPage { get; set; }
        [JsonPropertyName("totalPages")]
        public required int TotalPages { get; set; }
    }
}