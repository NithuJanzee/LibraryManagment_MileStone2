using LibraryManagment.DTO.RequestDTO.PublicationRequestDTO;
using LibraryManagment.DTO.ResponseDTO.PublicationResponseDTO;

namespace LibraryManagment.InterFace.IService.iPublicationService
{
    public interface IPublicationService
    {
        Task<PublicationResponseDTO> AddPublication(PublicationRequestDTO requestDTO);
        Task<List<PublicationResponseDTO>> GetAllPublication();
        Task<PublicationResponseDTO> GetById(Guid id);
    }
}
