using System.ComponentModel.DataAnnotations;

namespace pearlbox_api.data.Models
{
    public class BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
    }
}