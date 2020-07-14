import React from "react"
import { useParams } from 'react-router-dom'
import {Form,Input,Label} from "reactstrap"

export default function EditUser(){

    let {id}=useParams()

   // const [data, setData] = useState();

/*// Using useEffect to call the API once mounted and set the data
useEffect(() => {
(async () => {
  const result = await axios("api"+{id});
  setData(result.data);
})();
}, data);

    async function editUser(){
        await axios({
        url: 'api',
        method: 'PUT',
        headers: {
          Authorization: `Bearer ${tokenResponse.data.token}`,
        },
        data: {
          email: 'qwe.asd@euvic.pl',
        },
      });
    }
*/

    return (<h1>Edit</h1>)
    /*<Form>
        <h1>Edit user</h1>
        <Label>Name</Label>
        <Input type="text" name="name" value={data.name}/>
        <Label>Surname</Label>
        <Input type="text" name="name" value={data.surname}/>
        <Label>Phone</Label>
        <Input type="number" name="name" value={data.phone}/>
        <Label>Mail</Label>
        <Input type="mail" name="name" value={data.mail} onClick={editUser}/>
    </Form>*/
}