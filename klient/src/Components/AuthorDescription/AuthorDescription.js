import React from "react"
import { Col } from "reactstrap"
import styles from './AuthorDescription.module.css'
export default function AuthorDesc(props)
{ 
    return (
        <Col sm="4">
        <img className={styles.avatar} src={props.src} alt="author profile"/>
            <br/>
            {props.name} - {props.role}
            </Col>
            )
}