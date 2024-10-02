using LibraryManagment.Entity;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagment.Repository.BookTransaction
{
    public class BookTransactionBro
    {
        [Key]
        public Guid Id { get; set; }


        public Guid UserId { get; set; }
        public Guid BookId { get; set; }


        public string Status { get; set; }
        public DateTime? LendingDate { get; set; }
        public DateTime? ReturnDate { get; set; }


        public Users User { get; set; }
        public Book Book { get; set; }
    }
}
