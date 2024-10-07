
const messages = document.getElementById("ThrowMessages");
//SignUp
const signUpButton = document.getElementById("signUpBtn");
signUpButton.addEventListener("click", async (event) => {
  event.preventDefault();
  const firstName = document.getElementById("firstName").value;
  const lastName = document.getElementById("lastName").value;
  const nic = document.getElementById("nicSignUp").value;
  const email = document.getElementById("Email").value;
  const phoneNumber = document.getElementById("phone").value;
  const password = document.getElementById("passwordSignUp").value;
  const confirmPassword = document.getElementById("confirmPassword").value;

  const User = {
    firstName: firstName,
    lastName: lastName,
    nic: nic,
    email: email,
    phoneNumber: phoneNumber,
    password: password,
  };

  if (password !== confirmPassword) {
    messages.innerHTML = '<p id="Bmessage">password is not matching</p>';
    return;
  }
  // Post user function
  const PostUser = async (User) => {
    const response = await fetch(`http://localhost:5000/api/User/AddUsers`, {
      method: "POST",
      headers: { "Content-Type": "application/json" }, 
      body: JSON.stringify(User),
    });
    return response;
  };
  const theResponse = await PostUser(User);

  if (theResponse.ok) {
    messages.innerHTML = '<p class="Gmessage">User created successfully! Please signIn</p>';

  } else {
    messages.innerHTML = '<p id="Bmessage">Nic Already exits!</p>';
  }
});
