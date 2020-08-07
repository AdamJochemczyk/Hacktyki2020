import Api from "./API";
import axios from "axios";

export default class LocationApi {
  constructor() {
    this.locationAxios = axios.create({
      baseURL: "https://localhost:44390/api",
    });
  }
  async fetchLocalization(reservationid) {
    try {
      const res = await this.locationAxios.get("/locations/"+reservationid);
      return res.data;
    } catch (error) {
      console.log(error);
    }
  }

  async setLocalization(props) {
    try {
      await this.locationAxios.post('/locations/', props)
    } catch (error) {
      console.log(error);
    }
  }
}
