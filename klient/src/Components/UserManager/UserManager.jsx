import React, { useMemo, useState, useEffect } from "react";
import { Button } from "reactstrap";
import UserManagerTable from "../UserManagerTable/UserManagerTable";
import { Link } from "react-router-dom";
import axios from "axios";
import Loader from "react-loader-spinner"
import Swal from "sweetalert2"
import {useHistory} from "react-router-dom"

export default function UserManager() {
  const [data, setData] = useState([]);
  const [isLoading, setIsLoading]=useState(false)
  let history=useHistory()

  const fetchUsers = async () => {
    try {
      setIsLoading(true)
      const response = await axios.get("https://localhost:44390/api/users");
      setData(response.data);
      setIsLoading(false)
    } catch (error) {
      Swal.fire('Oops...', 'Something went wrong!', 'error').then(()=>history.goBack())
    }
  }
  useEffect(() => {
    fetchUsers();
    },[]);

    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
      },
      buttonsStyling: false
    })

   function deleteUser(id) {
    swalWithBootstrapButtons.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, cancel!',
      reverseButtons: true
    }).then((result) => {
      if (result.value) {
        axios({
          url: "https://localhost:44390/api/users/" + id,
          method: "DELETE",
        }).catch((error) => {
            Swal.fire("Oops", "Something went wrong when deleting. Error:"+error.response, "error");
          });
        swalWithBootstrapButtons.fire(
          'Deleted!',
          'Your user has been deleted.',
          'success'
        )
        setTimeout(()=>fetchUsers(),2000);
      } else if (
        result.dismiss === Swal.DismissReason.cancel
      ) {
        swalWithBootstrapButtons.fire(
          'Cancelled',
          'Your user is safe :)',
          'error'
        )
      }
    })
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
        Header: "Actions",
        Cell: ({ row }) => (
          <div>
            <Link to={"/user-manager/edit/" + row.original.userId}>
              <Button color="success">Edit</Button>
            </Link>
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
    <div>{isLoading ? <div className="loader">
      <Loader type="Oval" color="#00BFFF" height={80} width={80} />
    </div> : 
      <UserManagerTable columns={columns} data={data} />}
    </div>
  );
}
