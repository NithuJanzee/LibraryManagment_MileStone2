using LibraryManagment.DTO.ResponseDTO.BookResponseDto;
using LibraryManagment.Entity;

namespace LibraryManagment.InterFace.IRepository.IBookRepository
{
    public interface IBookRepository
    {
        Task<bool> CheckAuthor(Guid authorId);
        Task<bool> CheckGenre(Guid genreId);
        Task<bool> CheckPublication(Guid PublicationId);
        Task<Book> Addbooks(Book book);
        Task<List<Book>> GetAllBooks();
        Task<List<BookResponseDTO>> FilterBookBYId(Guid? authorId, Guid? genreId, Guid? publicationId);
        Task<Book> GetById(Guid id);


    }
}
