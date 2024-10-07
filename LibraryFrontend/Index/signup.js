import {addUser} from './api.js'

let register = document.getElementById("register");

register.onclick = function(){
    let firstName = document.getElementById("firstName").value;
    let lastName = document.getElementById("lastName").value;
    let nicNumber = document.getElementById("nicNumber").value;
    let email = document.getElementById("email").value;
    let phoneNumber = document.getElementById("phoneNumber").value;
    let password = document.getElementById("password").value;

    let singleUser = {fname:firstName, lname:lastName, nic:nicNumber, mail:email, tpnumber:phoneNumber, pw:password}
    addUser(singleUser);

}