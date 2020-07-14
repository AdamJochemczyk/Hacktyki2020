import React from "react"
import { useParams } from 'react-router-dom'

export default function EditCar(){

    let {id}=useParams()

    // const [data, setData] = useState();

/*// Using useEffect to call the API once mounted and set the data
useEffect(() => {
(async () => {
  const result = await axios("api"+{id});
  setData(result.data);
})();
}, data);
*/
    return (<h1>Edit Car{id}</h1>)
   /* <Form>
        <h1>Edit car</h1>
        <Label>Registration number</Label>
        <Input type="text" name="registrationnumber" value={data.registrationnumber}/>
        <Label>Url to img</Label>
        <Input type="text" name="urltoimg" value={data.urlToImg}/>
    </Form>*/
}