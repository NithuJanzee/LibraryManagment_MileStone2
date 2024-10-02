using LibraryManagment.DTO.RequestDTO.PublicationRequestDTO;
using LibraryManagment.DTO.ResponseDTO.GenreResponseDTO;
using LibraryManagment.DTO.ResponseDTO.PublicationResponseDTO;
using LibraryManagment.Entity;
using LibraryManagment.InterFace.IRepository.IPublicationRepository;
using LibraryManagment.InterFace.IService.iPublicationService;

namespace LibraryManagment.Service.PublicationService
{
    public class PublicationService : IPublicationService
    {
        private readonly IPublicationRepository _publicationRepository;

        public PublicationService(IPublicationRepository publicationRepository)
        {
            _publicationRepository = publicationRepository;
        }

        //Add publication
        public async Task<PublicationResponseDTO> AddPublication(PublicationRequestDTO requestDTO)
        {
            if (requestDTO == null)
            {
                throw new Exception("Give The value");
            }
            try
            {
                var PostNew = new Publication()
                {
                    Name = requestDTO.Name,
                };
                var PostPublication = await _publicationRepository.AddPublication(PostNew);
                //response
                var response = new PublicationResponseDTO()
                {
                    Name = requestDTO.Name,
                };
                return response;

                }
            catch (Exception ex)
            {
                throw new Exception("Error");
            }
        }
        //Get All publication
        public async Task<List<PublicationResponseDTO>> GetAllPublication()
        {
            try
            {
                var data = await _publicationRepository.GetAllPublication();
                //response
                var response = new List<PublicationResponseDTO>();
                foreach (var item in data)
                {
                    var res = new PublicationResponseDTO()
                    {
                        Id = item.Id,
                        Name = item.Name,
                    };
                    response.Add(res);
                }
                return response;

            }
            catch (Exception ex)
            {

                throw new Exception("Error");
            }
        }
        //Get by ID
        public async Task<PublicationResponseDTO> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new Exception("Enter Id");
            }
            try
            {

                var data = await _publicationRepository.GetById(id);
                var response = new PublicationResponseDTO()
                {
                    Id = id,
                    Name = data.Name
                };
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
