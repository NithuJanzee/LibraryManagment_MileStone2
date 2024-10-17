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
    }
}
