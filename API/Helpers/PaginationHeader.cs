namespace API.Helpers
{
    public class PaginationHeader
    {
        public PaginationHeader(int currentPage, int itemsOnPage, int totalItems, int totalPages)
        {
            CurrentPage = currentPage;
            ItemsOnPage = itemsOnPage;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }

        public int CurrentPage { get; set; }
        public int ItemsOnPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}