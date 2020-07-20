import React, { useState } from "react"
import {Form, Input, Button, Label} from "reactstrap"
import "../styles/componentsstyle.css"
import axios from "axios"

export default function AddUser(){

    const [user, setUser] = useState({
        firstName: '',
        lastName: '',
        email: '',
        phone: '',
    })
    
    const handleFormInput = e => {
        setUser({
            ...user,
            [e.target.name]: e.target.value
        })
    }
    async function AddUser(){
        console.log(user)
       await axios({
            url: "https://localhost:44390/api/users/",
            method: "POST",
            data: {user}
          }).then((res) => {
            if(res.status===200){
              alert("User added")
              window.location.reload(false)
            }
            }).catch(error => {
              console.log(error.response)
          });
    }

    return (
    <Form>
        <Label>Username</Label>
        <Input type="text" name="username" placeholder="username" onChange={handleFormInput} value={user.username}/>
        <Label>Surname</Label>
        <Input type="text" name="surname" placeholder="surname" onChange={handleFormInput} value={user.surname}/>
        <Label>Email</Label>
        <Input type="mail" name="email" placeholder="email" onChange={handleFormInput} value={user.email}/>
        <Label>Phone</Label>
        <Input type="number" name="phone" placeholder="phone" onChange={handleFormInput} value={user.phone}/>
        <Button color="success" onClick={AddUser}>Add user</Button>
    </Form>
    )
}