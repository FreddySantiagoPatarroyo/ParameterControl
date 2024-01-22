namespace ParameterControl.Models.Pagination
{
    public class PaginationResult
    {
        public int Page { get; set; } = 1;
        public int RecordsPage { get; set; } = 10;
        public int TotalRecords { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords/RecordsPage);
        public string BaseUrl { get; set; }
    }

    public class PaginationResult<T>: PaginationResult
    {
        public List<T> Elements { get; set; }
    }
}
