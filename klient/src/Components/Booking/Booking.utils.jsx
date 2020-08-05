import React, { useState } from "react";
import * as Yup from "yup";
import Api from "../API";
import { useHistory } from "react-router-dom";

export default function useBooking() {
  let redirect = useHistory();
  const [checkavilable, setCheckAvilable] = useState(false);
  const [freeTerms, setFreeTerms] = useState([]);

  const validationSchema = Yup.object().shape({
    rentaldate: Yup.date().required("Required"),
    returndate: Yup.date().min(
      Yup.ref("rentaldate"),
      "End date should be greater"
    ),
  });

  async function checkAvilable(carid,rentaldate,returndate) {
      
    let api = new Api();
    const response = await api.checkAvilable(carid,rentaldate,returndate);
    console.log(response);
    setFreeTerms(response);
    setCheckAvilable(true);
  }

  async function onSubmit(fields) {
    fields.userId = 1;
    fields.carId = 1;
    //FIXME:
    //get right userID from localstorage
    //get right carID from localstorage
    /*try {
      let api = new Api();
      await api.addReservation(fields);
    } catch (error) {
      console.log(error);
    }*/
  }

  return {
    validationSchema,
    checkavilable,
    freeTerms,
    onSubmit,
    checkAvilable,
  };
}
