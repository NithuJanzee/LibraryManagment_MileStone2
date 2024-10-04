using LibraryManagment.DTO.RequestDTO.BookRequestDTO;
using LibraryManagment.DTO.ResponseDTO.BookResponseDto;
using LibraryManagment.DTO.ResponseDTO.GenreResponseDTO;
using LibraryManagment.Entity;
using LibraryManagment.InterFace.IRepository.IBookRepository;
using LibraryManagment.InterFace.IService.IBookService;
using Microsoft.AspNetCore.Hosting;

namespace LibraryManagment.Service.BookService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BookService(IBookRepository bookRepository , IWebHostEnvironment _webHostEnvironment)
        {
            _bookRepository = bookRepository;
            webHostEnvironment = _webHostEnvironment;
        }

        public async Task<bool> Checkkey(Guid authorId, Guid PublicationID, Guid genreId)
        {
            bool auhorvalid = await _bookRepository.CheckAuthor(authorId);
            bool publicationId = await _bookRepository.CheckPublication(PublicationID);
            bool Genre = await _bookRepository.CheckGenre(genreId);
            
            return auhorvalid && publicationId && Genre;
        }

        public async Task<BookResponseDTO> Addbook(BookRequestDTO requestDTO)
        {
            if(requestDTO == null)
            {
                throw new Exception("Invalid input");
            }
            try
            {

                if (string.IsNullOrEmpty(webHostEnvironment.WebRootPath))
                {
                    throw new InvalidOperationException("WebRootPath is not set.");
                }

                var uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsDir))
                {
                    Directory.CreateDirectory(uploadsDir);
                }

                var imageUrls = new List<string>();

                // Loop through each image file
                foreach (var imageFile in requestDTO.Image)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    var filePath = Path.Combine(uploadsDir, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    imageUrls.Add($"/uploads/{uniqueFileName}");
                }





                var post = new Book()
                {
                    Name = requestDTO.Name,
                    Image = string.Join(",", imageUrls),
                    copies = requestDTO.copies,
                    AuthorId = requestDTO.AuthorId,
                    PublicationId = requestDTO.PublicationId,
                    GenreId = requestDTO.GenreId,
                };

                var data = await _bookRepository.Addbooks(post);
                //response
                var response = new BookResponseDTO()
                {
                    Name = data.Name,
                    Image = data.Image,
                    copies = data.copies,
                    AuthorId = data.AuthorId,
                    PublicationId = data.PublicationId,
                    GenreId = data.GenreId,
                };
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Some error");
            }
           
        }
        //Get all books
        public async Task<List<BookResponseDTO>> GetAllBooks()
        {
            try
            {
                var response = new List<BookResponseDTO>();
                var data = await _bookRepository.GetAllBooks();
                foreach (var book in data)
                {
                    var res = new BookResponseDTO()
                    {
                        Id = book.Id,
                        Name = book.Name,
                        Image = book.Image,
                        copies = book.copies,
                        AuthorId = book.AuthorId,
                        PublicationId = book.PublicationId,
                        GenreId = book.GenreId,
                    };
                    response.Add(res);
                }
                return response;
               
            }
            catch
            {
                throw new Exception("error");
            }
            
        }
        //Filter Book BY Id
        public async Task<List<BookResponseDTO>> FilterBookBYId(Guid? authorId, Guid? genreId, Guid? publicationId)
        {
            try
            {
                return await _bookRepository.FilterBookBYId(authorId, genreId, publicationId);

            }
            catch (Exception ex)
            {
                throw new Exception("Not Found");
            }
        }

        //get by id 
        public async Task<BookResponseDTO> GetById(Guid Id)
        {

            if (Id == Guid.Empty)
            {
                throw new Exception("Enter Id");
            }


            try
            {

                var data = await _bookRepository.GetById(Id);
                var response = new BookResponseDTO()
                {
                    Id = Id,
                    Name = data.Name,
                    Image= data.Image,
                    copies= data.copies,
                    AuthorId= data.AuthorId,
                    PublicationId= data.PublicationId,
                    GenreId= data.GenreId,

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
