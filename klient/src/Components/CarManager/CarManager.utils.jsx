import React, { useMemo, useState } from "react";
import { Button } from "reactstrap";
import { Link, useHistory } from "react-router-dom";
import Api from "../API";
import Swal from "sweetalert2"

export default function useCarManager() {

  let history = useHistory();
  const [data, setData] = useState([]);
  const [isLoading, setIsLoading] = useState(false);

  const columns = useMemo(
    () => [
      {
        Header: "Car ID",
        accessor: "carId",
      },
      {
        Header: "Registration Number",
        accessor: "registrationNumber",
      },
      {
        Header: "Brand",
        accessor: "brand",
      },
      {
        Header: "Model",
        accessor: "model",
      },
      {
        Header: "Url to img",
        accessor: "imagePath",
      },
      {
        Header: "Year Of Production",
        accessor: "yearOfProduction",
      },
      {
        Header: "Actions",
        Cell: ({ row }) => (
          <div>
            <Button color="success">
              <Link
                to={{
                  pathname: "/car-manager/edit",
                  state: row.original.carId,
                }}
                style={{ textDecoration: "none", color: "white" }}
              >
                Edit
              </Link>
            </Button>
            <Button
              color="danger"
              onClick={() => deleteCar(row.original.carId)}
            >
              Delete
            </Button>
          </div>
        ),
      },
    ],
    []
  );

  async function fetchCars() {
      try{
    let api = new Api();
    setIsLoading(true);
    const res = await api.fetchCars();
    setData(res);
    setIsLoading(false)
      }catch(error){
        history.goBack()
          console.log(error)
      }
  }

  function deleteCar(id) {
    Swal
      .fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel!",
        reverseButtons: true,
      })
      .then((result) => {
        if (result.value) {
            try{
            let api = new Api()
            api.deleteCar(id)       
          Swal.fire(
            "Deleted!",
            "Your car has been deleted.",
            "success"
          ).then(()=>window.location.reload(false));
            }catch(error){
                console.log(error)
            }
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
            "Cancelled",
            "Your car is safe :)",
            "success"
          );
        }
      });

  }

  return { columns, data, isLoading, fetchCars };
}
