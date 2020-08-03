import React, { useState } from "react";
import { Col, Row, Button, Table } from "reactstrap";
import { useHistory } from "react-router-dom";
import Swal from "sweetalert2";
import axios from "axios";
import { Formik, Field, Form, ErrorMessage } from "formik";
import * as Yup from "yup";
import moment from "moment"
import Api from "../API";

export default function Booking({ history }) {
  let data = history.location.state;
  let redirect = useHistory();
  const BASE_URL = process.env.REACT_APP_RESERVATION_API;
  const [checkavilable, setCheckAvilable] = useState(false);
  const [freeTerms, setFreeTerms] = useState([]);

  let initialValues = {
    rentaldate: data.startdate,
    returndate: data.enddate,
  };

  const validationSchema = Yup.object().shape({
    rentaldate: Yup.string()
      .required("Required"),
      returndate: Yup.string()
      .required("Required"),
  })

  async function checkAvilable() {
    setCheckAvilable(true);
    console.log("Chcek avilable: ",BASE_URL+"/terms/"+data.car)
    let api=new Api()
    const response = await api.checkAvilable(data.car)
    console.log(response)
    setFreeTerms(response);
  }

  async function onSubmit(fields) {
    fields.userId= 28
    fields.carId= data.car
      //FIXME:
      //get right userID from localstorage
    try {
      await axios({
        url: BASE_URL,
        method: "POST",
        data: fields,
      }).catch((error) => {
        Swal.fire("Oops...", error, "error");
      });
      Swal.fire("Good job!", "You successfully reserved a car!", "success");
      redirect.push("/");
    } catch (error) {
      Swal.fire("Oops...", "Something went wrong...", "error");
      console.log(error);
    }
  }

  return (
    <Col>
      <Row style={{ justifyContent: "center" }}>
        <h1>Reserve this car!</h1>
      </Row>
      <Row>
        <Col sm={6}>
          <p>CarId:{data.car}</p>
          <p>Brand: {data.brand}</p>
          <p>Doors: {data.brand}</p>
          <p>model: {data.model}</p>
          <p>registrationNumber: {data.registrationNumber}</p>
          <p>sits: {data.sits}</p>
          <p>src: {data.src}</p>
          <p>yearOfProduction: {data.yearOfProduction}</p>
        </Col>
        <Col sm={6}>
          <Formik
      initialValues={initialValues}
      validationSchema={validationSchema}
      enableReinitialize
      onSubmit={onSubmit}
    >
     {({ errors, touched }) => {
        return (
          <Form id="userUpsert" className="upsertforms">

            <label>Start date</label>
            <Field
              name="rentaldate"
              type="date"
              min={moment().format("YYYY-MM-DD")}
              className={
                "form-control" +
                (errors.rentaldate && touched.rentaldate ? " is-invalid" : "")
              }
            />
            <ErrorMessage
              name="rentaldate"
              component="div"
              className="invalid-feedback"
            />

            <label>End date</label>
            <Field
              name="returndate"
              type="date"
              min={moment().format("YYYY-MM-DD")}
              className={
                "form-control" +
                (errors.returndate && touched.returndate ? " is-invalid" : "")
              }
            />
            <ErrorMessage
              name="returndate"
              component="div"
              className="invalid-feedback"
            />


<div className="pt-3">
<Button color="primary" onClick={checkAvilable} name="checkdates">
            Check date
          </Button>
          <Button
            color="success"
            type="submit"
          >
            Confirm reservation
          </Button>
            </div>
          </Form>
        );
      }}
    </Formik>
    {checkavilable && (
      <div style={{
            maxHeight: '200px',
            overflowY: 'auto'
          }}>
            <Table bordered>
            <thead>
              <tr><td>Avilable terms</td></tr>
            </thead>
              {(freeTerms && freeTerms.lenght!==0) &&
                 freeTerms.map((reservation) => {
                    return <tr><td>{reservation}</td></tr>;
                  })
                  }
                  </Table>
                  </div>
          )}
    </Col>
      </Row>
    </Col>
  );
}