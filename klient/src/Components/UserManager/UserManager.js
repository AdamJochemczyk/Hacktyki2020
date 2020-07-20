import React, { useMemo, useState, useEffect } from "react";
import { Button } from "reactstrap";
import UserManagerTable from "../UserManagerTable/UserManagerTable";
import { Link } from "react-router-dom";
import axios from "axios";

export default function UserManager() {
  const [data, setData] = useState([]);

  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const response = await axios.get("https://localhost:44390/api/users");
        setData(response.data);
      } catch (e) {
        console.log(e);
        setData(data);
      }
    };
    fetchUsers();
  });

  async function deleteUser(id) {
    await axios({
            url: 'https://localhost:44390/api/users/'+id,
            method: 'DELETE'
          }).then((res) => {
            if(res.status===200){
              alert("Deleted")
              window.location.reload(false)
            }
            }).catch(error => {
              console.log(error.response)
          });
  }

  const columns = useMemo(
    () => [
      {
        Header: "User ID",
        accessor: "userId",
      },
      {
        Header: "Name",
        accessor: "firstName",
      },
      {
        Header: "Surname",
        accessor: "lastName",
      },
      {
        Header: "Phone",
        accessor: "mobileNumber",
      },
      {
        Header: "Mail",
        accessor: "email",
      },
      {
          Header: "Verification",
          accessor: "statusofverification",
      },
      {
        Header: "Actions",
        Cell: ({ row }) => (
          <div>

            <Link to={"/UserManager/Edit/" + row.original.userId}><Button color="success">Edit</Button></Link>
            <Button
              color="danger"
              onClick={() => deleteUser(row.original.userId)}
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
      <UserManagerTable columns={columns} data={data} />
    </div>
  );
}
