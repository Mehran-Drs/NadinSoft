namespace NadinSoft.Common.DTOs
{
    public class PaginationDto<T> where T : class
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public List<T> Source { get; set;}
    }
}
