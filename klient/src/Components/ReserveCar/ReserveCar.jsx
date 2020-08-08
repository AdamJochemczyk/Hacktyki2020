import React, { useEffect } from "react";
import { Col, Form, Input, Row, Button } from "reactstrap";
import Loader from "react-loader-spinner";
import moment from "moment";
import useReserveCar from "./ReserveCar.utils";

export default function ReserveCar() {
  const {
    data,
    filters,
    isLoading,
    fetchCars,
    handleChange,
    CreateCarCard,
    checkAvailability,
  } = useReserveCar();

  useEffect(() => {
    fetchCars();
  }, []);

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
                Search car by:
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
                  placeholder="Minimum year of production"
                  name="yearOfProduction"
                  value={filters.yearOfProduction}
                  onChange={handleChange}
                />
                <Input
                  type="text"
                  placeholder="Minimum number of doors"
                  name="numberOfDoor"
                  value={filters.numberOfDoor}
                  onChange={handleChange}
                />
                <Input
                  type="text"
                  placeholder="Minimum sits places"
                  name="numberOfSits"
                  value={filters.numberOfSits}
                  onChange={handleChange}
                />
                <Input
                  type="date"
                  min={moment().format("YYYY-MM-DD")}
                  onChange={handleChange}
                  name="startdate"
                />
                <Input
                  type="date"
                  min={moment().format("YYYY-MM-DD")}
                  onChange={handleChange}
                  name="enddate"
                />
                <Button
                  color="success"
                  disabled={filters.startdate === '' || filters.enddate === ''}
                  onClick={() =>
                    checkAvailability(filters.startdate, filters.enddate)
                  }
                >
                  Search
                </Button>
              </Form>
            </Col>
            <Col sm={10}>
              <Row>
                {data && data.length!==0 ? data
                  .filter((data) => {
                    return (
                      data.brand
                        .toLowerCase()
                        .includes(filters.brand.toLowerCase()) &&
                      data.model
                        .toLowerCase()
                        .includes(filters.model.toLowerCase()) &&
                      data.registrationNumber
                        .toLowerCase()
                        .includes(filters.registrationNumber.toLowerCase()) &&
                      data.yearOfProduction >= filters.yearOfProduction &&
                      data.numberOfDoor >= filters.numberOfDoor &&
                      data.numberOfSits >= filters.numberOfSits
                    );
                  }) 
                  .map(CreateCarCard) : "We don't have cars in this term"}
              </Row>
            </Col>
          </Row>
        </div>
      )}
    </div>
  );
}
