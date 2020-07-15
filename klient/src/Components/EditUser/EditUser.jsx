import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Form, Input, Label, Button } from "reactstrap";
import {Link} from "react-router-dom"
import axios from "axios";

export default function EditUser() {
  let { id } = useParams();

  const [user, setUser] = useState({
    userId: id,
    firstName: '',
    lastName: '',
    mobileNumber: '',
    email: '',
  })
  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const response = await axios.get("https://localhost:44390/api/users/"+id);
        setUser(response.data);
      } catch (e) {
        console.log(e);
      }
    };
    fetchUsers();
  }, []);

  const handleFormInput = e => {
        setUser({
            ...user,
            [e.target.name]: e.target.value
        })}

  async function editUser() {
    await axios({
      url: "https://localhost:44390/api/users/"+user.userId,
      method: "PUT",
      data: {user}
    });
  }

  return (
    <Form>
      <h1>Edit user</h1>
      <Label>First name</Label>
      <Input type="text" name="firstName" value={user.firstName} onChange={handleFormInput} />
      <Label>Last name</Label>
      <Input type="text" name="lastName" value={user.lastName} onChange={handleFormInput}/>
      <Label>Phone number</Label>
      <Input type="number" name="mobileNumber" value={user.mobileNumber} onChange={handleFormInput}/>
      <Label>Mail</Label>
      <Input type="mail" name="email" value={user.email} onChange={handleFormInput}/>
      <Link to="/UserManager">Back</Link>
      <Button color="success" onClick={editUser}>Edit</Button>
    </Form>
  );
}
