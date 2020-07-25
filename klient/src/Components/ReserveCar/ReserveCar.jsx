import React, { useState, useEffect } from "react";
import axios from "axios";
import CardCar from "../CardCar/CardCar";
import { Col, Form, Input, Row, Button } from "reactstrap";
import Loader from "react-loader-spinner";
import Swal from "sweetalert2";
import { useHistory } from "react-router-dom";

export default function ReserveCar() {
  const [data, setData] = useState([]);
  const [filters, setFilter] = useState({});
  const [isLoading, setIsLoading] = useState(false);
  const history = useHistory();

  useEffect(() => {
    async function fetchCars(){
      try {
        setIsLoading(true);
        const response = await axios.get("https://localhost:44390/api/cars");
        setData(response.data);
        setIsLoading(false);
      } catch (error) {
        Swal.fire("Oops...", "Something went wrong!", "error").then(() =>
          history.goBack()
        );
      }
    }
    fetchCars();
  }, []);

  function handleChange(event) {
    setFilter({
      ...filters,
      [event.target.name]: event.target.value,
    });
  }

  function reloadDataWithFilters() {
    console.log("clicked");
    let filteredData = {
      carId: 55,
      brand: "test",
    };
    setData(filteredData);
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
    <div>
      {isLoading ? (
        <div className="loader">
          <Loader type="Oval" color="#00BFFF" />
        </div>
      ) : (
        <div>
          <Col sm={2} style={{ float: "left", textAlign: "center" }}>
            <Form>
              Search your car by:
              <Input
                type="text"
                placeholder="Brand"
                name="brand"
                value={filters.brand}
                onChange={handleChange}
              />
              <Button onClick={reloadDataWithFilters}>Click me!</Button>
            </Form>
          </Col>
          <Col>
            <Row>{data.map(CreateCarCard)}</Row>
          </Col>
        </div>
      )}
    </div>
  );
}
