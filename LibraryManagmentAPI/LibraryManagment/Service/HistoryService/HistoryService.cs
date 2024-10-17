using LibraryManagment.Entity;
using LibraryManagment.InterFace.IRepository.IHistory;
using LibraryManagment.InterFace.IService.IHistory;

namespace LibraryManagment.Service.HistoryService
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _historyRepository;

        public HistoryService(IHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }
        public async Task<History> HistoryRequested(Guid UserID, Guid BookId)
        {
            if(UserID == null && BookId == null)
            {
                throw new Exception("Give the values");
            }
            try
            {
            var history = await _historyRepository.HistoryRequested(UserID, BookId);
            return history;

            }
            catch (Exception ex)
            {
                throw new Exception("something wrong");
            }
        }

        //update lending status
        public async Task<History> UpdateLendingStatus(Guid userId, Guid bookId)
        {

            if (userId == Guid.Empty || bookId == Guid.Empty)
            {
                throw new Exception("UserId and BookId cannot be empty");
            }


            var history = await _historyRepository.UpdateLendingStatus(userId, bookId);

            return history;  
        }
    }
}
