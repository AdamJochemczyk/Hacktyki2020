import React from "react";
import { Button, Label, Input, FormGroup, Col } from "reactstrap";
import "../styles/componentsstyle.css";
import axios from "axios";
import { Formik, Field, Form, ErrorMessage } from "formik";
import * as Yup from "yup";

export default function AddUser() {

  return (
    <Formik
      initialValues={{
        FirstName: "",
        LastName: "",
        NumberIdentificate: "",
        Email: "",
        MobileNumber: "",
      }}
      validationSchema={Yup.object().shape({
        FirstName: Yup.string()
          .min(3, "Too short!")
          .max(20, "Too long!")
          .required("Required"),
        LastName: Yup.string()
          .min(3, "Too short!")
          .max(20, "Too long!")
          .required("Required"),
        NumberIdentificate: Yup.string()
          .min(6, "Too short!")
          .max(15, "Too long!")
          .required("Required"),
        Email: Yup.string().email("Invalid email").required("Required"),
        MobileNumber: Yup.string()
          .min(9, "Must contains at least 9 numers")
          .max(15, "Too long number")
          .required("Required"),
      })}
      onSubmit={(values) => {
        console.log(values);
        axios({
          url: "https://localhost:44390/api/authorization",
          method: "POST",
          data: values,
        });
      }}
    >
      {({ errors, touched }) => (
        <Form>
        <h1>Add user</h1>
        <FormGroup row>
        <Col sm={12}>
        <Label>First Name</Label>
          <Input type="text" name="FirstName" tag={Field}/>
          <ErrorMessage name="FirstName" component="div" />
          <Label>Last Name</Label>
          <Input type="text" name="LastName" tag={Field}/>
          <ErrorMessage name="LastName" component="div"/>
          <Label>Identification number</Label>
          <Input type="text" name="NumberIdentificate" tag={Field}/>
          <ErrorMessage name="NumberIdentificate" component="div"/>
          <Label>Email</Label>
          <Input type="text" name="Email" tag={Field}/>
          <ErrorMessage name="Email" component="div"/>
          <Label>Phone number</Label>
          <Input type="text" name="MobileNumber"tag={Field} />
          <ErrorMessage name="MobileNumber" component="div"/>
          <Input type="submit" color="success" tag={Button}>
        Add user
      </Input>
      </Col>
          </FormGroup>
        </Form>
      )}
    </Formik>
  );
}
