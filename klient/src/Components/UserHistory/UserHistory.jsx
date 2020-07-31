import React, { useEffect } from "react";
import TableUserHistory from "../TableUserHistory/TableUserHistory";
import Loader from "react-loader-spinner";
import useUserHistory from "./UserHistory.utils";

export default function UserHistory() {
  
  const {data, isLoading, columns, fetchUserHistory}=useUserHistory();

  
  //FIXME:
  //get userID from localstorage
  useEffect(() => {
    //fetchUserHistory(id);
    fetchUserHistory(28)
  }, []);


  return (
    <div>
      {isLoading ? (
        <div className="loader">
          <Loader type="Oval" color="#00BFFF" />
        </div>
      ) : (
        <TableUserHistory columns={columns} data={data} />
      )}
    </div>
  );
}
