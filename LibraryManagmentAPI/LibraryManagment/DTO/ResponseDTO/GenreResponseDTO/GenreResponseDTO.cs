using System.ComponentModel.DataAnnotations;

namespace LibraryManagment.DTO.ResponseDTO.GenreResponseDTO
{
    public class GenreResponseDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set;}
    }
}
