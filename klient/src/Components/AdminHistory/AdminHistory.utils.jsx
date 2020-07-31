import { useState, useMemo } from "react";
import { useHistory } from "react-router-dom";
import Api from "../API";

export default function useAdminHistory() {
  let history = useHistory();
  const [data, setData] = useState([]);
  const [isLoading, setIsLoading] = useState(false);

  async function fetchUsersHistory() {
    setIsLoading(true);
    try {
      let api = new Api();
      const res = await api.fetchUsersHistory();
      setIsLoading(false);
      setData(res);
    } catch (error) {
      console.log(error);
      history.push(".");
    }
  }

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
        //accessor: "registrationNumber",
      },
      {
        Header: "Name",
       // accessor: "name",
      },
      {
        Header: "Surame",
       // accessor: "surname",
      },
      {
        Header: "Phone",
      //  accessor: "phone",
      },
      {
        Header: "Mail",
       // accessor: "mail",
      },
    ],
    []
  );

  return { isLoading, data, columns, fetchUsersHistory };
}
