import { useState } from "react";
import * as Yup from "yup";
import Api from "../API/ReservationApi";

export default function useBooking() {

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
    fields.userId = 2;
    fields.carId = 1;
    //FIXME:
    //get right userID from localstorage
    //get right carID from localstorage
    try {
      let api = new Api();
      await api.addReservation(fields);
    } catch (error) {
      console.log(error);
    }
  }

  return {
    validationSchema,
    checkavilable,
    freeTerms,
    onSubmit,
    checkAvilable,
  };
}
