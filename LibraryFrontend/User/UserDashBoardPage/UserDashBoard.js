const loggedUser = localStorage.getItem("loggedUSer");
if (loggedUser) {
    document.addEventListener('DOMContentLoaded', async () => {
        const GetUserDetails = await fetch(`http://localhost:5000/api/User/GetUserDetailsUsingID?NIC=${loggedUser}`);
        const userData = await GetUserDetails.json();
        //  console.log(userData.userId);

        const GetRequestedData = async () => {
            const RequestedData = await fetch(`http://localhost:5000/api/BookTransaction/TransactionDetails?ID=${userData.userId}`);
            const allData = await RequestedData.json();
            console.log(allData);

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
    });
} else {
    alert("some message");
}
