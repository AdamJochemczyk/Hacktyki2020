import React, { useMemo, useState, useEffect } from "react";
import { Button } from "reactstrap";
import { Link } from "react-router-dom";
import CarManagerTable from "../CarManagerTable/CarManagerTable";
import axios from "axios";
import Loader from "react-loader-spinner";
import { useHistory } from "react-router-dom";
import Swal from "sweetalert2";

export default function CarManager() {
  const [data, setData] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  let history = useHistory();

  useEffect(() => {
    async function fetchCars() {
      console.log("useEffect works now");
      try {
        setIsLoading(true);
        const response = await axios.get("https://localhost:44390/api/cars");
        setData(response.data);
        setIsLoading(false);
        return response;
      } catch (error) {
        Swal.fire("Oops...", "Something went wrong!", "error").then(() =>
          history.goBack()
        );
      }
    }
    fetchCars();
  }, []);

  const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
      confirmButton: "btn btn-success",
      cancelButton: "btn btn-danger",
    },
    buttonsStyling: false,
  });

  function deleteCar(id) {
    swalWithBootstrapButtons
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
          axios({
            url: "https://localhost:44390/api/cars/" + id,
            method: "DELETE",
          }).catch((error) => {
            Swal.fire(
              "Oops",
              "Something went wrong when deleting. Error:" + error.response,
              "error"
            );
          });
          swalWithBootstrapButtons.fire(
            "Deleted!",
            "Your car has been deleted.",
            "success"
          ).then(()=>window.location.reload(false));
        } else if (result.dismiss === Swal.DismissReason.cancel) {
          swalWithBootstrapButtons.fire(
            "Cancelled",
            "Your car is safe :)",
            "error"
          );
        }
      });

  }

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
        accessor: "urlToImg",
      },
      {
        Header: "Year Of Production",
        accessor: "yearOfProduction",
      },
      {
        Header: "Actions",
        Cell: ({ row }) => (
          <div>
            <Link
              to={"/car-manager/edit/" + row.original.carId}
              style={{ textDecoration: "none", color: "white" }}
            >
              <Button color="success">Edit</Button>
            </Link>
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

  return (
    <div>
      {isLoading ? (
        <div className="loader">
          <Loader type="Oval" color="#00BFFF" />
        </div>
      ) : (
        <CarManagerTable columns={columns} data={data} />
      )}
    </div>
  );
}
