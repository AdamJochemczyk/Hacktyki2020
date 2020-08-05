import { useState } from "react";
import { useHistory } from "react-router-dom";
import * as Yup from "yup";
import Api from "../API/UserApi";

export default function useEditUser() {
  let redirect = useHistory();
  const [user, setUser] = useState();
  let initialValues = {
    firstName: "",
    lastName: "",
    email: "",
    numberIdentificate: "",
    mobileNumber: "",
  };

  const validationSchema = Yup.object().shape({
    firstName: Yup.string()
      .min(3, "Too short!")
      .max(20, "Too long!")
      .required("Required"),
    lastName: Yup.string()
      .min(3, "Too short!")
      .max(20, "Too long!")
      .required("Required"),
    numberIdentificate: Yup.string()
      .min(6)
      .max(6, "Too long!")
      .required("Required"),
    email: Yup.string().email("Invalid email").required("Required"),
    mobileNumber: Yup.string()
      .min(9, "Must contains at least 9 numers")
      .max(15, "Too long number")
      .required("Required"),
  });

  async function createUser(fields, setSubmitting) {
    try {
      let api = new Api();
      api.createUser(fields);
      setSubmitting(false);
    } catch (error) {
      console.log(error);
      redirect.goBack();
    }
  }

  async function updateUser(id, fields, setSubmitting) {
    fields.UserId = parseInt(id);
    try {
      let api = new Api();
      api.updateUser(id, fields);
      setSubmitting(true);
      setTimeout(() => redirect.push("/user-manager"), 2500);
    } catch (error) {
      setSubmitting(false);
      console.log(error);
    }
  }

  async function fetchUser(id) {
    let api = new Api();
    const res = await api.fetchUser(id);
    setUser(res);
  }

  return {
    user,
    initialValues,
    validationSchema,
    createUser,
    updateUser,
    fetchUser,
  };
}
