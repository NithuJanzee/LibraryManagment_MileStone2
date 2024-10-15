using LibraryManagment.DTO.RequestDTO.BookTransactionRequest;
using LibraryManagment.InterFace.IRepository.IBookTransactionRepo;
using Microsoft.Data.SqlClient;
using LibraryManagment.Entity;
using System.Transactions;
using System.Net;
using LibraryManagment.DTO.ResponseDTO.BookTransactionResponse;


namespace LibraryManagment.Repository.BookTransaction
{
    public class BookTransactionRepository : IBookTransactionRepository
    {
        private readonly string _connectionString;

        public BookTransactionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        //Book Requst
      public async Task RequstBook(BookTransactionRequestDTO requestDTO)
      {
            const string query = @"
            INSERT INTO BookTransactions (Id, UserId, BookId, Status, LendingDate, ReturnDate) 
            VALUES (@Id, @UserId, @BookId, @Status, @LendingDate, @ReturnDate)";

            var transactionId = Guid.NewGuid();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", transactionId);
                    command.Parameters.AddWithValue("@UserId", requestDTO.UserId);
                    command.Parameters.AddWithValue("@BookId", requestDTO.BookId);
                    command.Parameters.AddWithValue("@Status", "Requested"); 
                    command.Parameters.AddWithValue("@LendingDate", DBNull.Value); 
                    command.Parameters.AddWithValue("@ReturnDate", DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
      }

        //Update Request TO Lending 
        public async Task UpdateLending(Guid TransactionId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand
                    ("UPDATE BookTransactions SET Status = @Status, LendingDate = @LendingDate, ReturnDate = @ReturnDate WHERE Id = @TransactionId", connection))
                {
                    DateTime lendingDate = DateTime.Now;
                    DateTime returnDate = lendingDate.AddDays(7);

                    command.Parameters.AddWithValue("@TransactionId", TransactionId);
                    command.Parameters.AddWithValue("@Status", "lending");
                    command.Parameters.AddWithValue("@LendingDate", lendingDate);
                    command.Parameters.AddWithValue("@ReturnDate", returnDate);

                    await command.ExecuteNonQueryAsync();
                }
            }

        }

        //Just check the Book Is OverDue or not
        public async Task<BookTransactionBro> ChecBookOverDue(Guid transactionId)
        {
            BookTransactionBro transaction = null;
            const string query = "SELECT * FROM BookTransactions WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", transactionId);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            transaction = new BookTransactionBro()
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                UserId = reader.GetGuid(reader.GetOrdinal("UserId")),
                                BookId = reader.GetGuid(reader.GetOrdinal("BookId")),
                                Status = reader.GetString(reader.GetOrdinal("Status")),
                                LendingDate = reader.IsDBNull(reader.GetOrdinal("LendingDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("LendingDate")),
                                ReturnDate = reader.IsDBNull(reader.GetOrdinal("ReturnDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ReturnDate"))
                            };
                        }
                    }
                }
            }

            return transaction;
        }

        //Check the book count 
        public async Task<int?> CheckTheQuantity(Guid BookId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT copies FROM [LibraryManagment].[dbo].[Book] WHERE id = @BookId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookId", BookId);

                await connection.OpenAsync();
                var result = await command.ExecuteScalarAsync();
                return result != null ? (int?)result : null;
            }

        }
        //Minus the quantity by one
        public async Task<bool> DecrementCopiesAsync(Guid bookId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
            
                string query = @"
                UPDATE [LibraryManagment].[dbo].[Book]
                SET copies = copies - 1
                WHERE id = @BookId AND copies > 0;
                
                SELECT @@ROWCOUNT;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookId", bookId);

                await connection.OpenAsync();
                int rowsAffected = (int)await command.ExecuteScalarAsync();
                return rowsAffected > 0; 
            }
        }

        //Pluse one the quantity 
        public async Task<bool> IncreaseQuantity(Guid bookId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                string query = @"
                UPDATE [LibraryManagment].[dbo].[Book]
                SET copies = copies + 1
                WHERE id = @BookId AND copies > 0;
                
                SELECT @@ROWCOUNT;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookId", bookId);

                await connection.OpenAsync();
                int rowsAffected = (int)await command.ExecuteScalarAsync();
                return rowsAffected > 0;
            }
        }

        //Get Paticual user from tansaction
        public async Task<List<BookTransactionMainDTO>> GetUserTransaction(Guid Id)
        {
            List<BookTransactionMainDTO> MainDTOs = new List<BookTransactionMainDTO>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(); 
                string query = "SELECT Id, UserId, BookId, Status, LendingDate, ReturnDate FROM LibraryManagment.dbo.BookTransactions WHERE UserId = @userID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userID", Id);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync()) 
                        {
                            MainDTOs.Add(new BookTransactionMainDTO()
                            {
                                UserId = reader.GetGuid(1),
                                BookId = reader.GetGuid(2), 
                                Status = reader.GetString(3),
                                LendingDate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4), 
                                ReturnDate = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5) 
                            });
                        }
                    }
                }
            }
            return MainDTOs;
        }

    }

}
    


