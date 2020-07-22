import React, { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { Formik, Field, Form, ErrorMessage } from "formik";
import * as Yup from "yup";
import axios from "axios";

export default function EditUser() {
  const { id } = useParams();
  const isAddMode = !id;

  let initialValues = {
    FirstName: "",
    LastName: "",
    Email: "",
    NumberIdentificate: "",
    MobileNumber: "",
  };

  const validationSchema = Yup.object().shape({
    FirstName: Yup.string()
      .min(3, "Too short!")
      .max(20, "Too long!")
      .required("Required"),
    LastName: Yup.string()
      .min(3, "Too short!")
      .max(20, "Too long!")
      .required("Required"),
    NumberIdentificate: Yup.string()
      .min(6)
      .max(6, "Too long!")
      .required("Required"),
    Email: Yup.string().email("Invalid email").required("Required"),
    MobileNumber: Yup.string()
      .min(9, "Must contains at least 9 numers")
      .max(15, "Too long number")
      .required("Required"),
  });

  function onSubmit(fields, { setStatus, setSubmitting }) {
    setStatus();
    if (isAddMode) {
      createUser(fields, setSubmitting);
    } else {
      updateUser(id, fields, setSubmitting);
    }
  }

  function createUser(fields, setSubmitting) {
    try {
      axios({
        url: "https://localhost:44390/api/authorization",
        method: "POST",
        data: fields,
      });
    } catch (e) {
      console.log(e);
      setSubmitting(false);
    }
  }

  function updateUser(id, fields, setSubmitting) {
    fields.UserId = parseInt(id);
    console.log(fields);
    try {
      axios({
        url: "https://localhost:44390/api/users/" + id,
        method: "PUT",
        data: fields,
      });
      alert("User edited");
      setSubmitting(true);
    } catch (e) {
      console.log(e);
      setSubmitting(false);
    }
  }
  const [user, setUser] = useState();

  const fetchUser = async () => {
    if (!isAddMode) {
      try {
        await axios.get(
          "https://localhost:44390/api/users/" + id
        ).then(res=> {
          const myuser=res.data;
          console.log("User from api:",myuser)
          setUser(myuser)
        });
        console.log("User:",user)
        /*setUser(response.data);
        console.log("Response data:", response.data);
        console.log("user:", user)
        console.log("initialvalues:", initialValues);*/
      } catch (e) {
        console.log(e);
      }
    }
  };
  useEffect(() => {
    fetchUser();
  }, []);

  return (
    <Formik
      initialValues={initialValues}
      validationSchema={validationSchema}
      enableReinitialize
      onSubmit={onSubmit}
    >
      {({ errors, touched, isSubmitting }) => {
        return (
          <Form>
            <h1>{isAddMode ? "Add User" : "Edit User"}</h1>

            <label>First Name</label>
            <Field
              name="FirstName"
              className={
                "form-control" +
                (errors.FirstName && touched.FirstName ? " is-invalid" : "")
              }
            />
            <ErrorMessage
              name="FirstName"
              component="div"
              className="invalid-feedback"
            />

            <label>Last Name</label>
            <Field
              name="LastName"
              type="text"
              className={
                "form-control" +
                (errors.LastName && touched.LastName ? " is-invalid" : "")
              }
            />
            <ErrorMessage
              name="LastName"
              component="div"
              className="invalid-feedback"
            />

            <label>Email</label>
            <Field
              name="Email"
              type="text"
              className={
                "form-control" +
                (errors.Email && touched.Email ? " is-invalid" : "")
              }
            />
            <ErrorMessage
              name="Email"
              component="div"
              className="invalid-feedback"
            />

            <label>Number Identificate</label>
            <Field
              name="NumberIdentificate"
              type="text"
              className={
                "form-control" +
                (errors.NumberIdentificate && touched.NumberIdentificate
                  ? " is-invalid"
                  : "")
              }
            />
            <ErrorMessage
              name="NumberIdentificate"
              component="div"
              className="invalid-feedback"
            />

            <label>Mobile Number</label>
            <Field
              name="MobileNumber"
              className={
                "form-control" +
                (errors.MobileNumber && touched.MobileNumber
                  ? " is-invalid"
                  : "")
              }
            />
            <ErrorMessage
              name="MobileNumber"
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
