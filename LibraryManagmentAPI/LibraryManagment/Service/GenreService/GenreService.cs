using LibraryManagment.DTO.RequestDTO.AuthorRequest;
using LibraryManagment.DTO.RequestDTO.GenreRequestDTO;
using LibraryManagment.DTO.ResponseDTO.AuthorResponse;
using LibraryManagment.DTO.ResponseDTO.GenreResponseDTO;
using LibraryManagment.Entity;
using LibraryManagment.InterFace.IRepository.IGenreRepository;
using LibraryManagment.InterFace.IService.IGenreService;

namespace LibraryManagment.Service.GenreService
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<GenreResponseDTO> AddGenre(GenreRequestDTO requestDTO)
        {
            if (requestDTO == null)
            {
                throw new Exception("Enter the Name Of Genre");
            }
            try
            {
                var PostGenre = new Genre()
                {
                    Name = requestDTO.Name
                };
                var data = await _genreRepository.AddGenre(PostGenre);
                //response
                var response = new GenreResponseDTO()
                {
                    Id = data.Id,
                    Name = requestDTO.Name,
                };
                return response;

            }
            catch (Exception ex) 
            {
                throw new Exception("error");
            }
        }

        //Get all Genre
        public async Task<List<GenreResponseDTO>> GetAllGenre()
        {
            try
            {
            var data = await _genreRepository.GetAllGenre();
                //response
             var response = new List<GenreResponseDTO>();
            foreach (var item in data)
            {
                var res = new GenreResponseDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                response.Add(res);
            }
            return response;

            } catch (Exception ex)
            {
                throw new Exception("Some Thing Wrong");
            }
        }

        //Get By Id
        public async Task<GenreResponseDTO> GetById(Guid Id)
        {

            if (Id == Guid.Empty)
            {
                throw new Exception("Enter Id");
            }


            try
            {
            
                var data = await _genreRepository.GetById(Id);
                var response = new GenreResponseDTO()
                {
                    Id = Id,
                    Name = data.Name
                };
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }
    }
}
