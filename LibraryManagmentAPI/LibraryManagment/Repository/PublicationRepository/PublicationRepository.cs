using LibraryManagment.Entity;
using LibraryManagment.InterFace.IRepository.IPublicationRepository;
using Microsoft.Data.SqlClient;

namespace LibraryManagment.Repository.PublicationRepository
{
    public class PublicationRepository : IPublicationRepository
    {
        private readonly string _connectionString;

        public PublicationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        //Add publication
        public async Task<Publication> AddPublication(Publication publication)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "INSERT INTO Publication (Name) VALUES (@name)" , connection);
                command.Parameters.AddWithValue("@name", publication.Name);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            return publication;
        }
        //Get All publication
        public async Task<List<Publication>> GetAllPublication()
        {
            var publications = new List<Publication>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("select * from Publication", connection))
            {
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        publications.Add(new Publication
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1),
                        });
                    }
                }

            }
            return publications;
        }

        //Get by Id
        public async Task<Publication> GetById(Guid id)
        {
            Publication publication = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "SELECT * FROM Publication WHERE ID = @id;", connection);
                command.Parameters.AddWithValue("@id", id);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        publication = new Publication
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1)
                        };
                    }
                }
                return publication;
            }
        }
    }
}
