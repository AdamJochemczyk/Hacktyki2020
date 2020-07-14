import React, { useState } from "react"
import {Form, Input, Button, Label} from "reactstrap"
import "../styles/componentsstyle.css"

export default function AddUser(){
    
    const [User, setUser] = useState({
        username: '',
        surname: '',
        email: '',
        phone: ''
    })
    
    const handleFormInput = e => {
        setUser({
            ...User,
            [e.target.name]: e.target.value
        })
    }
    const AddUser = e =>{
        e.preventDefault()
        console.log(User)
        //send data to API

        //after send alert and reload
    }

    return (
    <Form onSubmit={AddUser}>
    <h4>Add User</h4>
        <Label>Username</Label>
        <Input type="text" name="username" placeholder="username" onChange={handleFormInput} value={User.username}/>
        <Label>Surname</Label>
        <Input type="text" name="surname" placeholder="surname" onChange={handleFormInput} value={User.surname}/>
        <Label>Email</Label>
        <Input type="mail" name="email" placeholder="email" onChange={handleFormInput} value={User.email}/>
        <Label>Phone</Label>
        <Input type="number" name="phone" placeholder="phone" onChange={handleFormInput} value={User.phone}/>
        <Button color="success" type="submit" onClick={AddUser}>Add user</Button>
    </Form>
    )
}