using System.ComponentModel.DataAnnotations;

namespace LibraryManagment.Entity
{
    public class Author
    {
     
        public Guid AuthorId { get; set; }
        public string Name { get; set; }

        public string Image { get; set; }
    }
}
