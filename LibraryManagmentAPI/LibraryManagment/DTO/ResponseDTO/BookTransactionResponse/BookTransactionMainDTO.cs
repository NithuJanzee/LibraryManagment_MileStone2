﻿using LibraryManagment.Entity;

namespace LibraryManagment.DTO.ResponseDTO.BookTransactionResponse
{
    public class BookTransactionMainDTO
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }


        public string Status { get; set; }
        public DateTime? LendingDate { get; set; }
        public DateTime? ReturnDate { get; set; }


        public Users User { get; set; }
        public Book Book { get; set; }
    }
}