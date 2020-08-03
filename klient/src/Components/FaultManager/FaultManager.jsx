import React, { useMemo, useState, useEffect } from "react";
import { Input } from "reactstrap";
import FaultManagerTable from "../FaultManagerTable/FaultManagerTable";
import Swal from "sweetalert2";
import { useHistory } from "react-router-dom";
import Loader from "react-loader-spinner";
import axios from "axios";

export default function FaultManager() {
  //TODO:
  //API CREATE, GET, PUT, DELETE
  const [data, setData] = useState();
  const [isLoading, setIsLoading] = useState(false);
  let history = useHistory();

  /*useEffect(() => {
    async function fetchFaults(){
       try {
      setIsLoading(true)
      const response = await axios({
        url: '',
        metod: "GET"});
      setData(response.data);
      setIsLoading(false)
    } catch (error) {
      Swal.fire("Oops...", "Something went wrong!", "error").then(() =>
        history.goBack()
      );
    }
    }
    fetchFaults()
    },[]);*/
    /*const data= [{
      reservationId: 1,
      status: "to check"
    }]*/

    async function deleteFault(id){
      try{
      await axios({
        url: '',
        method: "DELETE",
        data: id
      }).catch((error)=>
      Swal.fire("Oops...", error.message,"error"))
    }catch(error){
      console.log(error)
    }
    }

    async function updateStatus(id){
      const fields={
        id: id,
        status: "in progress"
      }
      try{
        await axios({
          url: '',
          method: "PUT",
          data: fields
        }).catch((error)=>
        Swal.fire("Oops...", error.message,"error"))
      }catch(error){
        console.log(error)
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

  return (
    <div>
      {isLoading ? (
        <div className="loader">
          <Loader type="Oval" color="#00BFFF" />
        </div>
      ) : (
        <FaultManagerTable columns={columns} data={data} />
      )}
    </div>
  );
}
