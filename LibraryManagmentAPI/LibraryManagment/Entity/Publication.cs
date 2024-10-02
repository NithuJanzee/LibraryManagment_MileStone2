using System.ComponentModel.DataAnnotations;

namespace LibraryManagment.Entity
{
    public class Publication
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
