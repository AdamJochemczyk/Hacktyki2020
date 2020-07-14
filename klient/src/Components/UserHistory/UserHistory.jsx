import React, { useMemo } from "react";
import TableUserHistory from "../TableUserHistory/TableUserHistory";
import { Button } from "reactstrap";
import { Link } from "react-router-dom";

export default function UserHistory() {
  const data = [
    {
      reservationId: "3",
      rentalDate: "12",
      returnDate: "13",
      registrationNumber: "STA",
    },
    {
      reservationId: "4",
      rentalDate: "12",
      returnDate: "14",
      registrationNumber: "STW",
    },
  ];

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
        accessor: "registrationNumber",
      },
      {
        Header: "Actions",
        Cell: ({ row }) => (
          <div>
            <Button color="primary">
              <Link
                to={"/History/Edit/" + row.original.registrationNumber}
                style={{ textDecoration: "none", color: "white" }}
              >
                Report problem
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

  return <TableUserHistory columns={columns} data={data} />;
}
