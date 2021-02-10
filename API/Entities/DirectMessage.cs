using System;

namespace API.Entities
{
    public class DirectMessage
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderUsername { get; set; }
        public AppUser Sender { get; set; }
        public int RecieverId { get; set; }
        public string RecieverUsername { get; set; }
        public AppUser Reciever { get; set; }
        public string MessageContent { get; set; }
        public DateTime? TimeRead { get; set; }
        public DateTime TimeSent { get; set; } = DateTime.Now;
        public bool SenderDeleted { get; set; }
        public bool RecieverDeleted { get; set; }
    }
}