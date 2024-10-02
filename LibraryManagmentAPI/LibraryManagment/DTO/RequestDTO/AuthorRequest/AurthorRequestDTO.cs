using System.ComponentModel.DataAnnotations;

namespace LibraryManagment.DTO.RequestDTO.AuthorRequest
{
    public class AurthorRequestDTO
    {
        [Required]
        public string Name { get; set; }

        public IFormFile Image { get; set; }
    }
}
