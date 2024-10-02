using LibraryManagment.DTO.RequestDTO.PublicationRequestDTO;
using LibraryManagment.InterFace.IService.iPublicationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationController : ControllerBase
    {
        private readonly IPublicationService _publicationService;

        public PublicationController(IPublicationService publicationService)
        {
            _publicationService = publicationService;
        }

        [HttpPost("PostPublication")]
        public async Task<IActionResult> AddPublication(PublicationRequestDTO requestDTO)
        {
            if (requestDTO == null) {
                throw new Exception("Error");
            }
            try
            {
            var data = await _publicationService.AddPublication(requestDTO);
            return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllPublication")]
        public async Task<IActionResult> GetAllPublication()
        {
            try
            {
                var data = await _publicationService.GetAllPublication();
                return Ok(data);

            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            };
        }
        [HttpGet("GetByID")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
            var data = await _publicationService.GetById(id);
            return Ok(data);    

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
