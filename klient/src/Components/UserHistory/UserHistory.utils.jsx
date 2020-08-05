import React, { useState, useMemo } from "react"
import { useHistory } from "react-router-dom";
import moment from "moment";
import Swal from "sweetalert2";
import { Button } from "reactstrap";
import Api from "../API";

export default function useUserHistory(){

    const [data, setData] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    let history=useHistory()

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
            Header: "Actions",
            Cell: ({ row }) => (
              <div>
                {moment.utc(row.original.rentalDate).isBefore(moment().utc()) &&
                  moment
                    .utc(row.original.returnDate)
                    .add(14, "days")
                    .isBefore(moment().utc()) && 
                    <Button
                      color="primary"
                      onClick={() =>
                        reportProblem(
                          row.original.carId,
                          row.original.reservationId
                        )
                      }
                    >
                      Report problem
                    </Button>
                  }
                  { moment.utc().isBefore(moment.utc(row.original.rentalDate)) && 
                <Button
                  color="danger"
                  onClick={() =>
                    cancelReservation(row.original.reservationId)
                  }
                >
                  Cancel booking
                </Button>
                  }
              </div>
            ),
          },
        ],
        []
      );

      function cancelReservation(id) {
        Swal.fire({
          title: "Are you sure?",
          text: "You won't be able to revert this!",
          icon: "warning",
          showCancelButton: true,
          confirmButtonText: "Yes, delete reservation!",
          cancelButtonText: "No, cancel!",
          reverseButtons: true,
        }).then((result) => {
          if (result.value) {
              try{
                let api=new Api();
                api.cancelReservation(id)
              }catch(error){
                console.log(error)
              }
          } else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire("Cancelled", "Your reservation is safe :)", "success");
          }
        });
      }
  
      function reportProblem(carId, reservationId) {
        history.push({
          pathname: "/history/edit",
          state: { car: carId, reservation: reservationId },
        });
      }

      async function fetchUserHistory(id) {
        setIsLoading(true);
        try {
            setIsLoading(true)
            let api=new Api();
            const res= await api.fetchUserHistory(id)
          setIsLoading(false);
          setData(res);
        } catch (error) {
          history.push(".");
        }
      }   

      return {data, isLoading, columns, fetchUserHistory}
}