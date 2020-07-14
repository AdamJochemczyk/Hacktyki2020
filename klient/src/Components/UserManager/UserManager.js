import React, { useMemo } from "react"
import {Button} from "reactstrap"
import UserManagerTable from '../UserManagerTable/UserManagerTable'
import {Link} from "react-router-dom"

export default function UserManager(){


     /* const [data, setData] = useState([]);

// Using useEffect to call the API once mounted and set the data
useEffect(() => {
(async () => {
  const result = await axios("https://api.tvmaze.com/search/shows?q=snow");
  setData(result.data);
})();
}, []);*/

    async function deleteUser(id){
        /*await axios({
            url: '',
            method: 'DELETE',
            headers: {
              Authorization: `${token}`,
            },
          });*/
    }

    const data = [{
        userId: '1',
        name: 'Adam',
        surname: 'Aa',
        phone: '33',
        mail: '44',
    }]

    const columns = useMemo(
        ()=>[
            {
                Header: "User ID",
                accessor: "userId"
            },
            {
                Header: "Name",
                accessor: "name",
            },
            {
                Header: "Surname",
                accessor: "surname",
            },
            {
                Header: "Phone",
                accessor: "phone"
            },
            {
                Header: "Mail",
                accessor: "mail"
            },
            {
                Header: "Actions",
                Cell: ({row}) =>(
                    <div>
                    <Link to={'/UserManager/Edit/'+row.original.userId} >Edit</Link>
                    <Button color="danger" onClick={()=>deleteUser(row.original.userId)}> Delete </Button>
                    </div>  
                ),
            },
        ],
        []
    );

return (<div>
    <UserManagerTable columns={columns} data={data} />  
    </div>
    );
}