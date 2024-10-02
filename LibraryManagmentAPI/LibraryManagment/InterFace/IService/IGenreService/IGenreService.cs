using LibraryManagment.DTO.RequestDTO.GenreRequestDTO;
using LibraryManagment.DTO.ResponseDTO.GenreResponseDTO;

namespace LibraryManagment.InterFace.IService.IGenreService
{
    public interface IGenreService
    {
        Task<GenreResponseDTO> AddGenre(GenreRequestDTO requestDTO);
        Task<List<GenreResponseDTO>> GetAllGenre();
        Task<GenreResponseDTO> GetById(Guid Id);
    }
}
