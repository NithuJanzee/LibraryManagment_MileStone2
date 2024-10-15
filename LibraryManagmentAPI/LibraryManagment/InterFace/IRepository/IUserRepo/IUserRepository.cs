using LibraryManagment.DTO.RequestDTO.UserRequest;
using LibraryManagment.DTO.ResponseDTO.UserResponse;
using LibraryManagment.Entity;

namespace LibraryManagment.InterFace.IRepository.IUserRepo
{
    public interface IUserRepository
    {
         Task<Users> AddUsers(Users users);
        Task<bool> NICExists(string nic);
        Task<bool> ValidateUserAsync(string NIC, string password);
        Task<bool> UserLogin(UserLoginDTO userLogin);
        Task<UserResponseDTO> FindUserWithNic(string NIC);
    }
}
