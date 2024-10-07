using LibraryManagment.DTO.ResponseDTO.BookResponseDto;
using LibraryManagment.Entity;
using LibraryManagment.InterFace.IRepository.IBookRepository;
using Microsoft.Data.SqlClient;

namespace LibraryManagment.Repository.BookRepository
{
    public class BookRepository : IBookRepository
    {
        private readonly string _connectionString;

        public BookRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        //the plan is check author publication and genre before adding the book 

        //check author 
        public async Task<bool> CheckAuthor(Guid authorId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT COUNT(1) FROM Author WHERE AuthorId = @AuthorId";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AuthorId", authorId);

                await connection.OpenAsync();
                return (int) await command.ExecuteScalarAsync() > 0;
            }
        }
        //Check Genre
        public async Task<bool> CheckGenre(Guid genreId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT COUNT(1) FROM Genre WHERE ID = @GenreId";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@GenreId", genreId);

                await connection.OpenAsync();
                return (int) await command.ExecuteScalarAsync() > 0;

            }
        }

        //Check publication 
        public async Task<bool> CheckPublication(Guid PublicationId)
        {
            using ( var  connection = new SqlConnection(_connectionString))
            {
                var querry = "SELECT COUNT(1) FROM Publication WHERE ID = @publicationID";
                var command = new SqlCommand(querry, connection);
                command.Parameters.AddWithValue("@publicationID", PublicationId);

                await connection.OpenAsync() ;
                return (int)await command.ExecuteScalarAsync() > 0;
            }
        }

        //add book
        public async Task<Book> Addbooks(Book book)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var querry = "INSERT INTO Book (name, copies, author_id, genre_id, publication_id , image) VALUES (@bookName, @copies, @authorId, @genreId, @publicationID , @image)";
                var command = new SqlCommand(querry,connection);
                command.Parameters.AddWithValue("@bookName", book.Name);
                command.Parameters.AddWithValue("@copies", book.copies);
                command.Parameters.AddWithValue("@authorId", book.AuthorId);
                command.Parameters.AddWithValue("@genreId", book.GenreId);
                command.Parameters.AddWithValue("@publicationID", book.PublicationId);
                command.Parameters.AddWithValue("@image", book.Image);

                await connection.OpenAsync();
                await command.ExecuteScalarAsync();
            }
            return book;
        }

        //Get All books
        public async Task<List<Book>> GetAllBooks()
        {
            var Books = new List<Book>();
            using(var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("select * from Book",connection))
            {
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        Books.Add(new Book
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1),
                            copies = reader.GetInt32(2),    
                            AuthorId = reader.GetGuid(3),
                            GenreId = reader.GetGuid(4),
                            PublicationId = reader.GetGuid(5),
                            Image = reader.GetString(6),

                        });
                    }
                }
            }
            return Books;
           
        }

        //Filter Book
        public async Task<List<BookResponseDTO>> FilterBookBYId(Guid? authorId, Guid? genreId, Guid? publicationId)
        {
            var books = new List<BookResponseDTO>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"SELECT [id], [name], [copies], [author_id], [genre_id], [publication_id], [image]
                             FROM [LibraryManagment].[dbo].[Book]
                             WHERE (@AuthorId IS NULL OR author_id = @AuthorId)
                               AND (@GenreId IS NULL OR genre_id = @GenreId)
                               AND (@PublicationId IS NULL OR publication_id = @PublicationId)
                             ORDER BY CASE WHEN @AuthorId IS NOT NULL THEN author_id ELSE NULL END ASC,
                                      CASE WHEN @GenreId IS NOT NULL THEN genre_id ELSE NULL END ASC,
                                      CASE WHEN @PublicationId IS NOT NULL THEN publication_id ELSE NULL END ASC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AuthorId", (object)authorId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@GenreId", (object)genreId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@PublicationId", (object)publicationId ?? DBNull.Value);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            books.Add(new BookResponseDTO
                            {
                                Id = reader.GetGuid(0),
                                Name = reader.GetString(1),
                                copies = reader.GetInt32(2),
                                AuthorId = reader.GetGuid(3),
                                GenreId = reader.GetGuid(4),
                                PublicationId = reader.GetGuid(5),
                                Image = reader.GetString(6)
                            });
                        }
                    }
                }
            }

            return books;
        }

        //Get By bookId
        public async Task<Book> GetById(Guid id)
        {
            Book book = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "  SELECT * FROM Book WHERE ID = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        book = new Book
                        {
                            Id = reader.GetGuid(0),          
                            Name = reader.GetString(1),      
                            copies = reader.GetInt32(2),     
                            AuthorId = reader.GetGuid(3),     
                            GenreId = reader.GetGuid(4),      
                            PublicationId = reader.GetGuid(5),
                            Image = reader.GetString(6),      
                        };
                    }
                }
                return book;
            }
        }

    }
}
