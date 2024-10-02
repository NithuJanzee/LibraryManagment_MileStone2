using LibraryManagment.Entity;
using LibraryManagment.InterFace.IRepository.IAurthorRepo;
using Microsoft.Data.SqlClient;
using System.Diagnostics.Contracts;

namespace LibraryManagment.Repository.AurthorRepository
{
    public class AurthorRepository: IAurthorRepository
    {
        private readonly string _connectionString;

        public AurthorRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        //post
        public async Task<Author> AddAuthor(Author author)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "INSERT INTO Author (AuthorId ,Name, Image) VALUES (@Id ,@Name, @Image)", connection);
                command.Parameters.AddWithValue("@id", Guid.NewGuid());
                command.Parameters.AddWithValue("@Name", author.Name);
                command.Parameters.AddWithValue("@Image", author.Image);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            return author;

        }
        //Get All Author
        public async Task<List<Author>> GetAllAuthor()
        {
            var List = new List<Author>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("select * from Author", connection))
            {
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        List.Add(new Author
                        {
                            AuthorId = reader.GetGuid(0),
                            Name = reader.GetString(1),
                            Image = reader.GetString(2)
                        });
                    }
                }
                return List;
            }
        }
        //Get By Id
        public async Task<Author> GetByID(Guid Id)
        {
            Author author = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Author WHERE AuthorId =@Id", connection);
                command.Parameters.AddWithValue("@Id", Id);
                await connection.OpenAsync();
                using(var reader = await command.ExecuteReaderAsync())
                {
                    if(await reader.ReadAsync())
                    {
                        author = new Author
                        {
                            AuthorId = reader.GetGuid(0),
                            Name = reader.GetString(1),
                            Image = reader.GetString(2)
                        };
                    }
                }
            }
            return author;
        }

        //Edit By ID
        public async Task<Author> EditById(Author author)
        {
            Author Editauthor = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "UPDATE Author SET Name = @name, Image = @image WHERE AuthorId = @id", connection);
                command.Parameters.AddWithValue("@id", author.AuthorId);
                command.Parameters.AddWithValue("@name",author.Name);
                command.Parameters.AddWithValue("@image",author.Image);

                await connection.OpenAsync();

                var Data = await command.ExecuteNonQueryAsync();
                if(Data > 0)
                {
                    Editauthor = author;
                }
            }
            return Editauthor;
        }
       
    }
}
