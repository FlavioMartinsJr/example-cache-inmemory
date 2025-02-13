namespace Produto.Domain.Pagination
{
    public class PagedList<T> 
    {
        public int CurrentPage { get; set; }
        public int ItensPages { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public IEnumerable<T>? Dados { get; private set; }

        public PagedList(IEnumerable<T> items, int currentPage, int pageSize, int count)
        {
            CurrentPage = currentPage;
            TotalPages = 0;
            if (pageSize > 0)
            {
                TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            }
            ItensPages = pageSize;
            TotalCount = count;
            Dados = items;
            HasPreviousPage = (CurrentPage > 1 && CurrentPage <= TotalPages + 1);
            HasNextPage = CurrentPage < TotalPages;
        }
    }
}
