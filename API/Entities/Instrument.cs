using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Instruments")]
    public class Instrument
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}