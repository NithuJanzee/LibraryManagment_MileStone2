document.addEventListener('DOMContentLoaded' ,async(event)=>{
    event.preventDefault()

    //add total book count
    const GetAllBookCount = async()=>{
        const GetAllData = await fetch (`http://localhost:5000/api/Book/GetAllBooks`);
        const allBooks = await GetAllData.json();
        const totalBooks = allBooks.length;
        document.getElementById('totalBooks').innerHTML = totalBooks;
    }
    const GetAllUsersCount = async()=>{
        const GetAllUsers = await fetch (`http://localhost:5000/api/User/GetAllUsers`);
        const AllUsers = await GetAllUsers.json();
        const totalUsers = AllUsers.length;
        document.getElementById('totalUsers').innerHTML = totalUsers;
    } 
    const GetAllPendingRequest = async()=>{
        const GetAllPending = await fetch('http://localhost:5000/api/BookTransaction/AllRequestedData');
        const allPending = await GetAllPending.json();
        const PendingCount = allPending.length;
        document.getElementById('totalBorrowedBooks').innerHTML = PendingCount;
    }
    const GetAllLendingBooks = async()=>{
        const AllLendBooks = await fetch (`http://localhost:5000/api/BookTransaction/GetAllLending`);
        const LendBooks = await AllLendBooks.json();
        const countLendBooks = await LendBooks.length;
        document.getElementById('totalLendingBooks').innerHTML = countLendBooks;
    }
    GetAllBookCount()
    GetAllUsersCount()
    GetAllPendingRequest()
    GetAllLendingBooks()
})