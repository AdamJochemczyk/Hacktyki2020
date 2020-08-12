import React, { useState, useEffect} from "react"
import styles from './Footerstyle.module.css'; 

export default function Footer(){

    const [year,setYear]=useState(0);

    useEffect(()=>{
            const date = new Date().getFullYear();
            setYear(date); 
    },[year])

    return ( 
        <div className={styles.global}>
            <p>Company Car Reservation - HACKTYKI <a href="https://euvic.pl/">Euvic</a> ©{year}</p>
        </div>
    )
}