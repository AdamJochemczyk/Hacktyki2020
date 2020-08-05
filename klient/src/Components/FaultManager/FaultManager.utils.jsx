import React, {useState, useMemo} from "react";
import { Input } from "reactstrap";
import {useHistory} from "react-router-dom";
import Api from "../API/FaultApi"
import Swal from "sweetalert2"

export default function useFaultManager(){

    let history=useHistory()
    const [data, setData] = useState([]);
      const [isLoading, setIsLoading] = useState(false);

      async function deleteFault(id){
        try{
            let api= new Api()
            await api.deleteFault(id)
      }catch(error){
        console.log(error)
      }
      }
  
      async function updateStatus(id){
        try{
            let api = new Api()
            await api.updateStatus(id)
        }catch(error){
          console.log(error)
        }
      }

      async function fetchFaults(){
        try {
        let api = new Api()
       setIsLoading(true)
       const response=await api.fetchFaults()
       setData(response);
       setIsLoading(false)
     } catch (error) {
       Swal.fire("Oops...", "Something went wrong!", "error").then(() =>
         history.goBack()
       );
     }
     }
  
    function handleChange(id, status) {
      switch(status) {
        case "all okay":
          deleteFault(id)
          break;
          case "in progress":
          updateStatus(id)
          break;
          case "to check":
            Swal.fire("Whooa!","You should pick in progress or all okay status","question")
            break;
        default:
          Swal.fire("Oops...","Something went wrong","error")
      }
    }
  
    const columns = useMemo(
      () => [
        {
          Header: "Registration number",
          accessor: "registrationNumber",
        },
        {
          Header: "Description",
          accessor: "description",
        },
        {
          Header: "Name",
          accessor: "name",
        },
        {
          Header: "Surname",
          accessor: "surname",
        },
        {
          Header: "Phone",
          accessor: "phone",
        },
        {
          Header: "Date of report",
          accessor: "dateofreport",
        },
        {
          Header: "Status",
          accessor: "status",
          Cell: ({ row }) => (
            <Input
              type="select"
              defaultValue={row.original.status}
              onChange={(e) => handleChange(row.original.faultId, e.target.value)}
            >
              <option value="to check"> to check </option>
              <option value="all okay"> all okay </option>
              <option value="in progress"> in progress </option>
            </Input>
          ),
        },
      ],
      []
    );

    return {data,isLoading, columns, fetchFaults}
}