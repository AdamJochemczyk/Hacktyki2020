import React from "react";
import "../styles/componentsstyle.css";
import axios from "axios";
import Swal from "sweetalert2";
import { Formik, Field, Form, ErrorMessage } from "formik";
import { Link } from "react-router-dom";
import * as Yup from "yup";

export default function Login() {
  let initialValues = {
    email: "",
    encodePassword: "",
  };

  const validationSchema = Yup.object().shape({
    encodePassword: Yup.string().required("Required"),
    email: Yup.string().email("This isn't email").required("Required"),
  });

  async function signIn(fields) {
  console.log("signIn -> fields", fields)
        
    try {
      const response = await axios({
        url: "https://localhost:44390/api/authorization/signIn",
        method: "POST",
        data: fields,
      }).catch((error) => {
        if (error.response) {
          console.log(error);
          Swal.fire("Oops...", error.response.headers, "error");
        } else if (error.request) {
          Swal.fire(
            "Oops...",
            "Error request was made but no response was recived. Error request: " +
              error.request,
            "error"
          );
        } else {
          Swal.fire(
            "Oops...",
            "Something happened in setting up the request that triggered an Error. Error massage: " +
              error.message,
            "error"
          );
        }
      });
      console.log("signIn -> response", response.data)
    } catch (error) {
      Swal.fire("Oops...", "Something went wrong", "error");
    }
  }

  return (
    <Formik
      initialValues={initialValues}
      validationSchema={validationSchema}
      enableReinitialize
      onSubmit={signIn}
    >
      {({ errors, touched, isSubmitting }) => {
        return (
          <Form className="upsertforms" id="loginform">
            <h1>Sign In</h1>
            <img
              src="https://image.flaticon.com/icons/svg/3190/3190448.svg"
              alt="secure"
              height="150"
              width="150"
            />
            <label>Email</label>
            <Field
              name="email"
              type="email"
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

            <label>Password</label>
            <Field
              name="encodePassword"
              type="password"
              className={
                "form-control" +
                (errors.encodePassword && touched.encodePassword
                  ? " is-invalid"
                  : "")
              }
            />
            <ErrorMessage
              name="encodePassword"
              component="div"
              className="invalid-feedback"
            />

            <div className="pt-3">
              <Link to={"."} className="btn btn-link">
                Cancel
              </Link>
              <button type="submit" className="btn btn-primary">
                Log in
              </button>
            </div>
          </Form>
        );
      }}
    </Formik>
  );
}
