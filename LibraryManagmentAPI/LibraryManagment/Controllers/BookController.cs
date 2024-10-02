using LibraryManagment.DTO.RequestDTO.BookRequestDTO;
using LibraryManagment.Entity;
using LibraryManagment.InterFace.IService.IBookService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost("Addbook")]
        public async Task<IActionResult> PostBook(BookRequestDTO requestDTO)
        {
            if (requestDTO == null)
            {
                return BadRequest("Error");
            }
            bool isvalid = await _bookService.Checkkey(requestDTO.AuthorId,requestDTO.PublicationId,requestDTO.GenreId);
            if (!isvalid)
            {
                return BadRequest("Id is invalid");
            }
            try
            {
                var result = await _bookService.Addbook(requestDTO);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
            var data = await _bookService.GetAllBooks();
            return Ok(data);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("FilterByID")]
        public async Task<IActionResult> FilterById(Guid? authorId,Guid? genreId, Guid? publicationId)
        {
            try
            {
                var books = await _bookService.FilterBookBYId(authorId, genreId, publicationId);
                return Ok(books);

            }
            catch (Exception ex)
            {
                return BadRequest("Not Found");
            }
        }
    }
}
