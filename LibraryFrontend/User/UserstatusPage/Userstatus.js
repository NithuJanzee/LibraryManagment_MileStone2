document.addEventListener('DOMContentLoaded' , async(event)=>{
    event.preventDefault();
    const loggedUser = localStorage.getItem("loggedUSer");
    if(loggedUser)
    {
        const GetUserDetails = await fetch(`http://localhost:5000/api/User/GetUserDetailsUsingID?NIC=${loggedUser}`);
        const UserDetails = await GetUserDetails.json();
        //console.log(UserDetails.userId)

        const GetUserHistory = async ()=>{
            const UserHistory = await fetch(`http://localhost:5000/api/History/GetByUserId?Id=${UserDetails.userId}`)
            const History = await UserHistory.json();
            //console.log(History);

            //Print in table 
            let HistoryTemplate = '';
            for (const data of History) {
              const GetUserData = await fetch(`http://localhost:5000/api/User/UserDetailsGUID?ID=${data.userId}`)
              const User = await GetUserData.json()
             
              const GetBookData = await fetch(`http://localhost:5000/api/Book/GetById?id=${data.bookId}`)
              const Book = await GetBookData.json()
    
              const RequestDate = new Date(data.requestedDate).toLocaleDateString("en-CA");
              const lendedDate = data.lendedDate ? new Date(data.lendedDate).toLocaleDateString("en-CA") : "N/A";
              const dueDate = data.dueDate ? new Date(data.dueDate).toLocaleDateString("en-CA") : "N/A";
              const returnedDate = data.returnedDate ? new Date(data.returnedDate).toLocaleDateString("en-CA") : "N/A";
              
              HistoryTemplate+=` 
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
            document.getElementById('TableHistoryData').innerHTML=HistoryTemplate;
        }
        GetUserHistory();
    }else{
        alert('please log in')
    }
})