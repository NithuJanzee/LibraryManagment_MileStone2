using LibraryManagment.DTO.RequestDTO.BookTransactionRequest;
using LibraryManagment.DTO.ResponseDTO.BookTransactionResponse;
using LibraryManagment.InterFace.IService.IBookTransactionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Transactions;

namespace LibraryManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookTransactionController : ControllerBase
    {
        private readonly IBookTransactionService _bookTransactionService;

        public BookTransactionController(IBookTransactionService bookTransactionService)
        {
            _bookTransactionService = bookTransactionService;
        }

        [HttpPost("RequestBook")]
        public async Task<IActionResult> RequstBook(BookTransactionRequestDTO requestDTO)
        {
            await _bookTransactionService.RequstBook(requestDTO);
            return Ok();
        }

        [HttpPut("UpdateToLending")]
        public async Task<IActionResult>UpdateLending(Guid TransactionId)
        {
            await _bookTransactionService.UpdateLending(TransactionId);
            return Ok();
        }

        [HttpPost("CheckTheOverDue")]
        public async Task<IActionResult> CheckTheOverDue(Guid transactionID)
        {
            try
            {
                bool isBeforeReturnDate = await _bookTransactionService.ChecBookOverDue(transactionID);
                return Ok(isBeforeReturnDate);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Transaction not found.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CheckTheQuantity")]
        public async Task<IActionResult> CheckTheQuantity(Guid BookId)
        {
            var data = await _bookTransactionService.CheckTheQuantity(BookId);
            return Ok(data);
        }


        //Decerement the copies
        [HttpPut("decrementCopies")]
        public async Task<IActionResult> DecrementCopies(Guid bookId)
        {
            try
            {
                bool isDecremented = await _bookTransactionService.DecrementBookCopiesAsync(bookId);

                if (!isDecremented)
                {
                    return BadRequest("No copies available to decrement or book not found.");
                }

                return Ok("Book copies decremented successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        //Incraese quantity
        [HttpPut("increaseQuantityByOne")]
        public async Task<IActionResult> IncreaseQuantity(Guid BookId)
        {
            try
            {
            bool IncreaseQuantity = await _bookTransactionService.IncreaseQuantity(BookId);
            
                if(!IncreaseQuantity)
                {
                    return BadRequest("Somthing Wrong");
                }
                return Ok("Qantity Updated Successfult");
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound("some error found");
            }
        }

        //get transaction details with id
        [HttpGet("TransactionDetails")]
        public async Task<IActionResult>GetUserTransaction(Guid ID)
        {
            var data = await _bookTransactionService.GetUserTransaction(ID);
            return Ok(data);    
        }

        //Get all Requested Data
        [HttpGet("AllRequestedData")]
        public async Task<IActionResult> GetAllRequestedData()
        {
            try
            {
            var data = await _bookTransactionService.GetAllRequestdData();
            return Ok(data);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Get all GetAllLending
        [HttpGet("GetAllLending")]
        public async Task<IActionResult> GetAllLending()
        {
            try
            {
                var data = await _bookTransactionService.GetAllLending();
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GetLendingBooksByID
        [HttpGet("GetLendingBooksByID")]
        public async Task<IActionResult> GetLendingBooksByID(Guid ID)
        {
            try
            {
                var data = await _bookTransactionService.GetLendingBooksByID(ID);
                return Ok(data);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Delete Transaction
        [HttpDelete("ReturnDelete")]
        public async Task<IActionResult> ReturnDelete(Guid ID)
        {
            try
            {
                var data = await _bookTransactionService.ReturnDelete(ID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
