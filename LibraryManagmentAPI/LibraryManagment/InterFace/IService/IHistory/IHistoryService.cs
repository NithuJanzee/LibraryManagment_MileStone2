using LibraryManagment.Entity;

namespace LibraryManagment.InterFace.IService.IHistory
{
    public interface IHistoryService
    {
        Task<History> HistoryRequested(Guid UserID, Guid BookId);
        Task<History> UpdateLendingStatus(Guid userId, Guid bookId);
        Task<bool> UpdateReturnedDate(Guid userId, Guid bookId, string status);
        Task<List<History>> GetAll();
        Task<List<History>> GetByUserId(Guid Id);
    }
}
