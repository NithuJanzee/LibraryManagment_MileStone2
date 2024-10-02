using LibraryManagment.Entity;

namespace LibraryManagment.DTO.RequestDTO.BookRequestDTO
{
    public class BookRequestDTO
    {
        public string Name { get; set; }

        public List<IFormFile>? Image { get; set; }

        public int copies { get; set; }

        public Guid AuthorId { get; set; }
        public Guid PublicationId { get; set; }
        public Guid GenreId { get; set; }
    }
}
