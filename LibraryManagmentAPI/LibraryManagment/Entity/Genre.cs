using System.ComponentModel.DataAnnotations;

namespace LibraryManagment.Entity
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
