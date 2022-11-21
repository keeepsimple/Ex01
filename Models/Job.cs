using System.ComponentModel.DataAnnotations;

namespace Ex01.Models
{
    public class Job
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên công việc không được để trống")]
        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ProcessingTime { get; set;}

        public int Status { get; set; }
    }
}
