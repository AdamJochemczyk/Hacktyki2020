import React, { useMemo } from "react";
import { Input } from "reactstrap";
import FaultManagerTable from "../FaultManagerTable/FaultManagerTable";
import Swal from 'sweetalert2'
import {useHistory} from "react-router-dom"

export default function FaultManager() {
  /* const [data, setData] = useState([]);
  const [isLoading, setIsLoading]=useState(false)

  useEffect(() => {
    async function fetchFaults(){
       try {
      setIsLoading(true)
      const response = await axios.get(API);
      setData(response.data);
      setIsLoading(false)
    } catch (error) {
      Swal.fire("Oops...", "Something went wrong!", "error").then(() =>
        history.goBack()
      );
    }
    }
    fetchFaults()
    },[]); */

  const data = [
    {
      faultId: "1",
      registrationNumber: "STA22345",
      description: "motor cant start",
      name: "Grzegorz",
      surname: "Bąk",
      phone: "123456789",
      dateofreport: "12.01.2020",
      status: "to check",
    },
    {
      faultId: "2",
      registrationNumber: "STA22345",
      description: "motor cant start",
      name: "Grzegorz",
      surname: "Bąk",
      phone: "123456789",
      dateofreport: "13.01.2020",
      status: "all okay",
    },
  ];

  const columns = useMemo(
    () => [
      {
        Header: "Registration number",
        accessor: "registrationNumber",
      },
      {
        Header: "Description",
        accessor: "description",
      },
      {
        Header: "Name",
        accessor: "name",
      },
      {
        Header: "Surname",
        accessor: "surname",
      },
      {
        Header: "Phone",
        accessor: "phone",
      },
      {
        Header: "Date of report",
        accessor: "dateofreport",
      },
      {
        Header: "Status",
        accessor: "status",
        Cell: ({ row }) => (
          <Input
            type="select"
            defaultValue={row.original.status}
            onChange={(e) => console.log(e.target.value)}
          >
            <option value="to check"> to check </option>
            <option value="all okay"> all okay </option>
            <option value="in progress"> in progress </option>
          </Input>
        ),
      },
    ],
    []
  );

  return <FaultManagerTable columns={columns} data={data} />;
}
