using LibraryManagment.DTO.RequestDTO.UserRequest;
using LibraryManagment.DTO.ResponseDTO.UserResponse;
using LibraryManagment.Entity;
using LibraryManagment.InterFace.IRepository.IUserRepo;
using LibraryManagment.InterFace.IService.IUserServ;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

namespace LibraryManagment.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        //Post User
        public async Task<UserResponseDTO> AddUsers(UserRequestDTO request)
        {
            bool isNICExists = await _userRepository.NICExists(request.NIC);
            if (isNICExists)
            {
                throw new Exception("NIC already exists. Please check the NIC.");
            }

            if (request == null)
            {
                throw new Exception("please Fill the form");
            };
            try
            {
                var Post = new Users()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    NIC = request.NIC,
                    PhoneNumber = request.PhoneNumber,
                    Password = request.Password,
                };

                var data = await _userRepository.AddUsers(Post);
                //response
                var response = new UserResponseDTO()
                {
                    UserId = data.UserId,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Email = data.Email,
                    NIC = data.NIC,
                    PhoneNumber = data.PhoneNumber,
                };
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error");
            }
           
        }

        //afi
        //Login 
        public async Task<bool> LoginUserAsync(string NIC, string password)
        {
            try
            {
            return await _userRepository.ValidateUserAsync(NIC, password);

            }catch (Exception ex)
            {
                throw new Exception("Error");
            }
        }

        //user Login
        public async Task<bool> UserLogin(UserLoginDTO userLogin)
        {
            if (userLogin == null)
            {
                throw new Exception("Give the value");
            }

            try
            {
                return await _userRepository.UserLogin(userLogin);
            }
            catch (Exception ex)
            {
                throw new Exception("Error");
            }

        }

        //Find user using nic
        public async Task<UserResponseDTO> FindUserWithNic(string NIC)
        {
            try
            {
            var response = await _userRepository.FindUserWithNic(NIC);
            return response;

            }catch  (Exception ex)
            {
                throw new Exception("Error");
            }
        }

    }
}
