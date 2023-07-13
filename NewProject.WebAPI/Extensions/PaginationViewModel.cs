namespace NewProject.WebAPI.Extensions
{
    public class PaginationViewModel<T>
    {
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public T? Data { get; set; }
    }
}
