import React, { useEffect, useState } from "react";
import { Link, useParams, useHistory } from "react-router-dom";
import { Row, Col } from "reactstrap";
import { Formik, Field, Form, ErrorMessage } from "formik";
import * as Yup from "yup";
import axios from "axios";
import Swal from "sweetalert2";

export default function EditCar() {
  const { id } = useParams();
  const isAddMode = !id;
  let history = useHistory();
  const BASE_URL= process.env.REACT_APP_CAR_API;
  
  

  let initialValues = {
    brand: "",
    registrationNumber: "",
    model: "",
    typeOfCar: "",
    numberOfDoor: "",
    numberOfSits: "",
    yearOfProduction: "",
    imagePath: "",
  };
  const date = new Date();

  const validationSchema = Yup.object().shape({
    registrationNumber: Yup.string()
      .min(3, "Too short!")
      .max(7, "Too long!")
      .required("Required"),
    brand: Yup.string()
      .min(3, "Too short!")
      .max(30, "Too long!")
      .required("Required"),
    model: Yup.string()
      .min(1, "Too short!")
      .max(20, "Too long!")
      .required("Required"),
    typeOfCar: Yup.string().required("Required"),
    numberOfDoor: Yup.number().required("Required").min(2).max(5),
    numberOfSits: Yup.number().required("Required").min(2).max(9),
    yearOfProduction: Yup.number()
      .required("Required")
      .min(1950)
      .max(date.getFullYear()),
  });

  function onSubmit(fields, { setSubmitting }) {
    if (isAddMode) {
      createCar(fields, setSubmitting);
    } else {
      updateCar(id, fields, setSubmitting);
    }
  }

  async function createCar(fields, setSubmitting) {
    let cartype = parseInt(fields.typeOfCar);
    fields.typeOfCar = cartype;
    try {
      await axios({
        url: BASE_URL,
        method: "POST",
        data: fields,
      }).catch((error) => {
        if (error.response) {
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
        setSubmitting(false);
      });
      Swal.fire("Good job!", "You succesfully added new car!", "success");
      document.getElementById("carUpsert").reset();
    } catch (error) {
      Swal.fire("Oops...", "Something went wrong...", "error");
      console.log(error);
    }
  }

  async function updateCar(id, fields, setSubmitting) {
    fields.carId = parseInt(id);
    let cartype = parseInt(fields.typeOfCar);
    fields.typeOfCar = cartype;
    console.log(fields);
    try {
      await axios({
        url: BASE_URL +"/"+id,
        method: "PUT",
        data: fields,
      }).catch((error) => {
        if (error.response) {
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
        setSubmitting(false);
      });
      Swal.fire("Good job!", "You succesfully edited a car!", "success");
      setSubmitting(true);
      setTimeout(() => history.push("/car-manager"), 2000);
    } catch (error) {
      Swal.fire("Oops...", "Something went wrong", "error");
    }
  }
  const [car, setCar] = useState();

  useEffect(() => {
    async function fetchCar() {
      if (!isAddMode) {
        try {
          await axios
            .get(BASE_URL +"/"+ id)
            .then((res) => {
              console.log(res.data);
              setCar(res.data);
            });
        } catch (error) {
          alert(error.message);
        }
      }
    }
    fetchCar();
  }, []);

  return (
    <Formik
      initialValues={isAddMode ? initialValues : car}
      validationSchema={validationSchema}
      enableReinitialize
      onSubmit={onSubmit}
    >
      {({ errors, touched, isSubmitting }) => {
        return (
          <Form id="carUpsert" className="upsertforms">
            <h1>{isAddMode ? "Add Car" : "Edit Car"}</h1>
            <Row>
              <Col>
                <label>Brand</label>
                <Field
                  name="brand"
                  className={
                    "form-control" +
                    (errors.brand && touched.brand ? " is-invalid" : "")
                  }
                />
                <ErrorMessage
                  name="brand"
                  component="div"
                  className="invalid-feedback"
                />

                <label>Registration Number</label>
                <Field
                  name="registrationNumber"
                  type="text"
                  className={
                    "form-control" +
                    (errors.registrationNumber && touched.registrationNumber
                      ? " is-invalid"
                      : "")
                  }
                />
                <ErrorMessage
                  name="registrationNumber"
                  component="div"
                  className="invalid-feedback"
                />

                <label>Model</label>
                <Field
                  name="model"
                  type="text"
                  className={
                    "form-control" +
                    (errors.model && touched.model ? " is-invalid" : "")
                  }
                />
                <ErrorMessage
                  name="model"
                  component="div"
                  className="invalid-feedback"
                />

                <label>Type Of Car</label>
                <Field
                  name="typeOfCar"
                  as="select"
                  className={
                    "form-control" +
                    (errors.typeOfCar && touched.typeOfCar ? " is-invalid" : "")
                  }
                >
                  <option selected>Choose type...</option>
                  <option value="1">Classic</option>
                  <option value="0">Sport</option>
                  <option value="2">Retro</option>
                </Field>
              </Col>
              <Col>
                <label>Year Of Procuction</label>
                <Field
                  type="number"
                  name="yearOfProduction"
                  className={
                    "form-control" +
                    (errors.yearOfProduction && touched.yearOfProduction
                      ? " is-invalid"
                      : "")
                  }
                />
                <label>Number of sits</label>
                <Field
                  type="number"
                  name="numberOfSits"
                  className={
                    "form-control" +
                    (errors.numberOfSits && touched.numberOfSits
                      ? " is-invalid"
                      : "")
                  }
                />
                <label>Number of doors</label>
                <Field
                  type="number"
                  name="numberOfDoor"
                  className={
                    "form-control" +
                    (errors.numberOfDoor && touched.numberOfDoor
                      ? " is-invalid"
                      : "")
                  }
                />
                <label>Img src</label>
                <Field
                  name="imagePath"
                  className={
                    "form-control" +
                    (errors.imagePath && touched.imagePath ? " is-invalid" : "")
                  }
                />
              </Col>
            </Row>
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
