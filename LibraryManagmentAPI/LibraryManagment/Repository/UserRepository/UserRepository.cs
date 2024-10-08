﻿using LibraryManagment.DTO.RequestDTO.UserRequest;
using LibraryManagment.Entity;
using LibraryManagment.InterFace.IRepository.IUserRepo;
using Microsoft.Data.SqlClient;

namespace LibraryManagment.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        //Check NIC is Alredy Exit
        public async Task<bool> NICExists(string nic)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT COUNT(1) FROM Users WHERE NIC = @NIC", connection);
                command.Parameters.AddWithValue("@NIC", nic);

                await connection.OpenAsync();
                var result = await command.ExecuteScalarAsync();
                return (int)result > 0;
            }
        }

        //Add user 
        public async Task<Users> AddUsers(Users users)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "INSERT INTO Users (UserId, FirstName, LastName, NIC, Email, PhoneNumber, Password) VALUES (@id, @FirstName, @LastName, @NIC, @Email, @PhoneNumber, @Password)", connection);
                command.Parameters.AddWithValue("@id", Guid.NewGuid());
                command.Parameters.AddWithValue("@FirstName", users.FirstName);
                command.Parameters.AddWithValue("@LastName", users.LastName);
                command.Parameters.AddWithValue("@NIC", users.NIC);
                command.Parameters.AddWithValue("@Email", users.Email);
                command.Parameters.AddWithValue("@PhoneNumber", users.PhoneNumber);
                command.Parameters.AddWithValue("@Password", users.Password);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            return users;
        }

        //login api
        public async Task<bool> ValidateUserAsync(string NIC, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT COUNT(*) FROM Users WHERE NIC = @NIC AND Password = @Password", connection);
                command.Parameters.AddWithValue("@NIC", NIC);
                command.Parameters.AddWithValue("@Password", password);

                await connection.OpenAsync();
                var result = await command.ExecuteScalarAsync();

                // If the count is greater than 0, return true; otherwise, false
                return (int)result > 0;
            }
        }

        //Login User
        public async Task<bool> UserLogin(UserLoginDTO userLogin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT COUNT(*) FROM Users WHERE NIC = @NIC AND Password = @Password", connection);
                command.Parameters.AddWithValue("@NIC", userLogin.NIC);
                command.Parameters.AddWithValue("@Password",userLogin.Password);

                await connection.OpenAsync();
                var result = await command.ExecuteScalarAsync();

                return (int)result > 0;
            }
        }
    }
    
}
