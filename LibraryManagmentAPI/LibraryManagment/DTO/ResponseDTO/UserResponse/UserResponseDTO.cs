namespace LibraryManagment.DTO.ResponseDTO.UserResponse
{
    public class UserResponseDTO
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NIC { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
