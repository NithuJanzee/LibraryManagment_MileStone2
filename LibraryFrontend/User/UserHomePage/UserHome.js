document.addEventListener('DOMContentLoaded', async (event) => {
    event.preventDefault();
    GetAllBook = async () => {
        const GetAllBookData = await fetch(`http://localhost:5000/api/Book/GetAllBooks`);
        const bookData = await GetAllBookData.json();
        return bookData;
    }

   const AddBooksInGrid =async(bookData) => {
        let dataTemplate = '';
        for (const data of bookData) {
            const Image = `http://localhost:5000${data.image}`.trim();
            const FetchAuthorName = await fetch(`http://localhost:5000/api/Aurthor/GetByID?Id=${data.authorId}`);
            const AuthorName = await FetchAuthorName.json();
            const FetchGenreName = await fetch(`http://localhost:5000/api/GenreControler/GetById?id=${data.genreId}`)
            const Genre = await FetchGenreName.json();
            const FetchPublicationName = await fetch(`http://localhost:5000/api/Publication/GetByID?id=${data.publicationId}`)
            const Publication = await FetchPublicationName.json();

            dataTemplate += `<div class="book">
            <img src="${Image}" alt="Book Cover">
            <h3>${data.name}</h3>
            <p><strong>Author:</strong>${AuthorName.name}</p>
            <p><strong>Genre:</strong> ${Genre.name}</p>
            <p><strong>Publication:</strong>${Publication.name}</p>
            <button class="add-to-cart" data-id="${data.id}">More Details</button>
        </div>`
        
        document.querySelector('.book-container').innerHTML = dataTemplate;
        }
    }

    //Genre Drop Down
    const FilterDropGenre = async()=>{
        const FetchGenreName = await fetch(`http://localhost:5000/api/GenreControler/GetAllGenre`);
        const GenreName = await FetchGenreName.json()
        let genreTemplate = '';
        GenreName.forEach((data) => {
            genreTemplate+=`
            <option value="${data.name}" data-id="${data.id}">${data.name}</option>
            `
        });
        document.getElementById('genre').innerHTML = ` <option value="Default">Genre</option>` + genreTemplate;
    }
    //Publication DropDown
    const FilterDropPublication = async()=>{
        const FetchPublication = await fetch(`http://localhost:5000/api/Publication/GetAllPublication`);
        const PublicationName = await FetchPublication.json();
        let publicationTemplate = '';
        PublicationName.forEach((data)=>{
        publicationTemplate += `<option value="Publisher 1" data-id="${data.id}">${data.name}</option>`
        })
        document.getElementById('publication').innerHTML =` <option value="Default">Publication</option>` + publicationTemplate;
    };
    //Author DropDown
    const FilterDropAuthor = async()=>{
        const FetchAuther = await fetch(`http://localhost:5000/api/Aurthor/GetAllAuthor`);
        const AuthorName = await FetchAuther.json();
        let AuthorTemplate = '';
        AuthorName.forEach((data)=>{
            AuthorTemplate+=`<option value="Author 1" data-id="${data.id}">${data.name}</option>`
        });
        document.getElementById('author').innerHTML =`<option value="Default">Publication</option>` + AuthorTemplate
    }

    const bookData = await GetAllBook()
    await AddBooksInGrid(bookData)
    FilterDropGenre()
    FilterDropPublication();
    FilterDropAuthor()


})

