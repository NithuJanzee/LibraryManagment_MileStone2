const messages = document.getElementById("ThrowMessages");
const messages1 = document.getElementById('ThrowMessages1');
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
    setTimeout(() => {
      closeSignUp(()=>{
        openSignIn();
      })
    }, 1000)
    messages.innerHTML = '<p class="Gmessage">User created successfully! Please signIn</p>';
    

  } else {
    messages.innerHTML = '<p id="Bmessage">Nic Already exits!</p>';
  }

  function openSignIn() {
    var signInModal = new bootstrap.Modal(document.getElementById('signInModal'));
    signInModal.show();
  }

  function closeSignUp(callback) {
    var signUpModal = bootstrap.Modal.getInstance(document.getElementById('signUpModal'));
    if (signUpModal) {
        signUpModal.hide(); 
        if (callback && typeof callback === 'function') {
            callback(); 
        }
    }
}

});


const SignInButton = document.getElementById('SignInBtn');
SignInButton.addEventListener('click', async (event) => {
  event.preventDefault();
  const nic = document.getElementById('nicSignIn').value;
  const password = document.getElementById('passwordSignIn').value;

  const login = {
    nic: nic,
    password: password
  }

  const CheckUser = async () => {
    const postData = await fetch(`http://localhost:5000/api/User/UserLogin`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(login),

    })
    return postData;
  }

  const response = await CheckUser();
  const bool = await response.json();

  if (bool == true) {
    messages1.innerHTML = '<p class="Gmessage">LogIn successful</p>';
  }
  else {
    messages1.innerHTML = '<p id="Bmessage">Please Check your Nic and Password</p>';
  }
})