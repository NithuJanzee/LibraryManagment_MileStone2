using LibraryManagment.DTO.RequestDTO.BookTransactionRequest;
using LibraryManagment.DTO.ResponseDTO.BookTransactionResponse;
using LibraryManagment.InterFace.IRepository.IBookTransactionRepo;
using LibraryManagment.InterFace.IService.IBookTransactionService;
using System.Text.RegularExpressions;
using System.Transactions;

namespace LibraryManagment.Service.BookTransactionService
{
    public class BookTransactionService : IBookTransactionService
    {
        private readonly IBookTransactionRepository _bookTransactionRepository;

        public BookTransactionService(IBookTransactionRepository bookTransactionRepository)
        {
            _bookTransactionRepository = bookTransactionRepository;
        }

        public async Task RequstBook(BookTransactionRequestDTO requestDTO)
        {
            await _bookTransactionRepository.RequstBook(requestDTO);
        }
        public async Task UpdateLending(Guid TransactionId)
        {
            await _bookTransactionRepository.UpdateLending(TransactionId);
        }

        //Check ove due or not
        public async Task<bool> ChecBookOverDue(Guid transactionID)
        {

            var transaction = await _bookTransactionRepository.ChecBookOverDue(transactionID);

            if (transaction == null)
            {
                throw new KeyNotFoundException("Transaction not found.");
            }


            if (!transaction.LendingDate.HasValue || !transaction.ReturnDate.HasValue)
            {
                throw new InvalidOperationException("Lending or Return Date is not set.");
            }


            var currentDate = DateTime.UtcNow;
            var returnDate = transaction.ReturnDate.Value;


            return currentDate <= returnDate;
        }

        //Check the book quantity
        public async Task<bool> CheckTheQuantity(Guid BookID)
        {

            var quantity = await _bookTransactionRepository.CheckTheQuantity(BookID);

            if (quantity == null)
            {
                throw new KeyNotFoundException("Book not found with the given ID");
            }

            return quantity.Value > 0;
        }

        //Decrement the copies
        public async Task<bool> DecrementBookCopiesAsync(Guid bookId)
        {
            var quantity = await _bookTransactionRepository.CheckTheQuantity(bookId);

            if (quantity == null)
            {
                throw new KeyNotFoundException("Book not found with the given ID");
            }

            if (quantity.Value <= 0)
            {
                return false;
            }

            // Decrement the copies by 1
            return await _bookTransactionRepository.DecrementCopiesAsync(bookId);
        }
        //Increase by one
        public async Task<bool> IncreaseQuantity(Guid bookId)
        {
            var quantity = await _bookTransactionRepository.CheckTheQuantity(bookId);

            if (quantity == null)
            {
                throw new KeyNotFoundException("Book not found with the given ID");
            }

            if (quantity.Value <= 0)
            {
                return false;
            }

            // Decrement the copies by 1
            return await _bookTransactionRepository.IncreaseQuantity(bookId);
        }

        //get transaction details by user id
        public async Task<List<BookTransactionMainDTO>> GetUserTransaction(Guid Id)
        {
            var response = await _bookTransactionRepository.GetUserTransaction(Id);
            if (response == null)
            {
                throw new Exception("Error");
            }
            return response;
        }

        //get all requested data
        public async Task<List<BookTransactionMainDTO>> GetAllRequestdData()
        {
            try
            {
                var response = await _bookTransactionRepository.GetAllPendingRequest();
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex);
            }
        }

        //get all GetAllLending
        public async Task<List<BookTransactionMainDTO>> GetAllLending()
        {
            try
            {
                var response = await _bookTransactionRepository.GetAllLending();
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex);
            }
        }

        //GetLendingBooksByID
        public async Task<List<BookTransactionMainDTO>> GetLendingBooksByID(Guid ID)
        {
            try
            {
                var response = await _bookTransactionRepository.GetLendingBooksByID(ID);
                return response;
            }catch(Exception ex)
            {
                throw new Exception("error" + ex);
            }
        }
    }
}
    

