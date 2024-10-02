using LibraryManagment.DTO.RequestDTO.AuthorRequest;
using LibraryManagment.DTO.ResponseDTO;
using LibraryManagment.InterFace.IService.IAurthorServ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AurthorController : ControllerBase
    {
        private readonly IAurthorService _aurthorService;

        public AurthorController(IAurthorService aurthorService)
        {
            _aurthorService = aurthorService;
        }

        [HttpPost("AddAuthor")]
        public async Task<IActionResult> AddAuthor(AurthorRequestDTO Request)
        {
            try
            {
                var data = await _aurthorService.AddAuthor(Request);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllAuthor")]
        public async Task<IActionResult> GetAllAuthor()
        {
            try
            {
            var data = await _aurthorService.GetAllAuthor();
            return Ok(data);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetByID")]
        public async Task<IActionResult> GetByID(Guid Id)
        {
            try
            {
            var data = await _aurthorService.GetByID(Id);
            return Ok(data);

            }catch (Exception ex)
            {
              return BadRequest(ex.Message);
            }
        }

        [HttpPut("EditById")]
        public async Task<IActionResult> EditById(Guid Id , AurthorRequestDTO request)
        {
            try
            {
            var data = await _aurthorService.EditById(Id , request);
            return Ok(data);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }
    }
}
