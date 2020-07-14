import React from "react"
import UserHistory from "../UserHistory/UserHistory"
import AdminHistory from "../AdminHistory/AdminHistory"

export default function History(){

    const userrole = 0;

    return (
        (userrole===0? <UserHistory />: <AdminHistory />)
    )
}