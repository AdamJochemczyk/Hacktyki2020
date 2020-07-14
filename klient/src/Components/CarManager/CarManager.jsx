import React, { useMemo } from "react";
import { Button } from "reactstrap";
import { Link } from "react-router-dom";
import CarManagerTable from "../CarManagerTable/CarManagerTable";

export default function CarManager() {
  const data = [
    {
      carId: "1",
      registrationNumber: "STA123456",
      model: "X",
      brand: "Tesla",
      urlToImg: "https://costam",
      yearOfProduction: "2020",
    },
  ];

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
            <Button color="primary">
              <Link
                to={"/CarManager/Edit/" + row.original.carId}
                style={{ textDecoration: "none", color: "white" }}
              >
                Edit
              </Link>
            </Button>
          </div>
        ),
      },
    ],
    []
  );

  /* const [data, setData] = useState([]);

// Using useEffect to call the API once mounted and set the data
useEffect(() => {
(async () => {
  const result = await axios("https://api.tvmaze.com/search/shows?q=snow");
  setData(result.data);
})();
}, []);*/

  return <CarManagerTable columns={columns} data={data} />;
}
