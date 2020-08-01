import React from "react"
import { Formik, Field, Form, ErrorMessage } from "formik";
import { useHistory, Link} from "react-router-dom"
import * as Yup from "yup";
import "../styles/componentsstyle.css"
import axios from "axios"
import Swal from "sweetalert2"

export default function UserReportFault({history}){
  let data = history.location.state;
  console.log(data)

  let redirect = useHistory()
  const initialValues={
    description: ''
  }

  const validationSchema = Yup.object().shape({
    description: Yup.string()
      .required("Required"),
  });
  function onSubmit(fields) {
    fields.carId=data.carId;
    //FIXME:
    //get id from localstorage
    fields.userId=28;
    //TODO:
      //url
      //after send
      try{
        axios({
          url: "",
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
          redirect.goBack()
        });
        Swal.fire("Thank you!", 'You succesfully reported problem!', 'success')
        redirect.push('/')
      }catch(error){
        Swal.fire("Oops...", "Something went wrong...", "error")
        console.log(error)
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

              <Link to={"."} className="btn btn-link">
                Cancel
              </Link>
            </div>
          </Form>
        );
      }}
    </Formik>
  );
}