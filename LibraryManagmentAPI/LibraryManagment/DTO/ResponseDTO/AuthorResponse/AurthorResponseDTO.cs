using System.ComponentModel.DataAnnotations;

namespace LibraryManagment.DTO.ResponseDTO.AuthorResponse
{
    public class AurthorResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
