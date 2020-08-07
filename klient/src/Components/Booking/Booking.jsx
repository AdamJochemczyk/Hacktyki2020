import React, { useRef } from "react";
import { Col, Row, Button, Table } from "reactstrap";
import { Formik, Field, Form, ErrorMessage } from "formik";
import moment from "moment";
import useBooking from "./Booking.utils";

export default function Booking({ history }) {
  const {
    validationSchema,
    checkavilable,
    freeTerms,
    onSubmit,
    checkAvilable,
  } = useBooking();
  let data = history.location.state;
  sessionStorage.setItem("carID", data.car)
  let initialValues = {
    rentaldate: data.startdate,
    returndate: data.enddate,
  };
  const ref = useRef(null);

  return (
    <Col>
      <Row style={{ justifyContent: "center" }}>
        <h1>Reserve this car!</h1>
      </Row>
      <Row>
        <Col sm={6}>
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
            innerRef={ref}
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
                      (errors.rentaldate && touched.rentaldate
                        ? " is-invalid"
                        : "")
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
                    id="enddate"
                    min={moment().format("YYYY-MM-DD")}
                    className={
                      "form-control" +
                      (errors.returndate && touched.returndate
                        ? " is-invalid"
                        : "")
                    }
                  />
                  <ErrorMessage
                    name="returndate"
                    component="div"
                    className="invalid-feedback"
                  />

                  <div className="pt-3">
                    <Button
                      color="primary"
                      onClick={() =>
                        checkAvilable(
                          data.car,
                          ref.current.values.rentaldate,
                          ref.current.values.returndate
                        )
                      }
                    >
                      Check date
                    </Button>
                    <Button color="success" type="submit">
                      Confirm reservation
                    </Button>
                  </div>
                </Form>
              );
            }}
          </Formik>
          {checkavilable && (
            <div
              style={{
                maxHeight: "200px",
                overflowY: "auto",
              }}
            >
              <Table bordered>
                <thead>
                  <tr>
                    <td>Available terms</td>
                  </tr>
                </thead>
                <tbody>
                {freeTerms &&
                  freeTerms.length !== 0 &&
                  freeTerms.map((reservation, i) => {
                    return (
                      <tr key={i}>
                        <td>{reservation}</td>
                      </tr>
                    );
                  })}
                  </tbody>
              </Table>
            </div>
          )}
        </Col>
      </Row>
    </Col>
  );
}
