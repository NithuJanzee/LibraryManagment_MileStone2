document.addEventListener('DOMContentLoaded', async () => {

    //Update books
    const PrintBookData = async () => {
        const FetchAllBookData = await fetch(`http://localhost:5000/api/Book/GetAllBooks`);
        const AllBookData = await FetchAllBookData.json();

        let bookDatatemoplate = '';
        for (const data of AllBookData) {
            //Make First is first 
            const AllImages = data.image.split(',').map(img => img.trim());
            const firstImage = AllImages[0]; 
            const Image = `http://localhost:5000${firstImage}`.trim();

            const FetchAuthorName = await fetch(`http://localhost:5000/api/Aurthor/GetByID?Id=${data.authorId}`);
            const AuthorName = await FetchAuthorName.json();
            const FetchGenreName = await fetch(`http://localhost:5000/api/GenreControler/GetById?id=${data.genreId}`)
            const Genre = await FetchGenreName.json();
            const FetchPublicationName = await fetch(`http://localhost:5000/api/Publication/GetByID?id=${data.publicationId}`)
            const Publication = await FetchPublicationName.json();

            bookDatatemoplate += `<div class="col">
            <div class="card h-100">
                <div class="product-image-container">
                    <img src="${Image}" class="card-img-top product-image" alt="Product Image">
                </div>
                <div class="card-body" id="gridContent"> 
                    <h5 class="card-title limit-text-to-2-lines">${data.name}</h5>
                    <p class="card-text">
                        <strong>Author:</strong>${AuthorName.name}<br>
                        <strong>Genre:</strong>${Genre.name}<br>
                        <strong>Publication:</strong>${Publication.name}
                    </p>
                    <button type="button" class="btn btn-primary" id="openBookModal"
                    id="openModelWindow" data-id="${data.id}">View More</button>
                </div>
            </div>
        </div>`

            document.getElementById('UppendBooks').innerHTML = bookDatatemoplate;
        }
    }

    //DropDowns
    //Genre Drop Down
    const FilterDropGenre = async () => {
        const FetchGenreName = await fetch(`http://localhost:5000/api/GenreControler/GetAllGenre`);
        const GenreName = await FetchGenreName.json()
        let genreTemplate = '';
        GenreName.forEach((data) => {
            genreTemplate += `
            <option value="1">${data.name}</option>
            `
        });
        document.getElementById('genreSelect').innerHTML = `<option selected>Choose...</option>` + genreTemplate;
    }

    //Publication DropDown
    const FilterDropPublication = async () => {
        const FetchPublication = await fetch(`http://localhost:5000/api/Publication/GetAllPublication`);
        const PublicationName = await FetchPublication.json();
        let publicationTemplate = '';
        PublicationName.forEach((data) => {
            publicationTemplate += ` <option value="1">${data.name}</option>`
        })
        document.getElementById('publicationSelect').innerHTML = `<option selected>Choose...</option>` + publicationTemplate;
    };
    //Author DropDown
    const FilterDropAuthor = async () => {
        const FetchAuther = await fetch(`http://localhost:5000/api/Aurthor/GetAllAuthor`);
        const AuthorName = await FetchAuther.json();
        let AuthorTemplate = '';
        AuthorName.forEach((data) => {
            AuthorTemplate += `<option value="1">${data.name}</option>`
        });
        document.getElementById('authorSelect').innerHTML = `<option selected>Choose...</option>` + AuthorTemplate;
    }

    //Model window work
    const openModelWindow = () => {
        document.getElementById('UppendBooks').addEventListener('click', function(event) {
            if (event.target.classList.contains('btn-primary')) {
                const bookId = event.target.getAttribute('data-id');
                var bookModal = new bootstrap.Modal(document.getElementById('bookModal'));
                bookModal.show();

                console.log(bookId)
            }
        });
    }

    PrintBookData();
    FilterDropGenre();   
    FilterDropPublication();
    FilterDropAuthor();
    openModelWindow();
})