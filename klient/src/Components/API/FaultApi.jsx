import axios from "axios";

export default class FaultApi {
  constructor() {
    this.faultAxios = axios.create({
      baseURL: "",
    });
  }

  async createReport(fields){
      try{
        await this.faultAxios.post('',fields)
      }catch(error){
        console.log(error)
      }
  }

  async fetchFaults(){
    try {
        const response = await this.faultAxios.get('')
        return response.data
    }catch (error){
        console.log(error)
    }
  }
  async updateStatus(fields) {
    try {
      await this.faultAxios.put("", fields);
    } catch (error) {
      console.log(error);
    }
  }
  async deleteFault(id) {
    try {
      await this.faultAxios.delete("/" + id);
    } catch (error) {
      console.log(error);
    }
  }
}
