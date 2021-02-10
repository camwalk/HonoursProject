using System;

namespace API.DTOs
{
    public class DirectMessageDto
    {
         public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderUsername { get; set; }
        public string SenderPhotoUrl { get; set; }
        public int RecieverId { get; set; }
        public string RecieverUsername { get; set; }
        public string RecieverPhotoUrl { get; set; }
        public string MessageContent { get; set; }
        public DateTime? TimeRead { get; set; }
        public DateTime TimeSent { get; set; }
    }
}