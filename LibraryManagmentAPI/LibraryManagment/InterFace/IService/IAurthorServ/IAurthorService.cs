using LibraryManagment.DTO.RequestDTO.AuthorRequest;
using LibraryManagment.DTO.ResponseDTO.AuthorResponse;

namespace LibraryManagment.InterFace.IService.IAurthorServ
{
    public interface IAurthorService
    {
        Task<AurthorResponseDTO> AddAuthor(AurthorRequestDTO Request);
        Task<List<AurthorResponseDTO>> GetAllAuthor();
        Task<AurthorResponseDTO> GetByID(Guid Id);
        Task<AurthorResponseDTO> EditById(Guid Id, AurthorRequestDTO request);
    }
}
