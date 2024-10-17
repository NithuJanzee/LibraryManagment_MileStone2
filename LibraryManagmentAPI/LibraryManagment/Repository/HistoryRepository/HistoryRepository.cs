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
        public async Task<bool> UpdateReturnedDate(Guid userId, Guid bookId, string status)
        {
            // Define allowed statuses based on the CHECK constraint
            var allowedStatuses = new List<string>
            {
                "returned-overdue",
                "returned-onTime",
                "lending",
                "requested"
            };

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    UPDATE dbo.History
                    SET ReturnedDate = GETDATE(),  -- Use the current date and time
                        Status = @Status
                    WHERE UserId = @UserId AND BookId = @BookId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@BookId", bookId);

                    await conn.OpenAsync();
                    int affectedRows = await cmd.ExecuteNonQueryAsync();
                    return affectedRows > 0;
                }
            }
        }

        //Get all 
        public async Task<List<History>> GetAll()
        {
            List<History> historyList = new List<History>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Id, UserId, BookId, Status, RequestedDate, LendedDate, DueDate, ReturnedDate FROM LibraryManagment.dbo.History;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            History history = new History
                            {
                                Id = reader.GetGuid(0), 
                                UserId = reader.GetGuid(1), 
                                BookId = reader.GetGuid(2), 
                                Status = reader.GetString(3), 
                                RequestedDate = reader.GetDateTime(4),
                                LendedDate = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5), 
                                DueDate = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6), 
                                ReturnedDate = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7)
                            };

                            historyList.Add(history);
                        }
                    }
                }
            }

            return historyList;
        }

        //Get by user id
        public async Task<List<History>> GetByUserId(Guid Id)
        {
            List<History> historyList = new List<History>();
            using(SqlConnection connection = new SqlConnection (_connectionString))
            {
                connection.Open();
                string querry = " SELECT Id, UserId, BookId, Status, RequestedDate, LendedDate, DueDate, ReturnedDate FROM LibraryManagment.dbo.History WHERE UserId = @UserId";
                using(SqlCommand command = new SqlCommand(querry, connection))
                {
                    command.Parameters.AddWithValue("@UserId", Id);
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            History history = new History()
                            {
                                Id =reader.GetGuid(0),
                                UserId = reader.GetGuid(1),
                                BookId = reader.GetGuid(2),
                                Status = reader.GetString(3),
                                RequestedDate = reader.GetDateTime(4),
                                LendedDate = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                                DueDate = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
                                ReturnedDate = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7)
                            };
                            historyList.Add(history);
                        }
                    }
                }
            }
            return historyList;

        }


    }
}
