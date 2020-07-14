import React from "react"
import { Button } from "reactstrap"

export default function Cardcar(props){
    
    function reserveThatCar(){
        
    }

    return (
        <div>
            {props.carbrand} - {props.carmodel}
            <img src={props.img} alt="car" />
            <p>{props.yearOfProduction}</p>
            <Button onClick={reserveThatCar}>Reserve me!</Button>
        </div>
    )
}