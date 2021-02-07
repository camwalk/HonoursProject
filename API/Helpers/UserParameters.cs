namespace API.Helpers
{
    public class UserParameters
    {
        private const int MaxPageLength = 50;
        public int PageNumber { get; set; } = 1;
        public int _pageLength { get; set; } = 10;
        public int PageLength
        {
            get => _pageLength;
            set => _pageLength = (value > MaxPageLength) ? MaxPageLength : value;
        }
    }
}