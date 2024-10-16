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
                    const postTransaction = await fetch(`http://localhost:5000/api/BookTransaction/UpdateToLending?TransactionId=${TransactionID}`, {
                        method: 'PUT',
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    })

                    const UpdateTransaction = await fetch(`http://localhost:5000/api/History/UpdateLending?UserId=${UserId}&BookId=${BookId}`,{
                        method: 'PUT',
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    })
                    if(UpdateTransaction){
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

    GetAllBookCount()
    GetAllUsersCount()
    GetAllPendingRequest()
    GetAllLendingBooks()
    LendingTable()
})