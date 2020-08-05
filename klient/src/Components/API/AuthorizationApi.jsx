import axios from "axios";
import Swal from "sweetalert2"

const BASE_URL = process.env.REACT_APP_BASE_URL;

export default class AuthorizationApi {
  constructor() {
    this.authorizationAxios = axios.create({
      baseURL: BASE_URL,
    });
  }

  async signIn(params) {
    try {
      const response = await this.authorizationAxios.post(
        "/authorization/signin",
        params
      );
      return response.data;
    } catch (error) {
      console.log(error);
    }
  }

  async sendPassword(fields) {
    try {
      await this.authorizationAxios.put("/authorization", fields);
      Swal.fire("Good job!", "You succesfully set your password!", "success");
    } catch (error) {
      console.log(error.response.data);
    }
  }
}
