import React, { useState, useMemo } from "react";
import { useHistory } from "react-router-dom";
import Api from "../API/ReservationApi";
import moment from "moment"
import {Button} from "reactstrap"

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

  async function returnNow(reservationId){
    let api=new Api()
    await api.cancelReservation(reservationId)
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
        Header: "Surname",
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
      {
        Header: "Actions",
        Cell: ({ row }) => (
          <div>
            {moment().utc().isBefore(moment.utc(row.original.returnDate)) &&
                <Button
                  color="primary"
                  onClick={() =>
                    returnNow(
                      row.original.reservationId
                    )
                  }
                >
                  Return
                </Button>
              }
          </div>
        ),
      },
    ],
    []
  );

  return { isLoading, data, columns, fetchUsersHistory };
}
