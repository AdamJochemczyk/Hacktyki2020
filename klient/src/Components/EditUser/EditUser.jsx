import React, { useEffect, useState } from "react";
import { Link, useParams, useHistory } from "react-router-dom";
import { Formik, Field, Form, ErrorMessage } from "formik";
import * as Yup from "yup";
import axios from "axios";
import Swal from 'sweetalert2'

export default function EditUser() {
  const { id } = useParams();
  const isAddMode = !id;
  let history=useHistory()

  let initialValues = {
    firstName: "",
    lastName: "",
    email: "",
    numberIdentificate: "",
    mobileNumber: "",
  };

  const validationSchema = Yup.object().shape({
    firstName: Yup.string()
      .min(3, "Too short!")
      .max(20, "Too long!")
      .required("Required"),
    lastName: Yup.string()
      .min(3, "Too short!")
      .max(20, "Too long!")
      .required("Required"),
    numberIdentificate: Yup.string()
      .min(6)
      .max(6, "Too long!")
      .required("Required"),
    email: Yup.string().email("Invalid email").required("Required"),
    mobileNumber: Yup.string()
      .min(9, "Must contains at least 9 numers")
      .max(15, "Too long number")
      .required("Required"),
  });

  function onSubmit(fields, {setSubmitting}) {
    if (isAddMode) {
      createUser(fields, setSubmitting);
    } else {
      updateUser(id, fields, setSubmitting);
    }
  }

  function createUser(fields, setSubmitting) {
    try{
      axios({
        url: "https://localhost:44390/api/authorization",
        method: "POST",
        data: fields,
      }).catch((error) =>{
        if (error.response) {
          Swal.fire("Oops...", error.response.headers, "error")
        } else if (error.request) {
          Swal.fire("Oops...", "Error request was made but no response was recived. Error request: "+error.request, 'error');
        } else {
          Swal.fire("Oops...", "Something happened in setting up the request that triggered an Error. Error massage: "+error.message, 'error');
        }
        setSubmitting(false);
        history.goBack()
      });
      Swal.fire("Good job!", 'You succesfully added new user!', 'success')
      document.getElementById("userUpsert").reset()
    }catch(error){
      Swal.fire("Oops...", "Something went wrong...", "error")
      console.log(error)
    }
    }

  function updateUser(id, fields, setSubmitting) {
    fields.UserId = parseInt(id);
    try {
      axios({
        url: "https://localhost:44390/api/users/" + id,
        method: "PUT",
        data: fields,
      }).catch((error) =>{
        if (error.response) {
          Swal.fire("Oops...", error.response.headers, "error")
        } else if (error.request) {
          Swal.fire("Oops...", "Error request was made but no response was recived. Error request: "+error.request, 'error');
        } else {
          Swal.fire("Oops...", "Something happened in setting up the request that triggered an Error. Error massage: "+error.message, 'error');
        }
        setSubmitting(false);
      });
      Swal.fire("Good job!", 'You succesfully edited a car!', 'success')
      setSubmitting(true);
      setTimeout(()=>history.push('/user-manager'),2000)
    } catch (error) {
      Swal.fire("Oops...", "Something went wrong", "error")
    }
  }
  const [user, setUser] = useState();

  useEffect(() => {
    async function fetchUser(){
      if (!isAddMode) {
        try {
          await axios
            .get("https://localhost:44390/api/users/" + id)
            .then((res) => {
              console.log(res.data)
              setUser(res.data);
            });
        } catch (error) {
          Swal.fire("Oops...", "Something went wrong when fetching", "error")
        }
      }
    }
    fetchUser();
  }, []);

  return (
    <Formik
      initialValues={isAddMode ? initialValues : user}
      validationSchema={validationSchema}
      enableReinitialize
      onSubmit={onSubmit}
    >
      {({ errors, touched, isSubmitting }) => {
        return (
          <Form id="userUpsert" className="upsertforms">
            <h1>{isAddMode ? "Add User" : "Edit User"}</h1>

            <label>First Name</label>
            <Field
              name="firstName"
              className={
                "form-control" +
                (errors.firstName && touched.firstName ? " is-invalid" : "")
              }
            />
            <ErrorMessage
              name="firstName"
              component="div"
              className="invalid-feedback"
            />

            <label>Last Name</label>
            <Field
              name="lastName"
              type="text"
              className={
                "form-control" +
                (errors.lastName && touched.lastName ? " is-invalid" : "")
              }
            />
            <ErrorMessage
              name="lastName"
              component="div"
              className="invalid-feedback"
            />

            <label>Email</label>
            <Field
              name="email"
              type="text"
              className={
                "form-control" +
                (errors.email && touched.email ? " is-invalid" : "")
              }
            />
            <ErrorMessage
              name="email"
              component="div"
              className="invalid-feedback"
            />

            <label>Number Identificate</label>
            <Field
              name="numberIdentificate"
              type="text"
              className={
                "form-control" +
                (errors.numberIdentificate && touched.numberIdentificate
                  ? " is-invalid"
                  : "")
              }
            />
            <ErrorMessage
              name="numberIdentificate"
              component="div"
              className="invalid-feedback"
            />

            <label>Mobile Number</label>
            <Field
              name="mobileNumber"
              className={
                "form-control" +
                (errors.mobileNumber && touched.mobileNumber
                  ? " is-invalid"
                  : "")
              }
            />
            <ErrorMessage
              name="mobileNumber"
              component="div"
              className="invalid-feedback"
            />
            <div className="pt-3">
              <button
                type="submit"
                disabled={isSubmitting}
                className="btn btn-primary"
              >
                {isSubmitting && (
                  <span className="spinner-border spinner-border-sm mr-1"></span>
                )}
                Save
              </button>

              <Link to={isAddMode ? "." : ".."} className="btn btn-link">
                Cancel
              </Link>
            </div>
          </Form>
        );
      }}
    </Formik>
  );
}
