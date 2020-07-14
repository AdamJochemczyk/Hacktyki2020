import React, { useState } from "react"
import {Form, Button, Input, Label} from "reactstrap"
import "../styles/componentsstyle.css"

export default function AddCar(){

    const [Car, setCar]=useState({
        Brand: '',
        RegistrationNumber: '',
        Model: '',
        TypeOfCar: '',
        YearOfProcuction: '',
        NumberOfSits: '',
        NumberOfDoors: '',
        ImgSrc: ''
    })

    const handleFormInput = e => {
        setCar({
            ...Car,
            [e.target.name]: e.target.value
        })
    }

    const AddCar = e =>{
        e.preventDefault();
        console.log(Car)
        //send data to API

        //aftersend
        setCar({
            Brand: '',
            RegistrationNumber: '',
            Model: '',
            TypeOfCar: '',
            YearOfProcuction: '',
            NumberOfSits: '',
            NumberOfDoors: '',
            ImgSrc: ''
        })
    }

    return (
        <Form onSubmit={AddCar}>
        <h4>Add Car</h4>
            <Label>Brand</Label>
            <Input type="text" name="Brand" placeholder="Brand" onChange={handleFormInput} value={Car.Brand}/>
            <Label>Registration number</Label>
            <Input type="text" name="RegistrationNumber" placeholder="Registration number" onChange={handleFormInput} value={Car.RegistrationNumber}/>
            <Label>Model</Label>
            <Input type="text" name="Model" placeholder="Model" onChange={handleFormInput} value={Car.Model}/>
            <Label>Type of car</Label>
            <Input type="select" name="TypeOfCar" onChange={handleFormInput} value={Car.TypeOfCar}>
            <option value="sportCar">Sport Car</option>
            <option value="classicCar">Classic Car</option>
            <option value="retroCar">Retro Car</option>
            </Input>
            <Label>Year Of Procuction</Label>
            <Input type="number" name="YearOfProcuction" placeholder="Year of production" onChange={handleFormInput} value={Car.YearOfProcuction}/>
            <Label>Number of sits</Label>
            <Input type="number" name="NumberOfSits" onChange={handleFormInput} value={Car.NumberOfSits}/>
            <Label>Number of doors</Label>
            <Input type="number" name="NumberOfDoors" onChange={handleFormInput} value={Car.NumberOfDoors}/>
            <Label>Img source</Label>
            <Input type="text" name="ImgSrc" placeholder="Img src" onChange={handleFormInput} value={Car.ImgSrc}/>
            <Button color="success" type="submit" style={{marginTop: "15px"}} onClick={AddCar}>Add car</Button>
        </Form>
    )
}