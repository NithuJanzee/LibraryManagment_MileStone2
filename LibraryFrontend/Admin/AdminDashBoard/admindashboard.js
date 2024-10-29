document.addEventListener("DOMContentLoaded", async (event) => {
  event.preventDefault();

  //add total book count
  const GetAllBookCount = async () => {
    const GetAllData = await fetch(
      `http://localhost:5000/api/Book/GetAllBooks`
    );
    const allBooks = await GetAllData.json();
    const totalBooks = allBooks.length;
    document.getElementById("totalBooks").innerHTML = totalBooks;
  };
  const GetAllUsersCount = async () => {
    const GetAllUsers = await fetch(
      `http://localhost:5000/api/User/GetAllUsers`
    );
    const AllUsers = await GetAllUsers.json();
    const totalUsers = AllUsers.length;
    document.getElementById("totalUsers").innerHTML = totalUsers;
  };
  const GetAllPendingRequest = async () => {
    //add collection of genre author and publication
  };
  const GetAllLendingBooks = async () => {
    const AllLendBooks = await fetch(
      `http://localhost:5000/api/BookTransaction/GetAllLending`
    );
    const LendBooks = await AllLendBooks.json();
    const countLendBooks = await LendBooks.length;
    document.getElementById("totalLendingBooks").innerHTML = countLendBooks;
  };

  //L    
  const LendingTable = async () => {
    const fetchAllLendingRequest = await fetch(
      `http://localhost:5000/api/BookTransaction/AllRequestedData`
    );
    const LendingRequest = await fetchAllLendingRequest.json();

    let TableBodyTemplate = "";
    for (const Request of LendingRequest) {
      const UserDetails = await fetch(
        `http://localhost:5000/api/User/UserDetailsGUID?ID=${Request.userId}`
      );
      const userName = await UserDetails.json();

      const BookDetails = await fetch(
        `http://localhost:5000/api/Book/GetById?id=${Request.bookId}`
      );
      const Book = await BookDetails.json();

      TableBodyTemplate += `
            <tr>
                <td>${userName.firstName} ${userName.lastName}</td>
                <td>${Book.name}</td>
                <td>${Book.copies}</td>
                <td>
                    <button type="button" class="acceptBtn btn btn-success" data-id="${Request.transactionId}"
                    data-UserId="${Request.userId}"
                    data-BookId="${Request.bookId}">Accept</button>
                </td>
            </tr>
            `;
    }
    document.getElementById("requestTableBody").innerHTML = TableBodyTemplate;

    const AcceptBtn = document.querySelectorAll(".acceptBtn");
    AcceptBtn.forEach((button) => {
      button.addEventListener("click", async (data) => {
        openModal();
        const TransactionID = data.target.getAttribute("data-id");
        const UserId = data.target.getAttribute("data-UserId");
        const BookId = data.target.getAttribute("data-BookId");
        document
          .getElementById("confirmAccept")
          .addEventListener("click", async () => {
            //Update requested to lending
            const postTransaction = await fetch(
              `http://localhost:5000/api/BookTransaction/UpdateToLending?TransactionId=${TransactionID}`,
              {
                method: "PUT",
                headers: {
                  "Content-Type": "application/json",
                },
              }
            );
            //Decrease Quantity
            const DecreaseQuantity = await fetch(
              `http://localhost:5000/api/BookTransaction/decrementCopies?bookId=${BookId}`,
              {
                method: "PUT",
                headers: {
                  "Content-Type": "application/json",
                },
              }
            );
            //Update History Table
            const UpdateTransaction = await fetch(
              `http://localhost:5000/api/History/UpdateLending?UserId=${UserId}&BookId=${BookId}`,
              {
                method: "PUT",
                headers: {
                  "Content-Type": "application/json",
                },
              }
            );
            if (UpdateTransaction) {
              window.location.reload();
            }
          });
      });
    });
  };

  function openModal() {
    const confirmationModal = new bootstrap.Modal(
      document.getElementById("confirmationModal")
    );
    confirmationModal.show();
  }

  //Open total book modal
  const TotalBookData = async () => {
    const TotalBookBtn = document.getElementById("Show1");
    TotalBookBtn.addEventListener("click", async () => {
      const GetAllBooks = await fetch(
        `http://localhost:5000/api/Book/GetAllBooks`
      );
      const AllBooks = await GetAllBooks.json();

      let AllBookTemplate = "";
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
                `;
      }
      document.getElementById("AllbookListBody").innerHTML = AllBookTemplate;
    });
  };

  //open all user Model Window
  const TotalUserData = async () => {
    const totalUserBtn = document.getElementById("Show2");
    totalUserBtn.addEventListener("click", async () => {
      const FetchAllUserData = await fetch(
        `http://localhost:5000/api/User/GetAllUsers`
      );
      const AllUsers = await FetchAllUserData.json();

      let AllUserTemplate = "";
      AllUsers.forEach((data) => {
        AllUserTemplate += `
                <tr>
                            <td>${data.firstName}</td>
                            <td>${data.lastName}</td>
                            <td>${data.nic}</td>
                            <td>${data.email}</td>
                            <td>${data.phoneNumber}</td>
                        </tr>
                `;
      });
      document.getElementById("AlluserListBody").innerHTML = AllUserTemplate;
    });
  };
  //Open total lending Books
  const TotalLendingBooks = async () => {
    const TotalLendingBtn = document.getElementById("Show4");
    TotalLendingBtn.addEventListener("click", async () => {
      const GetAllLendingBookData = await fetch(
        `http://localhost:5000/api/BookTransaction/GetAllLending`
      );
      const AllLending = await GetAllLendingBookData.json();
      //console.log(AllLending);

      let AllLendingBookTemplate = "";
      for (const data of AllLending) {
        //Book Data
        const GetBookData = await fetch(
          `http://localhost:5000/api/Book/GetById?id=${data.bookId}`
        );
        const bookData = await GetBookData.json();
        //User data
        const GetUserData = await fetch(
          `http://localhost:5000/api/User/UserDetailsGUID?ID=${data.userId}`
        );
        const UserData = await GetUserData.json();
        // lend and return date
        const lendingDate = new Date(data.lendingDate).toLocaleDateString(
          "en-CA"
        );
        const DueDate = new Date(data.returnDate).toLocaleDateString("en-CA");

        AllLendingBookTemplate += `
                    <tr>
                    <td>${bookData.name}</td>
                    <td>${UserData.firstName} ${UserData.lastName}</td>
                    <td>${lendingDate}</td>
                    <td>${DueDate}</td>
                    </tr>
                `;
      }
      document.getElementById("AlllendingListBody").innerHTML =
        AllLendingBookTemplate;
    });
  };
  //Add book data
  //Add genre
  const AddNewGenre = () => {
    const addNewGenreBtn = document.getElementById("SaveGenreBtn");
    addNewGenreBtn.addEventListener("click", async () => {
      const NewGenre = document.getElementById("newGenre").value;
      const PostGenre = await fetch(
        `http://localhost:5000/api/GenreControler/AddGenre`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ name: NewGenre }),
        }
      );
      if (PostGenre) {
        const modal = bootstrap.Modal.getInstance(
          document.getElementById("addGenreModal")
        );
        modal.hide();
        setTimeout(() => {
          window.location.reload();
        }, 500);
      }
    });
  };
  //add publication
  const AddNewPublication = () => {
    const AddPublicationBtn = document.getElementById("AddPublicationBtn");
    AddPublicationBtn.addEventListener("click", async () => {
      const PublicationName = document.getElementById("newPublication").value;
      const postPublication = await fetch(
        `http://localhost:5000/api/Publication/PostPublication`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ name: PublicationName }),
        }
      );

      if (postPublication) {
        const modal = bootstrap.Modal.getInstance(
          document.getElementById("addPublishDateModal")
        );
        modal.hide();
        setTimeout(() => {
          window.location.reload();
        }, 500);
      }
    });
  };
  //Add new author
  const AddNewAuthor = () => {
    const AddAuthorBtn = document.getElementById("AddAuthorBtn");
    AddAuthorBtn.addEventListener("click", async () => {
      // Get form inputs
      const authorName = document.getElementById("authorName").value;
      const authorPhoto = document.getElementById("authorPhoto").files[0];
      const formData = new FormData();
      formData.append("image", authorPhoto);
      formData.append("name", authorName);
      const url = `http://localhost:5000/api/Aurthor/AddAuthor?Name=${encodeURIComponent(
        authorName
      )}`;

      const response = await fetch(url, {
        method: "POST",
        body: formData,
      });
      const modal = bootstrap.Modal.getInstance(
        document.getElementById("addAuthorModal")
      );
      modal.hide();
      setTimeout(() => {
        window.location.reload();
      }, 500);
    });

    //set add book
    const SetBookModel = async () => {
      //add author drop down
      const AllAuthorData = await fetch(
        `http://localhost:5000/api/Aurthor/GetAllAuthor`
      );
      const AllAuthor = await AllAuthorData.json();
      let AuthorTemplate = '';
      AllAuthor.forEach((data) => {
        AuthorTemplate += `
        <option value="${data.id}">${data.name}</option>
        `
      })
      document.getElementById('authorSelect').innerHTML = '<option selected disabled value="">Select Author</option>' + AuthorTemplate;

      //add genre drop down
      const allGenreData = await fetch(`http://localhost:5000/api/GenreControler/GetAllGenre`);
      const allGenre = await allGenreData.json();
      let GenreTemplate = ``
      allGenre.forEach((data) => {
        GenreTemplate += `
        <option value="${data.id}">${data.name}</option>
        `
      })
      document.getElementById('genre').innerHTML = ` <option selected disabled value="">Select Genre</option>` + GenreTemplate

      //Add publication drop down
      const AllPublicationData = await fetch(`http://localhost:5000/api/Publication/GetAllPublication`);
      const AllPublication = await AllPublicationData.json()
      let PublicationTemplate = '';
      AllPublication.forEach((data) => {
        PublicationTemplate += `
        <option value="${data.id}">${data.name}</option>
        `
      })
      document.getElementById('publishPublication').innerHTML = `<option selected disabled value="">Select Publish Date</option>` + PublicationTemplate;
    };

    const AddNewBook = async () => {
      const AddNewBookBtn = document.getElementById('AddNewBookBtn');
      AddNewBookBtn.addEventListener('click', async () => {
        const BookTitle = document.getElementById('bookTitle').value;
        const Quantity = document.getElementById('quantity').value;
        const Author = document.getElementById('authorSelect').value;
        const Genre = document.getElementById('genre').value;
        const Publication = document.getElementById('publishPublication').value;
        const bookPhotos = document.getElementById('BookPhotos').files;

        const formData = new FormData();
        formData.append('Name', BookTitle);
        formData.append('copies', Quantity);
        formData.append('AuthorId', Author);
        formData.append('GenreId', Genre);
        formData.append('PublicationId', Publication);

        for (let i = 0; i < bookPhotos.length; i++) {
          formData.append('Image', bookPhotos[i]);
        }
        const response = await fetch('http://localhost:5000/api/Book/Addbook', {
          method: 'POST',
          body: formData,
        });
        if (response) {
          window.location.reload();
        }
      });
    };


    //Manage Return Process
    const BookReturnManagement = () => {
      const ManageReturnBtn = document.getElementById('ManageReturnBtn');
      ManageReturnBtn.addEventListener('click', async () => {
        const GetAllLendingData = await fetch(`http://localhost:5000/api/BookTransaction/GetAllLending`);
        const LendingData = await GetAllLendingData.json();

        let ReturnManagementTableTemplate = '';
        for (const data of LendingData) {
          const UserDetails = await fetch(`http://localhost:5000/api/User/UserDetailsGUID?ID=${data.userId}`);
          const User = await UserDetails.json();

          const BookDetails = await fetch(`http://localhost:5000/api/Book/GetById?id=${data.bookId}`);
          const Book = await BookDetails.json();

          const lendingDate = new Date(data.lendingDate).toLocaleDateString("en-CA");
          const DueDate = new Date(data.returnDate).toLocaleDateString("en-CA");

          const CheckDueStatus = async () => {
            const checkStatus = await fetch(`http://localhost:5000/api/BookTransaction/CheckTheOverDue?transactionID=${data.transactionId}`, {
              method: "POST",
              headers: {
                "Content-Type": "application/json",
              }
            });
            return checkStatus;
          };

          const StatusResponse = await CheckDueStatus();
          const StatusDue = await StatusResponse.json();

          const statusButton = StatusDue == true
            ? `<button type="button" class="btn btn-success" 
            data-tId="${data.transactionId}"
            data-BId="${data.bookId}"
            data-UId="${data.userId}"
            data-Status="${'returned-onTime'}"
            >In-time</button>`
            : `<button type="button" class="btn btn-danger" 
            data-tId="${data.transactionId}"
            data-BId="${data.bookId}"
            data-UId="${data.userId}"
            data-Status="${'returned-overdue'}"
            >OverDue</button>`;

          ReturnManagementTableTemplate += `
            <tr>
              <td>${User.firstName} ${User.lastName}</td>
              <td>${Book.name}</td>
              <td>${lendingDate}</td>
              <td>${DueDate}</td>
              <td class="statusBtn">${statusButton}</td>
            </tr>
          `;
        }

        document.getElementById('returnsListBody').innerHTML = ReturnManagementTableTemplate;
        const statusBtn = document.querySelectorAll('.statusBtn');
        statusBtn.forEach((button) => {
          button.addEventListener('click', async (event) => {
            const TransactionID = event.target.getAttribute('data-tId');
            const BookId = event.target.getAttribute('data-BId');
            const UserId = event.target.getAttribute('data-UId');
            const Status = event.target.getAttribute('data-Status');

            const IncreaseBookQuantity = await fetch(`http://localhost:5000/api/BookTransaction/increaseQuantityByOne?BookId=${BookId}`, {
              method: "PUT",
              headers: {
                "Content-Type": "application/json",
              },
            })
            if (IncreaseBookQuantity) {
              const UpdateHistoryTable = await fetch(`http://localhost:5000/api/History/return?userId=${UserId}&bookId=${BookId}&status=${Status}`, {
                method: "PUT",
                headers: {
                  "Content-Type": "application/json",
                },
              })
              if (UpdateHistoryTable) {
                const DeleteTransaction = await fetch(`http://localhost:5000/api/BookTransaction/ReturnDelete?ID=${TransactionID}`, {
                  method: "DELETE", 
                  headers: {
                    "Content-Type": "application/json",
                  },
                })
                if (DeleteTransaction) {
                  window.location.reload()
                }
              }
            }
          });
        })

      });
      //end book return management
    };

    //Reports model
    const ReportsModel = ()=>{
      const ReportBtn = document.getElementById('ManageReportBtn');
      ReportBtn.addEventListener('click' , async()=>{
        const GetAllReportData = await fetch (`http://localhost:5000/api/History/GetAll`);
        const GetAllReport = await GetAllReportData.json();
        console.log(GetAllReport)
        
        let ReportTemplate = '';
        for (const data of GetAllReport) {
          const GetUserData = await fetch(`http://localhost:5000/api/User/UserDetailsGUID?ID=${data.userId}`)
          const User = await GetUserData.json()
         
          const GetBookData = await fetch(`http://localhost:5000/api/Book/GetById?id=${data.bookId}`)
          const Book = await GetBookData.json()

          const RequestDate = new Date(data.requestedDate).toLocaleDateString("en-CA");
          const lendedDate = data.lendedDate ? new Date(data.lendedDate).toLocaleDateString("en-CA") : "N/A";
          const dueDate = data.dueDate ? new Date(data.dueDate).toLocaleDateString("en-CA") : "N/A";
          const returnedDate = data.returnedDate ? new Date(data.returnedDate).toLocaleDateString("en-CA") : "N/A";
          
          ReportTemplate+=` 
                            <tr>
                                <td>${User.firstName} ${User.lastName}</td>
                                <td>${Book.name}</td>
                                <td>${RequestDate}</td>
                                <td>${lendedDate}</td>
                                <td>${dueDate}</td>
                                <td>${returnedDate}</td>
                                <td>${data.status}</td>
                            </tr>
          `
        } 
        document.getElementById('reportsListBody').innerHTML = ReportTemplate;
      })
    }


    SetBookModel();
    AddNewBook();
    BookReturnManagement()
    ReportsModel()
  };

  function openManageBookModal() {
    const manageBookModal = new bootstrap.Modal(
      document.getElementById("manageBookModal")
    );
    manageBookModal.show();
  }

  function openModal2(modalId) {
    const modal = new bootstrap.Modal(document.getElementById(modalId));
    modal.show();
  }

  GetAllBookCount();
  GetAllUsersCount();
  GetAllPendingRequest();
  GetAllLendingBooks();
  LendingTable();
  TotalBookData();
  TotalUserData();
  TotalLendingBooks();
  AddNewGenre();
  AddNewPublication();
  AddNewAuthor();
});
