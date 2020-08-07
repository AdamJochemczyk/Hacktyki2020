import React from "react"
import { Formik, Field, Form, ErrorMessage } from "formik";
import { useHistory, Link} from "react-router-dom"
import * as Yup from "yup";
import "../styles/componentsstyle.css"
import Api from "../API/FaultApi"
import Swal from "sweetalert2"

export default function UserReportFault({history}){
  let data = history.location.state;

  let redirect = useHistory()
  const initialValues={
    description: ''
  }

  const validationSchema = Yup.object().shape({
    description: Yup.string()
      .required("Required"),
  });
  async function onSubmit(fields) {
    fields.carId=data.carId;
    fields.userId=parseInt(sessionStorage.getItem("userID"));
      try{
        let api=new Api()
        await api.createReport(fields)
        Swal.fire("Thank you!", 'You succesfully reported problem!', 'success')
        redirect.push('/')
      }
      catch(error){
        console.log(error)
        Swal.fire("Oops...", "Something went wrong...", "error").then(()=>redirect.goBack())
      }
  }

  return (
    <Formik
      initialValues={initialValues}
      validationSchema={validationSchema}
      enableReinitialize
      onSubmit={onSubmit}
    >
      {({ errors, touched }) => {
        return (
          <Form  className="upsertforms">
            <h1>Report problem</h1>

            <label>Describe it!</label>
            <Field
            as="textarea"
              name="description"
              className={
                "form-control" +
                (errors.description && touched.description ? " is-invalid" : "")
              }
            />
            <ErrorMessage
              name="firstName"
              component="div"
              className="invalid-feedback"
            />

            
            <div className="pt-3">
              <button
                type="submit"
                className="btn btn-primary"
              >
                Save
              </button>

              <Link to={".."} className="btn btn-link">
                Cancel
              </Link>
            </div>
          </Form>
        );
      }}
    </Formik>
  );
}