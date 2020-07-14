import React, { useState } from "react"
import {Form, Label, Col, Input, Button} from "reactstrap"
import "../styles/componentsstyle.css"

export default function Login(){

    const [Login, setLogin]=useState({
      email: '',
      password: ''
    })

    const handleFormInput = e =>{
      setLogin({
        ...Login,
        [e.target.name]: e.target.value
      })
    }

    const sendLoginData = e=>{
      console.log(Login)
    }

    return (
        <Form onSubmit={sendLoginData}>
        <h4>Login</h4>
          <Label for="Username">Email</Label>
          <Col>
            <Input type="email" name="email" id="exampleEmail" placeholder="email" onChange={handleFormInput} value={Login.email} />
          </Col>
          <Label for="examplePassword">Password</Label>
          <Col>
            <Input type="password" name="password" placeholder="password" onChange={handleFormInput} value={Login.password}/>
          </Col>
        <Button color="primary" onClick={sendLoginData}>Sign in</Button>
        </Form>
    )
}