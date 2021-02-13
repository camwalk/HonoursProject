namespace API.Helpers
{
    public class UserParams : PaginationParams
    {
        public string CurrentUser { get; set; }
        public string Gender { get; set; }
    }
}