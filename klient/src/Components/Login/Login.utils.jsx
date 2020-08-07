import * as Yup from "yup";
import Api from "../API/AuthorizationApi";
import jwt_decode from "jwt-decode";
import Swal from "sweetalert2";
import {useHistory} from "react-router-dom"

export default function useLogin() {

  let history=useHistory()
  let initialValues = {
    email: "",
    encodePassword: "",
  };

  const validationSchema = Yup.object().shape({
    encodePassword: Yup.string().required("Required"),
    email: Yup.string().email("This isn't email").required("Required"),
  });

  async function onSubmit(fields, { resetForm }) {
    signIn(fields);
    resetForm({ state: "" });
  }

  async function signIn(params) {
    try {
      let api = new Api();
      const response = await api.signIn(params);
      if (sessionStorage.getItem("isLoggedIn")) {
        let decodedToken = jwt_decode(response.accessToken);
        sessionStorage.setItem(
          "userRole",
          decodedToken[
            "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
          ]
        );
        sessionStorage.setItem("userID", decodedToken.sub);
        sessionStorage.setItem("accessToken", response.accessToken);
        sessionStorage.setItem("refreshToken", response.refreshToken);
        switch (sessionStorage.getItem("userRole")) {
          case "Worker":
            sessionStorage.setItem("userRole", "user");
            history.push('/home');
            window.location.reload()
            break;
          case "Admin":
            sessionStorage.setItem("userRole", "admin");
            break;
          default:
            Swal.fire("Oops...", "Something went wrong");
            break;
        }
        if (sessionStorage.getItem("userRole") === "Worker") {
          console.log(sessionStorage.getItem("userRole"));
        }
      }
    } catch (error) {
      Swal.fire("Oops...", "Something went wrong", "error");
    }
  }

  return { initialValues, validationSchema, signIn, onSubmit };
}
