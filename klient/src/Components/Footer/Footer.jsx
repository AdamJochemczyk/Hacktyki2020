import React,  {useEffect} from "react"
import styles from './Footerstyle.module.css'; 
import useFooter from "./useFooter.utils"

export default function Footer(){

    const {year, getYear}=useFooter()

    useEffect(()=>{
         getYear() 
    },[])

    return ( 
        <div className={styles.global}>
            <p>Company Car Reservation - HACKTYKI <a href="https://euvic.pl/">Euvic</a> ©{year}</p>
        </div>
    )
}