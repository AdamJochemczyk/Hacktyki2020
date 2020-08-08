import React, { useState } from "react";
import CardCar from "../CardCar/CardCar";
import Api from "../API/CarApi";
import moment from "moment";
import Swal from "sweetalert2";

export default function useReserveCar() {
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
    }
  }

  async function checkAvailability(startdate, enddate) {
    if (moment.utc(startdate).isBefore(moment.utc(enddate))) {
      try {
        setIsLoading(true);
        let api = new Api();
        const response = await api.fetchCarByDate(startdate, enddate);
        setData(response);
        setIsLoading(false);
      } catch (error) {
        console.log(error);
      }
    }
    else{
      Swal.fire("Oops...", 'End date must be after start date','error')
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
