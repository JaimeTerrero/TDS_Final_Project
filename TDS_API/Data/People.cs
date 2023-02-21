using System.ComponentModel.DataAnnotations.Schema;

namespace TDS_API.Data
{
    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long ContactNumber { get; set; }
        public DateTime MissingDate { get; set; }
        public long Reward { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public IFormFile FileUri { get; set; }
        public string? ActualFileUrl { get; set; }
    }
}
