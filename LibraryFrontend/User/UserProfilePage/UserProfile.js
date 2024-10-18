document.addEventListener('DOMContentLoaded', async () => {
    const loggedUser = localStorage.getItem("loggedUSer");
    if (loggedUser) {
        const GetUserDetails = await fetch(`http://localhost:5000/api/User/GetUserDetailsUsingID?NIC=${loggedUser}`);
        const UserDetails = await GetUserDetails.json();

        // Update current profile information
        const firstName= document.getElementById('firstname').value = UserDetails.firstName;
        const lastName= document.getElementById('lastname').value = UserDetails.lastName;
        const Nic= document.getElementById('nic').value = UserDetails.nic;
        const email = document.getElementById('email').value = UserDetails.email;
        const phoneNumber= document.getElementById('phone').value = UserDetails.phoneNumber;

        //edit modal
        const SaveChangesBtn = document.getElementById('SaveChanges');
        SaveChangesBtn.addEventListener('click', async () => {
            const updateFirstname = document.getElementById('updateFirstname').value;
            const updateLastname = document.getElementById('updateLastname').value;
            const updateEmail = document.getElementById('updateEmail').value;
            const updatePhone = document.getElementById('updatePhone').value;
            const updatedPassword = document.getElementById('updatedPassword').value;

            const Update = {
                firstName: updateFirstname,
                lastName: updateLastname,
                email: updateEmail,
                phoneNumber: updatePhone,
                password: updatedPassword
            };


            const postUpdatedUser = await fetch(`http://localhost:5000/api/User/${UserDetails.userId}`, {
                method: "PUT",  
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(Update),
            });

            if (postUpdatedUser.ok) {
                window.location.reload();
            } else {
                alert('Failed to update user information.');
            }
        });
    } else {
        alert('Please log in');
    }
});
