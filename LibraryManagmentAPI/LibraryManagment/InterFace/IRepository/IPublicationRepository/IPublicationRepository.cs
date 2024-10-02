using LibraryManagment.Entity;

namespace LibraryManagment.InterFace.IRepository.IPublicationRepository
{
    public interface IPublicationRepository
    {
        Task<Publication> AddPublication(Publication publication);
        Task<List<Publication>> GetAllPublication();
        Task<Publication> GetById(Guid id);
    }
}
