import axios from "axios";

export default class Api {

  constructor() {

    this.baseAxios = axios.create({
      baseURL: process.env.REACT_APP_BASE_URL,
    });

    // Request interceptor
    this.baseAxios.interceptors.request.use(
      (config) => {
        const token = sessionStorage.getItem("accessToken");
        return token
          ? {
              ...config,
              headers: { ...config.headers, Authorization: `Bearer ${token}`,  ContentType: 'application/json' },
            }
          : config;
      },
      (error) => {
        Promise.reject(error);
      }
    );
    
    //Response interceptor

    this.baseAxios.interceptors.response.use(
      (response) => {
        return response;
      },
      async (error)=> {
        console.log(error)
        const originalRequest = error.config;
        console.log(error.headers)
        console.log(originalRequest)

        if (
          (error.response.status === 401 || error.response.status === 403) &&
          originalRequest.url === process.env.REACT_APP_BASE_URL + "/refresh"
        ) {
          return Promise.reject(error);
        }

        if (
          (error.response.status === 401 || error.response.status === 403) &&
          !originalRequest._retry
        ) {
          originalRequest._retry = true;
          const refreshToken = sessionStorage.getItem("refreshToken");
          const res = await this.baseAxios
            .post("/refresh", {
              RefreshToken: refreshToken,
            })
            console.log(res)
              if (res.status === 200) {
                sessionStorage.setItem("accessToken", res.accessToken);
                sessionStorage.setItem("refreshToken", res.refreshToken);
                this.baseAxios.defaults.headers.common["Authorization"] =
                  "Bearer " + sessionStorage.setItem("accessToken");
                return axios(originalRequest);
              } else {
                return error;
              }
            };
        return Promise.reject(error);
      }
    );
  }
}