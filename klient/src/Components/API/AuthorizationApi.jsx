import Swal from "sweetalert2"
import Api from "./API";

export default class AuthorizationApi extends Api{

  async signIn(params) {
    try {
      const response = await this.baseAxios.post(
        "/authorization/signin",
        params
      );
      sessionStorage.setItem("isLoggedIn", true)
      return response.data;
    } catch (error) {
      if (error.response) {
        Swal.fire("Oops...", error.response.data, "error");
      }
    }
  }

  async sendPassword(fields) {
    try {
      await this.baseAxios.put("/authorization", fields);
      Swal.fire("Good job!", "You succesfully set your password!", "success");
    } catch (error) {
      console.log(error.response.data);
    }
  }
}
