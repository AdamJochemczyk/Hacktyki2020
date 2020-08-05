import Api from "./API";
import axios from "axios";

export default class LocationApi extends Api {
  constructor() {
    this.locationAxios = axios.create({
      baseURL: "https://localhost:44390/api",
    });
  }
  async fetchLocalization(reservationid) {
    try {
      //FIXME:
      //right reservation ID
      const res = await this.locationAxios.get("/locations/1");
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
