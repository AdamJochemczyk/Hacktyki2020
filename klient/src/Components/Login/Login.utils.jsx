import * as Yup from "yup";
import Api from "../API";

export default function useLogin(){

    let initialValues = {
        email: "",
        encodePassword: "",
      };

      const validationSchema = Yup.object().shape({
        encodePassword: Yup.string().required("Required"),
        email: Yup.string().email("This isn't email").required("Required"),
      });
    
      async function onSubmit(fields, {resetForm}){
          signIn(fields)
          resetForm({state: ''})
        }

      async function signIn(params) {
        try {
          let api = new Api();
          const response= await api.signIn(params)
          console.log(response)
        } catch (error) {
        console.log("signIn -> error", error)  
        }
      }

      return {initialValues, validationSchema, signIn, onSubmit}
}