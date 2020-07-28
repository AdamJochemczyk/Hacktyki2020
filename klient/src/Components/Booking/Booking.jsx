import React, {useState} from "react";
import { Col, Row,Input,Button, Table } from "reactstrap";
import {useHistory} from "react-router-dom"
import Swal from "sweetalert2";
import axios from "axios"

export default function Booking({ history }) {
  let data = history.location.state;
  let redirect = useHistory()
  const BASE_URL=process.env.REACT_APP_RESERVATION_API
  const [checkavilable, setCheckAvilable]=useState(false)
  function checkAvilable(){
    setCheckAvilable(true)
  }

  async function reserveCar(){
      const fields={
      rentaldate: data.startdate,
      returndate: data.enddate,
      //FIXME:
      //get right userID from localstorage
      userId: 28,
      carId: data.car
    }
    console.log("reserveCar -> fields", fields)
    try {
      await axios({
      url: BASE_URL,
      method: "POST",
      data: fields
    }).catch((error)=>{   
         Swal.fire("Oops...", error, "error")
    })
    Swal.fire("Good job!", "You successfully reserved a car!", "success")
    redirect.push('/')
  }
    catch (error) {
      Swal.fire("Oops...", "Something went wrong...", "error");
      console.log(error);
    }
  }

  return (
    <Col>
      <Row style={{justifyContent: "center"}}>
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
        <Col sm={6}>Check date
        <Input type="date" value={data.startdate} />
          <Input type="date" value={data.enddate} />
          <Button color="primary" onClick={checkAvilable}>Check date</Button>
          <Button color="success" onClick={reserveCar} disabled={!checkavilable}>Confirm reservation</Button>
          {checkavilable && <Table>
            <td>Checked</td>
          </Table>}
          </Col>
      </Row>
    </Col>
  );
}
