namespace LibraryManagment.DTO.ResponseDTO.BookResponseDto
{
    public class BookResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public int copies { get; set; }
        public Guid AuthorId { get; set; }
        public Guid PublicationId { get; set; }
        public Guid GenreId { get; set; }
    }
}
