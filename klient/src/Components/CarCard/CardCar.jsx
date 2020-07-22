import React from "react";
import { Button, Col } from "reactstrap";

export default function Cardcar(props) {
  const DEFAULT_IMAGE =
    "https://pngimg.com/uploads/question_mark/question_mark_PNG136.png";

  function reserveThatCar(event) {
    console.log(event.target.value);
  }

  return (
    <Col sm="4">
    <div
      style={{
        margin: "3%",
        border: "black 2px",
        boxShadow: "5px 5px 5px 5px #888888",
        padding: "2%",
      }}
    >
      <div style={{ textAlign: "center", display: "block" }}>
        <img
          src={props.imagePath || DEFAULT_IMAGE}
          alt="car"
          style={{ width: 200, height: 200 }}
        />
      </div>
      <h5 key={props.key}>Model: {props.model}</h5>
      <p>Year of production: {props.yearOfProduction}</p>
      <p>Brand: {props.brand}</p>
      <p>Registration number: {props.registrationNumber}</p>
      <p>Sits: {props.sits}</p>
      <p>Doors: {props.doors}</p>
      <div style={{ textAlign: "center" }}>
        <Button color="success" onClick={reserveThatCar} value={props.car}>
          Book me!
        </Button>
      </div>
    </div>
    </Col>
  );
}
