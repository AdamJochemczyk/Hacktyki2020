import axios from "axios"
import Api from "./API"
import Swal from "sweetalert2"

const REGISTER_URL=process.env.REACT_APP_REGISTER_API

export default class UserApi extends Api{

    constructor(){
      super()
      this.createAxios=axios.create({
        baseURL: REGISTER_URL,
      })
    }

    //TODO:
    //create user with token
   
    async createUser(params) {
        try {
          await this.createAxios.post('',params)
          Swal.fire("Good job!", "You successfully added new user!", "success");
        } catch (error) {
          console.log(error)
          /*if(error.response===500)
          {
            console.log("Srutututu")
          }
          else{
          console.log(error.response.data);
          }*/
        }
      }

      async fetchUsers() {
        try {
          const res= await this.baseAxios.get('/users').catch((error)=>console.log(error))
          return res.data;
        } catch (error) {
          Swal.fire("Oops...", "Something went wrong!", "error");
        }
      }

      async fetchUser(id) {
        try {
          const res = await this.baseAxios.get('/users/'+id)
          return res.data;
        } catch (error) {
          alert(error.message);
        }
      }
      

      async updateUser(id, fields) {
        try {
          await this.baseAxios.put('/users/'+id, fields)
          Swal.fire("Good job!", "You successfully edited a user!", "success");
        } catch (error) {
          console.log(error);
        }
      }
      
      async deleteUser(id) {
        try {
          await this.baseAxios.delete('/users/'+id)
        } catch (error) {
          console.log(error);
        }
      }
}
