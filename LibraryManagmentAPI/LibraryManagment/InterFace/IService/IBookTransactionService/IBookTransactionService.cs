using LibraryManagment.DTO.RequestDTO.BookTransactionRequest;
using LibraryManagment.DTO.ResponseDTO.BookTransactionResponse;
using LibraryManagment.Repository.BookTransaction;

namespace LibraryManagment.InterFace.IService.IBookTransactionService
{
    public interface IBookTransactionService
    {
        Task RequstBook(BookTransactionRequestDTO requestDTO);
        Task UpdateLending(Guid TransactionId);
        Task<bool> ChecBookOverDue(Guid transactionID);
        Task<bool> CheckTheQuantity(Guid BookID);
        Task<bool> DecrementBookCopiesAsync(Guid bookId);
        Task<bool> IncreaseQuantity(Guid bookId);
        Task<List<BookTransactionMainDTO>> GetUserTransaction(Guid Id);
        Task<List<BookTransactionMainDTO>> GetAllRequestdData();
        Task<List<BookTransactionMainDTO>> GetAllLending();

    }
}
