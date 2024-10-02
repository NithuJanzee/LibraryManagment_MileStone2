using LibraryManagment.DTO.RequestDTO.BookTransactionRequest;
using LibraryManagment.Repository.BookTransaction;

namespace LibraryManagment.InterFace.IRepository.IBookTransactionRepo
{
    public interface IBookTransactionRepository
    {
        Task RequstBook(BookTransactionRequestDTO requestDTO);
        Task UpdateLending(Guid TransactionId);
        Task<BookTransactionBro> ChecBookOverDue(Guid transactionId);
        Task<int?> CheckTheQuantity(Guid BookId);
        Task<bool> DecrementCopiesAsync(Guid bookId);
        Task<bool> IncreaseQuantity(Guid bookId);
    }
}
