using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PhotoUrl { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string SearchingFor { get; set; }
        public string Background { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ExperienceLevel { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
        public ICollection<InstrumentDto> PreferredInstruments { get; set; }
    }
}