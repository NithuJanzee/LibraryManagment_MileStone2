using LibraryManagment.Entity;

namespace LibraryManagment.InterFace.IRepository.IHistory
{
    public interface IHistoryRepository
    {
        Task<History> HistoryRequested(Guid userId, Guid bookId);
        Task<History> UpdateLendingStatus(Guid userId, Guid bookId);
        Task<bool> UpdateReturnedDate(Guid userId, Guid bookId, string status);
        //Task<bool> UpdateReturnedDate(Guid userId, Guid bookId, string status);

    }
}
