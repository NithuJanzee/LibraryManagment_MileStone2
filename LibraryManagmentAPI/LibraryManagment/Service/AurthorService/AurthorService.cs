using LibraryManagment.DTO.RequestDTO.AuthorRequest;
using LibraryManagment.DTO.ResponseDTO.AuthorResponse;
using LibraryManagment.Entity;
using LibraryManagment.InterFace.IRepository.IAurthorRepo;
using LibraryManagment.InterFace.IService.IAurthorServ;
using System.ComponentModel;

namespace LibraryManagment.Service.AurthorService
{
    public class AurthorService : IAurthorService
    {
        private readonly IAurthorRepository _aurthorRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AurthorService(IAurthorRepository aurthorRepository , IWebHostEnvironment webHostEnvironment)
        {
            _aurthorRepository = aurthorRepository;
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        }

        //post
        public async Task<AurthorResponseDTO> AddAuthor(AurthorRequestDTO Request)
        {
            Console.WriteLine($"WebRootPath:{_webHostEnvironment.WebRootPath}");
            //chech if webrootpath is null
            if (string.IsNullOrEmpty(_webHostEnvironment.WebRootPath))
            {
                throw new InvalidOperationException("WebRootPath is not set");
            }
            var uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsDir))
            {
                Directory.CreateDirectory(uploadsDir);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Request.Image.FileName;
            var filePath= Path.Combine(uploadsDir, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Request.Image.CopyToAsync(stream);
            }

            if (Request == null)
            {
                throw new Exception("Please Fill the Author Detail");
            };
            var Post = new Author()
            {
                Name = Request.Name,
                Image = $"/uploads/{uniqueFileName}",
            };
            var SentData = await _aurthorRepository.AddAuthor(Post);
            //response
            var response = new AurthorResponseDTO()
            {
                Name = SentData.Name,
                Image = SentData.Image
            };
            return response;
        }
        //GetAll
        public async Task<List<AurthorResponseDTO>> GetAllAuthor()
        {
            try { 
            var Data = await _aurthorRepository.GetAllAuthor();
            //response
            var response = new List<AurthorResponseDTO>();
            foreach (var Author in Data)
            {
                var res = new AurthorResponseDTO();
                res.Id = Author.AuthorId;
                res.Name = Author.Name;
                res.Image = Author.Image;
                response.Add(res);
            }
            return response;
            }catch (Exception ex)

            {
                throw new Exception("Cant Get The AuthorDetails");
            }
        }

        //Get By ID
        public async Task<AurthorResponseDTO> GetByID(Guid Id)
        {
            var data = await _aurthorRepository.GetByID(Id);
            if (data == null)
            {
                throw new Exception("Please Enter the ID");
            }
            //response
            var response = new AurthorResponseDTO()
            { 
                Id = Id,
                Name = data.Name,
                Image = data.Image
            };
            return response;
        }

        //Edit
        public async Task<AurthorResponseDTO> EditById(Guid Id, AurthorRequestDTO request)
        {
            var FindAuthor = await _aurthorRepository.GetByID(Id);
            if (FindAuthor == null)
            {
                throw new Exception("Author Not found");
            }

            FindAuthor.Name = request.Name;
          //  FindAuthor.Image = request.Image;

            var EditAuthor = await _aurthorRepository.EditById(FindAuthor);
            if (EditAuthor == null)
            {
                throw new Exception("Cannot Edit");
            }
            //response
            var response = new AurthorResponseDTO()
            {
                Id = Id,
                Name = EditAuthor.Name,
                Image = EditAuthor.Image
            };
            return response;
        }

      

    }
}
