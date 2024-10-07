export async function addUser(obj){
    await fetch('http://localhost:3000/user',{
      method:'POST',
      headers: {'Content-Type': 'application/json'},
      body: JSON.stringify({"firstname":obj.fname, "lastname":obj.lname, "nic":obj.nic, "email":obj.mail, "phone":obj.tpnumber, "password":obj.pw})
    })
    
  }