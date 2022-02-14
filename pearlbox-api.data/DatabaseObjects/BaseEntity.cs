using System.ComponentModel.DataAnnotations;

namespace pearlbox_api.data.DatabaseObjects
{
    public class BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
    }
}