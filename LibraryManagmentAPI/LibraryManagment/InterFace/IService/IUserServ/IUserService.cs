using LibraryManagment.DTO.RequestDTO.UserRequest;
using LibraryManagment.DTO.ResponseDTO.UserResponse;

namespace LibraryManagment.InterFace.IService.IUserServ
{
    public interface IUserService
    {
        Task<UserResponseDTO> AddUsers(UserRequestDTO request);
        Task<bool> LoginUserAsync(string NIC, string password);
        Task<bool> UserLogin(UserLoginDTO userLogin);
        Task<UserResponseDTO> FindUserWithNic(string NIC);
    }
}
