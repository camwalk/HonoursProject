using System;
using System.Collections.Generic;
using API.Extensions;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; }
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string SearchingFor { get; set; }
        public string Background { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ExperienceLevel { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Instrument> PreferredInstruments { get; set; }
        public ICollection<DirectMessage> DirectMessagesSent { get; set; }
        public ICollection<DirectMessage> DirectMessagesRecieved { get; set; }
    }
}