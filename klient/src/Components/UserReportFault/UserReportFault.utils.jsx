import {useState} from "react"
import * as Yup from "yup";
import { useHistory} from "react-router-dom"
import Api from "../API/FaultApi"
import Swal from "sweetalert2"

const useUserReportFault = ()=>{

    let redirect = useHistory()
  const initialValues={
    description: ''
  }

  const [isSended, setIsSended]=useState()

  const validationSchema = Yup.object().shape({
    description: Yup.string()
      .required("Required"),
  });
  async function onSubmit(fields, carId) {
    setIsSended(true)
    fields.carId=carId;
    fields.userId=parseInt(sessionStorage.getItem("userID"));
      try{
        let api=new Api()
        await api.createReport(fields)
        setIsSended(false)
        Swal.fire("Thank you!", 'You succesfully reported problem!', 'success')
        redirect.push('/')
      }
      catch(error){
        console.log(error)
        setIsSended(false)
        Swal.fire("Oops...", "Something went wrong...", "error").then(()=>redirect.goBack())
      }
  }

    return {initialValues, isSended, validationSchema, onSubmit}
}

export default useUserReportFault