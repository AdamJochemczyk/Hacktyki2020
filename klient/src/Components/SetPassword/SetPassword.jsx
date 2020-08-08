import React from "react";
import { useParams, useHistory } from "react-router-dom";
import Swal from "sweetalert2";
import { Formik, Field, Form, ErrorMessage } from "formik";
import * as Yup from "yup";
import Api from "../API/AuthorizationApi"

export default function SetPassword() {

  const { code } = useParams();
  let history = useHistory()

  let initialValues = {
    encodePassword: '',
    confirmEncodePassword: '',
    codeofverification: code
  }

  const validationSchema = Yup.object().shape({
    encodePassword: Yup.string()
      .min(8, "Too short!")
      .required("Required"),
      confirmEncodePassword: Yup.string()
      .min(8, "Too short!")
      .required("Required")
      .oneOf([Yup.ref('encodePassword'),null], 'Passwords must match')
  });


  function onSubmit(fields) {
    if (fields.encodePassword===fields.confirmEncodePassword) {
      sendPassword(fields);
    }
    else{
      Swal.fire("Oops...", "Try again password weren't same", "error");
    }
  }

  async function sendPassword(fields) {
    try {
      let api=new Api()
      await api.sendPassword(fields)
      history.push('/')
    } catch (error) {
      Swal.fire("Oops...", "Something went wrong", "error");
    }
  }

  return (
    <Formik
      initialValues={initialValues}
      validationSchema={validationSchema}
      enableReinitialize
      onSubmit={onSubmit}
    >
      {({ errors, touched, isSubmitting }) => {
        return (
          <Form id="setPasswordForm">
            <h1>Set your password</h1>
            <label>Password</label>
            <Field
              name="encodePassword"
              type="password"
              className={
                "form-control" +
                (errors.encodePassword && touched.encodePassword ? " is-invalid" : "")
              }
            />
            <ErrorMessage
              name="encodePassword"
              component="div"
              className="invalid-feedback"
            />

            <label>Confirm password</label>
            <Field
              name="confirmEncodePassword"
              type="password"
              className={
                "form-control" +
                (errors.confirmEncodePassword && touched.confirmEncodePassword
                  ? " is-invalid"
                  : "")
              }
            />
            <ErrorMessage
              name="confirmEncodePassword"
              component="div"
              className="invalid-feedback"
            />

            <div className="pt-3">
              <button
                type="submit"
                className="btn btn-primary"
              >
                Set password
              </button>
            </div>
          </Form>
        );
      }}
    </Formik>
  );
}
