using LibraryManagment.Entity;
using LibraryManagment.InterFace.IRepository.IGenreRepository;
using Microsoft.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace LibraryManagment.Repository.GenreRepository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly string _connectionString;

        public GenreRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        //Add genre
        public async Task<Genre> AddGenre(Genre genre)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                 var command = new SqlCommand(
                    "insert into Genre (Name)values (@name)", connection);
                command.Parameters.AddWithValue("@name", genre.Name);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            return genre;
        }
        //Get all Genre
        public async Task<List<Genre>> GetAllGenre()
        {
            var products = new List<Genre >();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("SELECT * FROM Genre", connection))
            {
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        products.Add(new Genre
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
            }
            return products;
        }

        //Get by ID
        public async Task<Genre> GetById(Guid id)
        {
            Genre genre = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "  SELECT * FROM Genre WHERE ID = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        genre = new Genre
                        {
                            Id= reader.GetGuid(0),
                            Name= reader.GetString(1)
                        };
                    }
                }
                return genre;
            }
        }

    }
}
