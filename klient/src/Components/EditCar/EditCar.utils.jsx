import { useState } from "react";
import { useHistory } from "react-router-dom";
import * as Yup from "yup";
import moment from "moment";
import Api from "../API";

export default function useEditCar() {
  let redirect = useHistory();
  const [car, setCar] = useState();
  let initialValues = {
    brand: "",
    registrationNumber: "",
    model: "",
    typeOfCar: "",
    numberOfDoor: "",
    numberOfSits: "",
    yearOfProduction: "",
    imagePath: "",
  };

  const validationSchema = Yup.object().shape({
    registrationNumber: Yup.string()
      .min(3, "Too short!")
      .max(7, "Too long!")
      .required("Required"),
    brand: Yup.string()
      .min(3, "Too short!")
      .max(30, "Too long!")
      .required("Required"),
    model: Yup.string()
      .min(1, "Too short!")
      .max(20, "Too long!")
      .required("Required"),
    typeOfCar: Yup.string().required("Required"),
    numberOfDoor: Yup.number().required("Required").min(2).max(5),
    numberOfSits: Yup.number().required("Required").min(2).max(9),
    yearOfProduction: Yup.number()
      .required("Required")
      .min(1950)
      .max(moment().format("YYYY")),
  });

  function createCar(fields, setSubmitting) {
    try{
    let cartype = parseInt(fields.typeOfCar);
    fields.typeOfCar = cartype;
    let api = new Api();
    api.createCar(fields);
    setSubmitting(false)
    }
    catch(error){
      console.log(error)
    }
  }

  function updateCar(id, fields, setSubmitting) {
    try{
    fields.carId = parseInt(id);
    let cartype = parseInt(fields.typeOfCar);
    fields.typeOfCar = cartype;
    console.log(fields);
    let api = new Api();
    api.updateCar(id,fields);
    setSubmitting(false);
    setTimeout(() => redirect.push("/car-manager"), 2000);
    }
    catch(error){
      console.log(error)
    }
  }

  async function fetchCar(id) {
    let api = new Api();
    const res=await api.fetchCar(id);
    setCar(res);
  }

  return {
    initialValues,
    validationSchema,
    car,
    createCar,
    updateCar,
    fetchCar,
  };
}
