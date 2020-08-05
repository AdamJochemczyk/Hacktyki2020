import axios from "axios";
const BASE_URL=process.env.REACT_APP_BASE_URL

export default class Api {
  //TODO:
  //get tokens from cookie and check if token valid
  constructor() {
    this.token ="eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImFkYW1qb2NoZUBnbWFpbC5jb20iLCJzdWIiOiI2dVpRb2N4MlRiQWFwb3loVWlZM1VHVG44SVRUMHAyampLelZMa3V2azB4RUJub3pvZ2pGTzlnak4vbVZreThYWmNtamV3a0hsVWxrOFVkemYzc3owRTk0bE12LzF6MzdEKy9tc1p4MTd5YlJDSFRPbVRrQkh2K0pRcXhZNEJraVVWQlllenNSdkd4a3FBa1FtenJoRER5SWFvSi9PZHUvTzk2SUdsdlNyTit1TU15UnpaUUR5M3hCOERGWFM4b01mMFFZSW44MWJYTEZZZVdHbFhsd3NZbEh0cEU5NlhVVnMzZmpQUmNzS3laRDFqTXRRUUN2aU9kRWtwYkQ5MjdKeDJudEozNnFBVXcrbjhYTjA4K05EaWxHV1hqK0hDQzd4akVTbWVLNnRNQzhSVjdycUZPMTNmcS8wRnY2NXcvV2RrZXVXKytIZUtUZEoxWEd5MWZqZ3c9PSIsImp0aSI6IldvcmtlciIsImV4cCI6MTU5NjYxNDUzMywiaXNzIjoiTXlBdXRoU2VydmVyIiwiYXVkIjoiTXlBdXRoQ2xpZW50In0.1o7SrrGfoYncY8OfsMOCQp1rIV7QguX2NA3w44y-Ra4";
    this.refreshToken = "bbb";
    this.baseAxios=axios.create({
      baseURL: BASE_URL,
      headers: {
        Authorization: `Bearer ${this.token}`,
      },
    })
  }
}
