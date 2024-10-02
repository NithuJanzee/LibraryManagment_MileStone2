using LibraryManagment.DTO.RequestDTO.GenreRequestDTO;
using LibraryManagment.InterFace.IService.IGenreService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreControler : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreControler(IGenreService genreService)
        {
            _genreService = genreService;
        }
        [HttpPost("AddGenre")]
        public async Task<IActionResult> AddGenre(GenreRequestDTO requestDTO)
        {
            try
            {
            var data = await _genreService.AddGenre(requestDTO);
            return Ok(data);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllGenre")]
        public async Task<IActionResult> GetAllGenre()
        {
            try
            {
            var data = await _genreService.GetAllGenre();
            return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var data = await _genreService.GetById(id);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }
    }
}
