import axios from "axios";
const BASE_URL = process.env.REACT_APP_BASE_URL;

export default class Api {
  //TODO:
  //get tokens from cookie and check if token valid
  constructor() {
    this.token = sessionStorage.getItem("accesstoken");
    console.log(this.token);
    this.refreshToken = "bbb";
    this.baseAxios = axios.create({
      baseURL: BASE_URL,
      headers: {
        Authorization: `Bearer ${this.token}`,
      },
    });
  }
}
