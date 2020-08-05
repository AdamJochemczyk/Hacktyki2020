import axios from "axios"
import Swal from "sweetalert2"

export default class CarApi {

    constructor(){
        this.axiosInstance=axios.create({
            baseURL: "https://localhost:44390/api"
        })
    }

    async createCar(params) {
        try {
          await this.axiosInstance.post('/cars', params)
          Swal.fire("Good job!", "You successfully added new car!", "success");
        } catch (error) {
          Swal.fire("Oops...", "Something went wrong...", "error");
        }
      }

      async fetchCars() {
        try {
          const res = await this.axiosInstance.get('/cars');
          return res.data;
        } catch (error) {
          Swal.fire("Oops...", "Something went wrong!", "error");
        }
      }

      async fetchCar(id) {
        try {
          const res = await this.axiosInstance.get('/cars/'+id)
          return res.data;
        } catch (error) {
          alert(error.message);
        }
      }

      async updateCar(id, params) {
        try {
            await this.axiosInstance.put('/cars/'+id,params)
          Swal.fire("Good job!", "You successfully edited a car!", "success");
        } catch (error) {
          Swal.fire("Oops...", "Something went wrong", "error");
        }
      }
      
      async deleteCar(id) {
        try {
            await this.axiosInstance.delete('/cars/'+id)
        } catch (error) {
          console.log(error);
        }
      }
    
      async fetchCarByDate(startdate, enddate) {
        try {
          const res = await this.axiosInstance.get('/terms/'+ startdate + "/" + enddate)
          return res.data;
        } catch (error) {
          Swal.fire("Oops...", "Something went wrong!", "error");
        }
      }
}