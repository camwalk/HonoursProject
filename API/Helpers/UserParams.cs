namespace API.Helpers
{
    public class UserParams : PaginationParams
    {
        public string CurrentUser { get; set; }
        public string SearchLocation { get; set; }
        public string SearchInstrument { get; set; }
        public string SortBy { get; set; } = "lastActive";
    }
}