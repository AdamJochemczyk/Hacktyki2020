import axios from "axios";
const BASE_URL = process.env.REACT_APP_BASE_URL;

export default class Api {

  //TODO:
  //refresh token
  constructor() {
    this.token = sessionStorage.getItem("accessToken");
    this.refreshToken = sessionStorage.getItem("refreshToken");
    this.baseAxios = axios.create({
      baseURL: BASE_URL,
      headers: {
        Authorization: `Bearer ${this.token}`,
      },
    });
  }
}
