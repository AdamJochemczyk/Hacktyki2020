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
        accessor: "car.registrationNumber",
      },
      {
        Header: "Name",
        accessor: "user.firstName",
      },
      {
        Header: "Surame",
        accessor: "user.lastName",
      },
      {
        Header: "Phone",
        accessor: "user.mobileNumber",
      },
      {
        Header: "Mail",
        accessor: "user.email",
      },
    ],
    []
  );

  return { isLoading, data, columns, fetchUsersHistory };
}
