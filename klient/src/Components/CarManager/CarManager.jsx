import React, { useMemo, useState, useEffect } from "react";
import { Button } from "reactstrap";
import { Link } from "react-router-dom";
import CarManagerTable from "../CarManagerTable/CarManagerTable";
import axios from "axios";

export default function CarManager() {
  const [data, setData] = useState([]);

  const fetchCars = async () => {
    try {
      const response = await axios.get("https://localhost:44390/api/cars");
      setData(response.data);
    } catch (e) {
      console.log(e);
      setData(data);
    }
  };
  useEffect(() => {
    fetchCars();
  }, []);

  async function deleteCar(id) {
    await axios({
      url: "https://localhost:44390/api/cars/" + id,
      method: "DELETE",
    })
      .then((res) => {
        if (res.status === 200) {
          alert("Deleted");
          window.location.reload(false);
        }
      })
      .catch((error) => {
        console.log(error.response);
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

  return <CarManagerTable columns={columns} data={data} />;
}