import axios from "axios";
import Swal from "sweetalert2"

export default class FaultApi {
  constructor() {
    this.faultAxios = axios.create({
      baseURL: process.env.REACT_APP_BASE_URL,
    });
  }

  async createReport(fields){
      try{
        await this.faultAxios.post('/defects',fields)
      }catch(error){
        Swal.fire("Oops..",error,"error")
      }
  }

  async fetchFaults(){
    try {
        const response = await this.faultAxios.get('/defects')
        return response.data
    }catch (error){
        console.log(error)
    }
  }
  async updateStatus(fields) {
    try {
      await this.faultAxios.put('/defects', fields);
    } catch (error) {
      console.log(error);
    }
  }
  async deleteFault(fields) {
    try {
      await this.faultAxios.put('/defects', fields);
    } catch (error) {
      console.log(error);
    }
  }
}
