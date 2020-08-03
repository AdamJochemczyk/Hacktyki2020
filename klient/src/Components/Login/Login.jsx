import React from "react";
import "../styles/componentsstyle.css";
import { Formik, Field, Form, ErrorMessage } from "formik";
import { Link } from "react-router-dom";
import useLogin from "./Login.utils";

export default function Login() {
  const { initialValues, validationSchema, onSubmit } = useLogin();

  return (
    <Formik
      initialValues={initialValues}
      validationSchema={validationSchema}
      enableReinitialize
      onSubmit={onSubmit}
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
