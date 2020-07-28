import React, { useMemo, useState, useEffect } from "react";
import { Button } from "reactstrap";
import UserManagerTable from "../UserManagerTable/UserManagerTable";
import { Link } from "react-router-dom";
import axios from "axios";
import Loader from "react-loader-spinner";
import Swal from "sweetalert2";
import { useHistory } from "react-router-dom";

export default function UserManager() {
  const [data, setData] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  let history = useHistory();
  const USER_URL=process.env.REACT_APP_USER_API

  useEffect(() => {
    async function fetchUsers() {
      try {
        setIsLoading(true);
        const response = await axios.get(USER_URL);
        setData(response.data);
        setIsLoading(false);
      } catch (error) {
        Swal.fire("Oops...", "Something went wrong!", "error").then(() =>
          history.goBack()
        );
      }
    }
    fetchUsers();
  }, []);

  const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
      confirmButton: "btn btn-success",
      cancelButton: "btn btn-danger",
    },
    buttonsStyling: false,
  });

  function deleteUser(id) {
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
            url: USER_URL +"/" + id,
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
            "Your user has been deleted.",
            "success"
          ).then(()=>window.location.reload(false));
        } else if (result.dismiss === Swal.DismissReason.cancel) {
          swalWithBootstrapButtons.fire(
            "Cancelled",
            "Your user is safe :)",
            "error"
          );
        }
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
    <div>
      {isLoading ? (
        <div className="loader">
          <Loader type="Oval" color="#00BFFF" />
        </div>
      ) : (
        <UserManagerTable columns={columns} data={data} />
      )}
    </div>
  );
}
