using System.ComponentModel.DataAnnotations.Schema;

namespace TDS_API.DTOs
{
    public class PeopleDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public long ContactNumber { get; set; }
        public DateTime MissingDate { get; set; }
        public long Reward { get; set; }
        public string Description { get; set; }
    }
}
