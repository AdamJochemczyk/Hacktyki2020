import React, { useState, useEffect } from "react";
import axios from "axios";
import CardCar from "../CarCard/CardCar";
import { Col, Form, Input, Row, Container } from "reactstrap";

export default function ReserveCar() {
  const [data, setData] = useState([]);
  const [filters, setFilter] = useState({});
  const fetchCars = async () => {
    try {
      const response = await axios.get("https://localhost:44390/api/cars");
      setData(response.data);
    } catch (e) {
      console.log(e);
      setData(data);
    }
  };
  useEffect(() => {
    fetchCars();
  }, []);

  function handleChange(event) {
    setFilter({
      ...filters,
      [event.target.name]: event.target.value,
    });
  }
  function CreateCarCard(data) {
    return (
      <CardCar
        key={data.carId}
        car={data.carId}
        brand={data.brand}
        model={data.model}
        registrationNumber={data.registrationNumber}
        yearOfProduction={data.yearOfProduction}
        doors={data.numberOfDoor}
        sits={data.numberOfSits}
        src={data.imagePath}
      />
    );
  }
  return (
    <Container>
      <Row>
        <Form>
          <h1>Search your car</h1>
          <Input
            type="text"
            placeholder="Brand"
            name="brand"
            value={filters.brand}
            onChange={handleChange}
          />
        </Form>
      </Row>
      <Row>{data.map(CreateCarCard)}</Row>
    </Container>
  );
}
