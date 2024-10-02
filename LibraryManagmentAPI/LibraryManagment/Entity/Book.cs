using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagment.Entity
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string? Image { get; set; }

        public int copies { get; set; }

        public Guid AuthorId { get; set; }
        public Author Author { get; set; }

        public Guid PublicationId { get; set; }
        public Publication Publication { get; set; }

        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
