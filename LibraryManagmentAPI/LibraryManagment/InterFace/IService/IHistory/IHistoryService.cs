﻿using LibraryManagment.Entity;

namespace LibraryManagment.InterFace.IService.IHistory
{
    public interface IHistoryService
    {
        Task<History> HistoryRequested(Guid UserID, Guid BookId);
        Task<History> UpdateLendingStatus(Guid userId, Guid bookId);
    }
}
