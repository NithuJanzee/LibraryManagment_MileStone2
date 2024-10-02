using LibraryManagment.Entity;

namespace LibraryManagment.InterFace.IRepository.IAurthorRepo
{
    public interface IAurthorRepository
    {
      Task<Author> AddAuthor(Author author);
        Task<List<Author>> GetAllAuthor();
        Task<Author> GetByID(Guid Id);
        Task<Author> EditById(Author author);

    }
}
