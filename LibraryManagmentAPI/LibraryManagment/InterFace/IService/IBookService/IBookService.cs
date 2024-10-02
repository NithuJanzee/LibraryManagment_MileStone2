using LibraryManagment.DTO.RequestDTO.BookRequestDTO;
using LibraryManagment.DTO.ResponseDTO.BookResponseDto;
using LibraryManagment.Entity;

namespace LibraryManagment.InterFace.IService.IBookService
{
    public interface IBookService
    {
        Task<bool> Checkkey(Guid authorId, Guid PublicationID, Guid genreId);
        Task<BookResponseDTO> Addbook(BookRequestDTO requestDTO);
        Task<List<BookResponseDTO>> GetAllBooks();
        Task<List<BookResponseDTO>> FilterBookBYId(Guid? authorId, Guid? genreId, Guid? publicationId);
    }
}
