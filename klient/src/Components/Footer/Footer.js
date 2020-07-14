import React, { useState, useEffect} from "react"
import { Row } from "reactstrap"
import styles from './Footerstyle.module.css'; 
import AuthorDesc from "../AuthorDescription/AuthorDescription";

const authors =[
    {id: 1, name: "Żaneta Borowska", role: "backend", src: "https://scontent.fwaw7-1.fna.fbcdn.net/v/t1.0-9/16298446_1290156867698087_3066057403762340950_n.jpg?_nc_cat=101&_nc_sid=09cbfe&_nc_ohc=EqX7hMQAnrYAX9LSpzm&_nc_ht=scontent.fwaw7-1.fna&oh=d33c5d6b5ebfeb8a18c235173393dbe3&oe=5F2F29C4"},
    {id: 2, name: "Bogdan Kucher", role: "backend", src: "https://scontent.fwaw7-1.fna.fbcdn.net/v/t31.0-8/27355853_1860336884276612_8053680163315302839_o.jpg?_nc_cat=110&_nc_sid=09cbfe&_nc_ohc=KxYhnQXwEAoAX_F6r8s&_nc_ht=scontent.fwaw7-1.fna&oh=29b0b853e43d2bbaa30318fec93a1d15&oe=5F2FC141"},
    {id: 3, name: "Adam Jochemczyk", role: "frontend", src: "https://scontent.fwaw7-1.fna.fbcdn.net/v/t1.0-9/67827335_1114327365428495_5081469124652040192_o.jpg?_nc_cat=104&_nc_sid=09cbfe&_nc_ohc=ej58UdRJi44AX9behFw&_nc_ht=scontent.fwaw7-1.fna&oh=71587f2327ab5fc4138eaca00e7e471c&oe=5F30B3B5"}
];
function CreateAuthor(authors){
    return (
        <AuthorDesc 
        key={authors.id}
        name={authors.name}
        role={authors.role}
        src={authors.src}
        />
    )
}

export default function Footer(){

    const [year,setYear]=useState(0);

    useEffect(()=>{
            const date = new Date().getFullYear();
            setYear(date); 
    },[year])

    return ( 
        <div className={styles.global}>
        <h5 className={styles.author}>Authors</h5>
        <hr />
        <Row>
        {authors.map(CreateAuthor)}
        </Row>
        <hr/>
        <div>
            <p>Company Car Reservation - HACKTYKI <a href="https://euvic.pl/">Euvic</a> ©{year}</p>
        </div>
        </div>
    )
}