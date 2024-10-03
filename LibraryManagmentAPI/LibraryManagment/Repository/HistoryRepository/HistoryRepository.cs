using LibraryManagment.Entity;
using LibraryManagment.InterFace.IRepository.IHistory;
using Microsoft.Data.SqlClient;

namespace LibraryManagment.Repository.HistoryRepository
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly string _connectionString;

        public HistoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<History> HistoryRequested(Guid userId, Guid bookId)
        {
           
            var history = new History
            {
                Id = Guid.NewGuid(),            
                UserId = userId,                
                BookId = bookId,                
                Status = "requested",          
                RequestedDate = DateTime.Now,  
                LendedDate = null,            
                DueDate = null,                 
                ReturnedDate = null   
            };

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
            INSERT INTO History (Id, UserId, BookId, Status, RequestedDate, LendedDate, DueDate, ReturnedDate)
            VALUES (@Id, @UserId, @BookId, @Status, @RequestedDate, NULL, NULL, NULL)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                   
                    cmd.Parameters.AddWithValue("@Id", history.Id);
                    cmd.Parameters.AddWithValue("@UserId", history.UserId);
                    cmd.Parameters.AddWithValue("@BookId", history.BookId);
                    cmd.Parameters.AddWithValue("@Status", history.Status);
                    cmd.Parameters.AddWithValue("@RequestedDate", history.RequestedDate ?? (object)DBNull.Value);

                    await conn.OpenAsync();  
                    await cmd.ExecuteNonQueryAsync();
                }
            }

           
            return history;
        }

        //update into lended
        public async Task<History> UpdateLendingStatus(Guid userId, Guid bookId)
        {
            var history = new History
            {
                UserId = userId,
                BookId = bookId,
                Status = "lended",
                LendedDate = DateTime.Now, 
                DueDate = DateTime.Now.AddDays(7)  
            };

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    UPDATE History
                    SET Status = @Status,
                        LendedDate = @LendedDate,
                        DueDate = @DueDate
                    WHERE UserId = @UserId AND BookId = @BookId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", history.UserId);
                    cmd.Parameters.AddWithValue("@BookId", history.BookId);
                    cmd.Parameters.AddWithValue("@Status", "lending");
                    cmd.Parameters.AddWithValue("@LendedDate", history.LendedDate);
                    cmd.Parameters.AddWithValue("@DueDate", history.DueDate);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }

            return history;  
        }

        //next update it into returned ontime / overdue

    }
}
