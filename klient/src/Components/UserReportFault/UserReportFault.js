import React, { useState } from "react"
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import "../styles/componentsstyle.css"

export default function UserReportFault(){

    let [problemDesc, setProbDesc] = useState("")
    function handleTextChange(event){
      setProbDesc(event.target.value)
    }

    let [labelTxt, setLabelTxt]=useState("Describe it!")

    function ReportFaultToAPI(e){
      setLabelTxt("Thanks for report!")
      e.preventDefault()
      //Send report to API
      console.log(problemDesc)
      //after send
      setProbDesc('')
    }

    return(
        <Form>
        <h1>Report a problem</h1>
        <FormGroup>
        <Label style={labelTxt==='Thanks for report!' ? {color: 'green'} : null}>
        {labelTxt}</Label>
        <Input onChange={handleTextChange} type="textarea" name="text" value={problemDesc} />
      </FormGroup>
      <Button color="primary" size="large" onClick={ReportFaultToAPI}>Send report</Button>
      </Form>
    )
}