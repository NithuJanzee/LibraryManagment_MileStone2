using LibraryManagment.DTO.RequestDTO.HistoryRequest;
using LibraryManagment.InterFace.IService.IHistory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace LibraryManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpPost("UpdateRequestHistory")]
        public async Task<IActionResult> HistoryRequested(Guid UserId, Guid BookId)
        {
            try
            {

               var data = await _historyService.HistoryRequested(UserId, BookId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateLending")]
        public async Task<IActionResult> UpdateLendingStatus(Guid UserId, Guid BookId)
        {
            if(UserId==null && BookId==null)
            {
                return BadRequest("Bad request");
            }
            try
            {
            var history = await _historyService.UpdateLendingStatus(UserId, BookId);

            return Ok(history);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
 
        }
        [HttpPut("return")]
        public async Task<IActionResult> UpdateReturnDate(Guid userId, Guid bookId, [FromQuery] string status)
        {
            if (userId == Guid.Empty || bookId == Guid.Empty)
            {
                return BadRequest("User ID and Book ID must be provided.");
            }

            try
            {
                bool result = await _historyService.UpdateReturnedDate(userId, bookId, status);
                if (result)
                {
                    return Ok("Return date updated successfully.");
                }
                return NotFound("No records found to update.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateReturnDate: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllonTime()
        {
            try
            {
                var data = await _historyService.GetAll();
                return Ok(data);
            }catch(Exception ex)
            {
                return NotFound("Data not found");
            }
        }
        [HttpGet("GetByUserId")]
        public async Task<IActionResult> GetByUserId(Guid Id)
        {
            try
            {
                var data = await _historyService.GetByUserId(Id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return NotFound("Data not found");
            }
        }
    }
}
