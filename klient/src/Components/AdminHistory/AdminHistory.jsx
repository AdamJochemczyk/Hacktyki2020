import React, { useMemo, useState, useEffect } from "react";
import TableAdminHistory from "../TableAdminHistory/TableAdminHistory";
import axios from "axios";
import Swal from "sweetalert2"
import { useHistory } from "react-router-dom";
import Loader from "react-loader-spinner";

export default function AdminHistory() {
  const BASE_URL = process.env.REACT_APP_RESERVATION_API;
  const [data, setData] = useState([]);
  let history = useHistory();
  const [isLoading, setIsLoading]=useState(false)

 //TODO:
 //right accessors to data
 
  useEffect(() => {
    async function fetchUserHistory() {
      setIsLoading(true);
      try {
        const res = await axios({
          method: "GET",
          url: BASE_URL,
        });
        setIsLoading(false);
        setData(res.data);
      } catch (error) {
        Swal.fire("Oops...","Something went wrong", "error");
        history.push('.')
      }
    }
    fetchUserHistory();
  }, []);

  const columns = useMemo(
    () => [
      {
        Header: "Reservation ID",
        accessor: "reservationId",
      },
      {
        Header: "Rental Date",
        accessor: "rentalDate",
      },
      {
        Header: "Return Date",
        accessor: "returnDate",
      },
      {
        Header: "Registration Number",
        accessor: "car.registrationNumber",
      },
      {
        Header: "Name",
        //accessor: "user.name",
      },
      {
        Header: "Surame",
        //accessor: "user.surname",
      },
      {
        Header: "Phone",
        //accessor: "user.phone",
      },
      {
        Header: "Mail",
        //accessor: "user.mail",
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
        <TableAdminHistory columns={columns} data={data} />
      )}
    </div>);
}
