import React, {useMemo, useState} from "react"
import { Link, useHistory } from "react-router-dom";
import { Button } from "reactstrap";
import Api from "../API"
import Swal from "sweetalert2"

export default function useUserManager(){

    let history=useHistory();
    const [data, setData] = useState([]);
    const [isLoading, setIsLoading] = useState(false);

    async function fetchUsers() {
      try{
    let api = new Api();
    setIsLoading(true);
    const res = await api.fetchUsers();
    setData(res);
    setIsLoading(false)
      }catch(error){
       // history.goBack()
          console.log(error)
      }
  }

  function deleteUser(id) {
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
            api.deleteUser(id)       
          Swal.fire(
            "Deleted!",
            "Your user has been deleted.",
            "success"
          ).then(()=>window.location.reload(false));
            }catch(error){
                console.log(error)
            }
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
            "Cancelled",
            "Your user is safe :)",
            "success"
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
              <Button color="success">
              <Link
                  to={{
                    pathname: "/user-manager/edit",
                    state: row.original.userId
                    }}
                  style={{ textDecoration: "none", color: "white" }}
                >
                  Edit
                </Link>
                </Button>
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

      return {columns,data,fetchUsers,isLoading}
}