document.addEventListener('DOMContentLoaded', async (event) => {
    event.preventDefault()

    //add total book count
    const GetAllBookCount = async () => {
        const GetAllData = await fetch(`http://localhost:5000/api/Book/GetAllBooks`);
        const allBooks = await GetAllData.json();
        const totalBooks = allBooks.length;
        document.getElementById('totalBooks').innerHTML = totalBooks;
    }
    const GetAllUsersCount = async () => {
        const GetAllUsers = await fetch(`http://localhost:5000/api/User/GetAllUsers`);
        const AllUsers = await GetAllUsers.json();
        const totalUsers = AllUsers.length;
        document.getElementById('totalUsers').innerHTML = totalUsers;
    }
    const GetAllPendingRequest = async () => {
        const GetAllPending = await fetch('http://localhost:5000/api/BookTransaction/AllRequestedData');
        const allPending = await GetAllPending.json();
        const PendingCount = allPending.length;
        document.getElementById('totalBorrowedBooks').innerHTML = PendingCount;
    }
    const GetAllLendingBooks = async () => {
        const AllLendBooks = await fetch(`http://localhost:5000/api/BookTransaction/GetAllLending`);
        const LendBooks = await AllLendBooks.json();
        const countLendBooks = await LendBooks.length;
        document.getElementById('totalLendingBooks').innerHTML = countLendBooks;
    }

    //Lending request Table
    const LendingTable = async () => {
        const fetchAllLendingRequest = await fetch(`http://localhost:5000/api/BookTransaction/AllRequestedData`);
        const LendingRequest = await fetchAllLendingRequest.json();

        let TableBodyTemplate = '';
        for (const Request of LendingRequest) {
            const UserDetails = await fetch(`http://localhost:5000/api/User/UserDetailsGUID?ID=${Request.userId}`);
            const userName = await UserDetails.json();

            const BookDetails = await fetch(`http://localhost:5000/api/Book/GetById?id=${Request.bookId}`)
            const Book = await BookDetails.json();


            TableBodyTemplate += `
            <tr>
                <td>${userName.firstName} ${userName.lastName}</td>
                <td>${Book.name}</td>
                <td>${Book.copies}</td>
                <td>
                    <button type="button" class="acceptBtn" data-id="${Request.transactionId}"
                    data-UserId="${Request.userId}"
                    data-BookId="${Request.bookId}">Accept</button>
                    <button type="button" id="declineBtn">Decline</button>
                </td>
            </tr>
            `;
        }
        document.getElementById('requestTableBody').innerHTML = TableBodyTemplate;

        const AcceptBtn = document.querySelectorAll('.acceptBtn');
        AcceptBtn.forEach(button => {
            button.addEventListener('click', async (data) => {
                openModal()
                const TransactionID = data.target.getAttribute("data-id");
                const UserId = data.target.getAttribute("data-UserId")
                const BookId = data.target.getAttribute("data-BookId");
                document.getElementById('confirmAccept').addEventListener('click', async () => {
                    //Update requested to lending
                    const postTransaction = await fetch(`http://localhost:5000/api/BookTransaction/UpdateToLending?TransactionId=${TransactionID}`, {
                        method: 'PUT',
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    })
                    //Decrease Quantity
                    const DecreaseQuantity = await fetch(`http://localhost:5000/api/BookTransaction/decrementCopies?bookId=${BookId}`,{
                        method: 'PUT',
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    })
                    //Update History Table
                    const UpdateTransaction = await fetch(`http://localhost:5000/api/History/UpdateLending?UserId=${UserId}&BookId=${BookId}`, {
                        method: 'PUT',
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    })
                    if (UpdateTransaction) {
                        window.location.reload()
                    }
                })
            })
        });
    }


    function openModal() {
        const confirmationModal = new bootstrap.Modal(document.getElementById('confirmationModal'));
        confirmationModal.show();
    }

    //Open total book modal
    const TotalBookData = async () => {
        const TotalBookBtn = document.getElementById('Show1');
        TotalBookBtn.addEventListener('click', async () => {
            const GetAllBooks = await fetch(`http://localhost:5000/api/Book/GetAllBooks`);
            const AllBooks = await GetAllBooks.json()

            let AllBookTemplate = '';
            for (const data of AllBooks) {
                const FetchAuthorName = await fetch(
                    `http://localhost:5000/api/Aurthor/GetByID?Id=${data.authorId}`
                );
                const AuthorName = await FetchAuthorName.json();
                const FetchGenreName = await fetch(
                    `http://localhost:5000/api/GenreControler/GetById?id=${data.genreId}`
                );
                const Genre = await FetchGenreName.json();
                const FetchPublicationName = await fetch(
                    `http://localhost:5000/api/Publication/GetByID?id=${data.publicationId}`
                );
                const Publication = await FetchPublicationName.json();

                AllBookTemplate += `
                <tr>
                <td>${data.name}</td>
                <td>${AuthorName.name}</td>
                <td>${Genre.name}</td>
                <td>${Publication.name}</td>
                <td>${data.copies}</td>
            </tr>
                `
            }
            document.getElementById('AllbookListBody').innerHTML = AllBookTemplate;
        })
    }

    //open all user Model Window
    const TotalUserData = async()=>{
        const totalUserBtn = document.getElementById('Show2');
        totalUserBtn.addEventListener('click' , async()=>{
            const FetchAllUserData = await fetch(`http://localhost:5000/api/User/GetAllUsers`);
            const AllUsers = await FetchAllUserData.json();

            let AllUserTemplate = '';
            AllUsers.forEach((data)=>{
                AllUserTemplate+=`
                <tr>
                            <td>${data.firstName}</td>
                            <td>${data.lastName}</td>
                            <td>${data.nic}</td>
                            <td>${data.email}</td>
                            <td>${data.phoneNumber}</td>
                        </tr>
                `
            })
            document.getElementById('AlluserListBody').innerHTML = AllUserTemplate;
        })
    }
    //Open total lending Books
    const TotalLendingBooks = async()=>{
        const TotalLendingBtn = document.getElementById('Show4');
        TotalLendingBtn.addEventListener('click' , async()=>{
            const GetAllLendingBookData = await fetch(`http://localhost:5000/api/BookTransaction/GetAllLending`);
            const AllLending = await GetAllLendingBookData.json();
            console.log(AllLending)

            let AllLendingBookTemplate = '';
            for (const data of AllLending) {
                //Book Data
                const GetBookData = await fetch(`http://localhost:5000/api/Book/GetById?id=${data.bookId}`);
                const bookData = await GetBookData.json();
                //User data
                const GetUserData = await fetch(`http://localhost:5000/api/User/UserDetailsGUID?ID=${data.userId}`)
                const UserData = await GetUserData.json();
                // lend and return date 
                const lendingDate = new Date(data.lendingDate).toLocaleDateString('en-CA'); 
                const DueDate = new Date(data.returnDate).toLocaleDateString('en-CA');
        

                AllLendingBookTemplate+=`
                    <tr>
                    <td>${bookData.name}</td>
                    <td>${UserData.firstName} ${UserData.lastName}</td>
                    <td>${lendingDate}</td>
                    <td>${DueDate}</td>
                    </tr>
                `
            }
            document.getElementById('AlllendingListBody').innerHTML = AllLendingBookTemplate;
        })
    }

    GetAllBookCount()
    GetAllUsersCount()
    GetAllPendingRequest()
    GetAllLendingBooks()
    LendingTable()
    TotalBookData()
    TotalUserData()
    TotalLendingBooks()
})