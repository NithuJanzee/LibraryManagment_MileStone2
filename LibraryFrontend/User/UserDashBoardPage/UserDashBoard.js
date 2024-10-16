const loggedUser = localStorage.getItem("loggedUSer");
if (loggedUser) {
    document.addEventListener('DOMContentLoaded', async () => {
        const GetUserDetails = await fetch(`http://localhost:5000/api/User/GetUserDetailsUsingID?NIC=${loggedUser}`);
        const userData = await GetUserDetails.json();

        const GetRequestedData = async () => {
            const RequestedData = await fetch(`http://localhost:5000/api/BookTransaction/TransactionDetails?ID=${userData.userId}`);
            const allData = await RequestedData.json();   

            let RequestedTable = '';

            for (const data of allData) {
                const getBookDetail = await fetch(`http://localhost:5000/api/Book/GetById?id=${data.bookId}`);
                const bookData = await getBookDetail.json();
                console.log(bookData);

                const authorName = await GetAuthor(bookData.authorId);
                const GenreName = await GetGenre(bookData.genreId)
                const PublicationName = await GetPublication(bookData.publicationId)

                RequestedTable += `
                <tr class="table-active" >
                    <td>${bookData.name}</td>
                    <td>${authorName}</td>
                    <td>${GenreName}</td>
                    <td>${PublicationName}</td>
                    <td>Pending</td>
                </tr>`

            }
            document.getElementById('requestedTableData').innerHTML = RequestedTable;
        }

        const GetLendingData = async()=>{
            const LendingData = await fetch (`http://localhost:5000/api/BookTransaction/GetLendingBooksByID?ID=${userData.userId}`)
            const AllLendingData = await LendingData.json()
            console.log(AllLendingData);

            let LendingBookTemplate='';
            for (const data of AllLendingData) {
                const getBookDetail = await fetch(`http://localhost:5000/api/Book/GetById?id=${data.bookId}`);
                const bookData = await getBookDetail.json();
                console.log(bookData);

                const authorName = await GetAuthor(bookData.authorId);
                const GenreName = await GetGenre(bookData.genreId)
                const lendingDate = new Date(data.lendingDate).toLocaleDateString('en-CA'); 
                const DueDate = new Date(data.returnDate).toLocaleDateString('en-CA');

                LendingBookTemplate+=` 
                <tr class="table-active">
                <td>${bookData.name}</td> 
                <td>${authorName}</td> 
                <td>${GenreName}</td> 
                <td>${lendingDate}</td>
                <td>${DueDate}</td> 
              </tr>`
            }
            document.getElementById('lendingDataUppendimg').innerHTML = LendingBookTemplate;
        }


        const GetAuthor = async (authorId) => {
            const AuthorData = await fetch(`http://localhost:5000/api/Aurthor/GetByID?Id=${authorId}`);
            const author = await AuthorData.json();
            return author.name;
        }

        const GetGenre = async (genreId) => {
            const genreData = await fetch(`http://localhost:5000/api/GenreControler/GetById?id=${genreId}`);
            const genre = await genreData.json()
            return genre.name;
        }

        const GetPublication = async (publicationId) => {
            const publicationData = await fetch(`http://localhost:5000/api/Publication/GetByID?id=${publicationId}`);
            const publication = await publicationData.json()
            return publication.name;
        }

        GetRequestedData();
        GetLendingData();
    });
} else {
    alert("some message");
}
