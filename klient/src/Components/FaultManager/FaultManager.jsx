import React, { useEffect } from "react";
import FaultManagerTable from "../FaultManagerTable/FaultManagerTable";
import Loader from "react-loader-spinner";
import useFaultManager from "./FaultManager.utils"

export default function FaultManager() {

  const {data,isLoading, columns, fetchFaults} = useFaultManager()


  useEffect(() => {
    fetchFaults()
    });

  return (
    <div>
      {isLoading ? (
        <div className="loader">
          <Loader type="Oval" color="#00BFFF" />
        </div>
      ) : (
        <FaultManagerTable columns={columns} data={data} />
      )}
    </div>
  );
}
