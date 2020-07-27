import React, { useState, useEffect } from "react";
import axios from "axios";
import CardCar from "../CardCar/CardCar";
import { Col, Form, Input, Row } from "reactstrap";
import Loader from "react-loader-spinner";
import Swal from "sweetalert2";
import { useHistory } from "react-router-dom";

export default function ReserveCar() {
  const [data, setData] = useState([]);
  const [filters, setFilter] = useState({
    brand: "",
    model: "",
    registrationNumber: "",
    yearOfProduction: "",
    numberOfDoor: "",
    numberOfSits: ""
  });
  const [isLoading, setIsLoading] = useState(false);
  const history = useHistory();

  useEffect(() => {
    async function fetchCars() {
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
  console.log(data)
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
          <Row>
            <Col sm={2}>
              <Form>
                Search your car by:
                <Input
                  type="text"
                  placeholder="Brand"
                  name="brand"
                  value={filters.brand}
                  onChange={handleChange}
                />
                <Input
                  type="text"
                  placeholder="Model"
                  name="model"
                  value={filters.model}
                  onChange={handleChange}
                />
                <Input
                  type="text"
                  placeholder="Registration Number"
                  name="registrationNumber"
                  value={filters.registrationNumber}
                  onChange={handleChange}
                />
                <Input
                  type="text"
                  placeholder="Year Of Production"
                  name="yearOfProduction"
                  value={filters.yearOfProduction}
                  onChange={handleChange}
                />
                <Input
                  type="text"
                  placeholder="Number of doors"
                  name="numberOfDoor"
                  value={filters.numberOfDoor}
                  onChange={handleChange}
                />
                <Input
                  type="text"
                  placeholder="Sits places"
                  name="numberOfSits"
                  value={filters.numberOfSits}
                  onChange={handleChange}
                />
              </Form>
            </Col>
            <Col>
              <Row>
                {data
                  .filter((data) => {
                    return (
                      data.brand.toLowerCase().includes(filters.brand.toLowerCase()) &&
                      data.model.toLowerCase().includes(filters.model.toLowerCase()) &&
                      data.registrationNumber.toLowerCase().includes(filters.registrationNumber.toLowerCase()) &&
                      data.yearOfProduction >=filters.yearOfProduction &&
                      data.numberOfDoor >=filters.numberOfDoor &&
                      data.numberOfSits >= filters.numberOfSits
                    );
                  })
                  .map(CreateCarCard)}
              </Row>
            </Col>
          </Row>
        </div>
      )}
    </div>
  );
}
