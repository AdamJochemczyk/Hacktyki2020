import axios from "axios";
import { useHistory } from "react-router-dom";

export default class Api {
  //TODO:
  //refresh token

  // constructor() {
  //   this.history = useHistory();
  //   this.baseAxios = axios.create({
  //     baseURL: process.env.REACT_APP_BASE_URL,
  //   });

  //   // Request interceptor
  //   this.baseAxios.interceptors.request.use(
  //     (config) => {
  //       const token = sessionStorage.getItem("accessToken");
  //       if (token) {
  //         config.headers["Authorization"] = "Bearer " + token;
  //       }
  //       return config;
  //     },
  //     (error) => {
  //       Promise.reject(error);
  //     }
  //   );

  //   //Response interceptor

  //   this.baseAxios.interceptors.response.use(
  //     (response) => {
  //       return response;
  //     },
  //     function (error) {
  //       const originalRequest = error.config;

  //       if (
  //         (error.response.status === 401 || error.response.status === 403) &&
  //         originalRequest.url === process.env.REACT_APP_BASE_URL + "/refresh"
  //       ) {
  //         //this.history.push("/");
  //         return Promise.reject(error);
  //       }

  //       if (
  //         (error.response.status === 401 || error.response.status === 403) &&
  //         !originalRequest._retry
  //       ) {
  //         originalRequest._retry = true;
  //         const refreshToken = sessionStorage.getItem("refreshToken");
  //         return this.baseAxios
  //           .post("/refresh", {
  //             RefreshToken: refreshToken,
  //           })
  //           .then((res) => {
  //             if (res.status === 201) {
  //               sessionStorage.setItem("accessToken", res.accessToken);
  //               sessionStorage.setItem("refreshToken", res.refreshToken);
  //               this.baseAxios.defaults.headers.common["Authorization"] =
  //                 "Bearer " + sessionStorage.setItem("accessToken");
  //               return axios(originalRequest);
  //             }
  //           });
  //       }
  //       return Promise.reject(error);
  //     }
  //   );
  // }

  //without interceptor
  constructor(){
    this.token = sessionStorage.getItem("accessToken");
    this.refreshToken = sessionStorage.getItem("refreshToken");
    this.baseAxios = axios.create({
      baseURL: process.env.REACT_APP_BASE_URL,
      headers: {
        Authorization: `Bearer ${this.token}`,
      },
    })
  };
}
