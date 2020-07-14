import React, { useMemo } from "react"
import TableAdminHistory from '../TableAdminHistory/TableAdminHistory'

export default function AdminHistory(){

    const reservations = [{
        reservationId: '1',
        rentalDate: '12',
        returnDate: '12',
        registrationNumber: 'KTW22',
        name: 'Aaa',
        surname: 'BBB',
        phone: '123455',
        mail: 'aaa@aaa.pl'
    },
    {
        reservationId: '2',
        rentalDate: '13',
        returnDate: '13',
        registrationNumber: 'KTWaa',
        name: 'Aaa',
        surname: 'ccc',
        phone: '123455',
        mail: 'bbb@bbb.pl'
    }]

   /* const [data, setData] = useState([]);

    // Using useEffect to call the API once mounted and set the data
    useEffect(() => {
    (async () => {
      const result = await axios("https://api.tvmaze.com/search/shows?q=snow");
      setData(result.data);
    })();
    }, []);*/

    const columns = useMemo(
        () => [
              {
                Header: "Reservation ID",
                accessor: "reservationId"
              },
              {
                Header: "Rental Date",
                accessor: "rentalDate"
              },
              {
                Header: "Return Date",
                accessor: "returnDate"
              },
              {
                Header: "Registration Number",
                accessor: "registrationNumber"
              },
              {
                Header: "Name",
                accessor: "name"
              },
              {
                Header: "Surname",
                accessor: "surname"
              },
              {
                  Header: "Phone",
                  accessor: "phone"
              },
              {
                  Header: "Mail",
                  accessor: 'mail'
              }
            ],
        []
      );

    return(
        <TableAdminHistory columns={columns} data={reservations} />
    )
}