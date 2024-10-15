const loggedUser = localStorage.getItem("loggedUSer");
if (loggedUser) {
  document.addEventListener("DOMContentLoaded", async () => {
    //print user name
    const GetUserDetails = await fetch(`http://localhost:5000/api/User/GetUserDetailsUsingID?NIC=${loggedUser}`);
    const UserDetails = await GetUserDetails.json();
    const userName = document.querySelector('.username');
    userName.innerHTML =`${UserDetails.firstName} ${UserDetails.lastName}` 


    // Update books
    const PrintBookData = async () => {
      const FetchAllBookData = await fetch(
        `http://localhost:5000/api/Book/GetAllBooks`
      );
      const AllBookData = await FetchAllBookData.json();

      let bookDataTemplate = "";
      for (const data of AllBookData) {
        // Get the first image
        const AllImages = data.image.split(",").map((img) => img.trim());
        const firstImage = AllImages[0];
        const Image = `http://localhost:5000${firstImage}`.trim();

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

        bookDataTemplate += `
            <div class="col">
                <div class="card h-100">
                    <div class="product-image-container">
                        <img src="${Image}" class="card-img-top product-image" alt="Product Image">
                    </div>
                    <div class="card-body" id="gridContent"> 
                        <h5 class="card-title limit-text-to-2-lines">${data.name}</h5>
                        <p class="card-text">
                            <strong>Author:</strong> ${AuthorName.name}<br>
                            <strong>Genre:</strong> ${Genre.name}<br>
                            <strong>Publication:</strong> ${Publication.name}
                        </p>
                        <button type="button" class="btn btn-primary" data-id="${data.id}">View More</button>
                    </div>
                </div>
            </div>`;

        document.getElementById("UppendBooks").innerHTML = bookDataTemplate;
      }
    };

    

    // Model window work
    const openModelWindow = () => {
      document
        .getElementById("UppendBooks")
        .addEventListener("click", async (event) => {
          if (event.target.classList.contains("btn-primary")) {
            const bookId = event.target.getAttribute("data-id");
            const GetBookData = await fetch(
              `http://localhost:5000/api/Book/GetById?id=${bookId}`
            );
            const bookDetails = await GetBookData.json();

            // Fetch related data
            const FetchAuthorName = await fetch(
              `http://localhost:5000/api/Aurthor/GetByID?Id=${bookDetails.authorId}`
            );
            const Author = await FetchAuthorName.json();
            const FetchGenreName = await fetch(
              `http://localhost:5000/api/GenreControler/GetById?id=${bookDetails.genreId}`
            );
            const Genre = await FetchGenreName.json();
            const FetchPublicationName = await fetch(
              `http://localhost:5000/api/Publication/GetByID?id=${bookDetails.publicationId}`
            );
            const Publication = await FetchPublicationName.json();
            const AuthorImage = `http://localhost:5000${Author.image}`.trim();

            // Update modal content
            document.getElementById(
              "ModelWindowBookName"
            ).innerHTML = `<strong>${bookDetails.name}</strong>`;
            document.getElementById(
              "ModelWindowAuthorName"
            ).innerHTML = `<strong>Author:</strong> ${Author.name}`;
            document.getElementById(
              "ModelwinodwGenreName"
            ).innerHTML = `<strong>Genre:</strong> ${Genre.name}`;
            document.getElementById(
              "ModelWindowPublicationName"
            ).innerHTML = `<strong>Publication:</strong> ${Publication.name}`;
            document.getElementById(
              "AuthorImage"
            ).innerHTML = `<img src="${AuthorImage}" class="rounded-circle" alt="Author Image" width="250" height="250">`;

            // Update book images in carousel
            const BookImages = bookDetails.image.split(",");
            let imageTemplate = "";
            BookImages.forEach((URL, index) => {
              const imageUrl = `http://localhost:5000${URL}`.trim();
              const activeClass = index === 0 ? "active" : "";
              imageTemplate += `
                        <div class="carousel-item ${activeClass}">
                            <img src="${imageUrl}" class="d-block w-100" alt="Book Cover ${index + 1
                }">
                        </div>`;
            });
            document.querySelector(".bookCoverShow").innerHTML = imageTemplate;

            // Show the modal
            const bookModal = new bootstrap.Modal(
              document.getElementById("bookModal")
            );
            bookModal.show();

            const ConformBtn = document.getElementById("conformRequest");
            ConformBtn.removeEventListener("click", handleConformClick);
            ConformBtn.addEventListener("click", handleConformClick);

            //GET logged user info
            const GetUserDetails = await fetch(`http://localhost:5000/api/User/GetUserDetailsUsingID?NIC=${loggedUser}`);
            const UserDetails = await GetUserDetails.json();
            async function handleConformClick() {
              const req = {
                userId: UserDetails.userId,
                bookId: bookDetails.id
              }
              //post book request 
              console.log(req);
              const requestBook = await fetch(`http://localhost:5000/api/BookTransaction/RequestBook`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(req),

              })

              if (requestBook) {
                window.location.reload()
              }
            }

          }
        });
    };

    // Initial function calls
    PrintBookData();
    openModelWindow();
  });
} else {
  alert("please log in");

}
