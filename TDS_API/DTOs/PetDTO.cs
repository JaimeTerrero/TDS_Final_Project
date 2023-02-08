using System.ComponentModel.DataAnnotations.Schema;

namespace TDS_API.DTOs
{
    public class PetDTO
    {
        public string Name { get; set; }
        public DateTime MissingDate { get; set; }
        public long Reward { get; set; }
        public long ContactNumber { get; set; }
        public string Description { get; set; }
    }
}
