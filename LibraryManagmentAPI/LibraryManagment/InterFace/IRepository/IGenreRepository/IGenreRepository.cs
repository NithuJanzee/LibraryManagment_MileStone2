using LibraryManagment.Entity;

namespace LibraryManagment.InterFace.IRepository.IGenreRepository
{
    public interface IGenreRepository
    {
        Task<Genre> AddGenre(Genre genre);
        Task<List<Genre>> GetAllGenre();
        Task<Genre> GetById(Guid id);
    }
}
