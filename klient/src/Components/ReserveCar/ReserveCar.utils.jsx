import React, { useState } from "react";
import CardCar from "../CardCar/CardCar";
import { useHistory } from "react-router-dom";
import Api from "../API";

export default function useReserveCar() {
  const history = useHistory();

  const [data, setData] = useState([]);
  const [filters, setFilter] = useState({
    brand: "",
    model: "",
    registrationNumber: "",
    yearOfProduction: "",
    numberOfDoor: "",
    numberOfSits: "",
    startdate: "",
    enddate: "",
  });
  const [isLoading, setIsLoading] = useState(false);

  async function fetchCars() {
    try {
      setIsLoading(true);
      let api = new Api();
      const response = await api.fetchCars();
      setData(response);
      setIsLoading(false);
    } catch (error) {
      console.log(error);
      history.goBack();
    }
  }

  async function checkAvailability(startdate, enddate) {
    try {
      setIsLoading(true);
      let api = new Api();
      const response = await api.fetchCarByDate(startdate, enddate);
      setData(response);
      console.log(response);
      setIsLoading(false)
    } catch (error) {
      console.log(error);
    }
  }

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
        startdate={filters.startdate}
        enddate={filters.enddate}
      />
    );
  }

  return {
    data,
    filters,
    isLoading,
    fetchCars,
    handleChange,
    CreateCarCard,
    checkAvailability,
  };
}
