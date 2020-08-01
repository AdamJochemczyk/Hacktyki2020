import axios from "axios";
import Swal from "sweetalert2";

const CAR_API = process.env.REACT_APP_CAR_API;
const REGISTER_URL = process.env.REACT_APP_REGISTER_API;
const USER_URL = process.env.REACT_APP_USER_API;
const RESERVATION_URL=process.env.REACT_APP_RESERVATION_API;

export default class Api {
  //TODO:
  //get tokens from localstorage and check if token valid
  //class to user, car, extends api
  //class for errors
  constructor() {
    this.token = "aaa";
    this.refreshtoken = "bbb";
  }

  //CARS
  async createCar(params) {
    try {
      await axios({
        url: CAR_API,
        method: "POST",
        data: params,
      }).catch((error) => {
        if (error.response) {
          Swal.fire("Oops...", error.response.headers, "error");
        } else if (error.request) {
          Swal.fire(
            "Oops...",
            "Error request was made but no response was recived. Error request: " +
              error.request,
            "error"
          );
        } else {
          Swal.fire(
            "Oops...",
            "Something happened in setting up the request that triggered an Error. Error massage: " +
              error.message,
            "error"
          );
        }
      });
      Swal.fire("Good job!", "You succesfully added new car!", "success");
    } catch (error) {
      Swal.fire("Oops...", "Something went wrong...", "error");
    }
  }
  async updateCar(id, params) {
    try {
      await axios({
        url: CAR_API + "/" + id,
        method: "PUT",
        data: params,
      }).catch((error) => {
        if (error.response) {
          Swal.fire("Oops...", error.response.headers, "error");
        } else if (error.request) {
          Swal.fire(
            "Oops...",
            "Error request was made but no response was recived. Error request: " +
              error.request,
            "error"
          );
        } else {
          Swal.fire(
            "Oops...",
            "Something happened in setting up the request that triggered an Error. Error massage: " +
              error.message,
            "error"
          );
        }
      });
      Swal.fire("Good job!", "You succesfully edited a car!", "success");
    } catch (error) {
      Swal.fire("Oops...", "Something went wrong", "error");
    }
  }
  async fetchCar(id) {
    try {
      const res = await axios({
        url: CAR_API + "/" + id,
        method: "GET",
      }).catch((error) => {
        console.log(error);
      });
      return res.data;
    } catch (error) {
      alert(error.message);
    }
  }
  async fetchCars() {
    try {
      const res = await axios({
        url: CAR_API,
        method: "GET",
      });
      return res.data;
    } catch (error) {
      Swal.fire("Oops...", "Something went wrong!", "error");
    }
  }
  async deleteCar(id) {
    try {
      axios({
        url: CAR_API + "/" + id,
        method: "DELETE",
      }).catch((error) => {
        Swal.fire(
          "Oops",
          "Something went wrong when deleting. Error:" + error.response,
          "error"
        );
      });
    } catch (error) {
      console.log(error);
    }
  }

  //USER
  async createUser(params) {
    try {
      await axios({
        url: REGISTER_URL,
        method: "POST",
        data: params,
      }).catch((error) => {
        if (error.response) {
          Swal.fire("Oops...", error.response.headers, "error");
        } else if (error.request) {
          Swal.fire(
            "Oops...",
            "Error request was made but no response was recived. Error request: " +
              error.request,
            "error"
          );
        } else {
          Swal.fire(
            "Oops...",
            "Something happened in setting up the request that triggered an Error. Error massage: " +
              error.message,
            "error"
          );
        }
      });
      Swal.fire("Good job!", "You succesfully added new user!", "success");
    } catch (error) {
        console.log(error)
    }
  }
  async updateUser(id, fields) {
    try {
      await axios({
        url: USER_URL + "/" + id,
        method: "PUT",
        data: fields,
      }).catch((error) => {
        if (error.response) {
          Swal.fire("Oops...", error.response.headers, "error");
        } else if (error.request) {
          Swal.fire(
            "Oops...",
            "Error request was made but no response was recived. Error request: " +
              error.request,
            "error"
          );
        } else {
          Swal.fire(
            "Oops...",
            "Something happened in setting up the request that triggered an Error. Error massage: " +
              error.message,
            "error"
          );
        }
      });
      Swal.fire("Good job!", "You succesfully edited a user!", "success");
    } catch (error) {
      console.log(error);
    }
  }
  async fetchUser(id) {
    try {
      const res = await axios({
        url: USER_URL + "/" + id,
        method: "GET",
      }).catch((error) => {
        console.log(error);
      });
      return res.data;
    } catch (error) {
      alert(error.message);
    }
  }
  async fetchUsers() {
    try {
      const res = await axios({
        url: USER_URL,
        method: "GET",
      });
      return res.data;
    } catch (error) {
      Swal.fire("Oops...", "Something went wrong!", "error");
    }
  }
  async deleteUser(id) {
    try {
      axios({
        url: USER_URL + "/" + id,
        method: "DELETE",
      }).catch((error) => {
        Swal.fire(
          "Oops",
          "Something went wrong when deleting. Error:" + error.response,
          "error"
        );
      });
    } catch (error) {
      console.log(error);
    }
  }

  //RESERVATIONS
  async fetchUserHistory(id){
    try {
      const res = await axios({
        method: "GET",
        url: RESERVATION_URL + "/users/" + 28,
      });
      return res.data;
    } catch (error) {
      Swal.fire("Oops...", "Something went wrong!", "error");
    }
  }

  async fetchUsersHistory(){
    try{
    const {data}=await axios({
      method: "GET",
      url: RESERVATION_URL,
    });
    return data;
  }catch(error){
    Swal.fire("Oops...", "Something went wrong!", "error");
  }
  }

  async cancelReservation(id){
    await axios({
      url: RESERVATION_URL + "/" + id,
      method: "DELETE",
    }).catch((error) => {
      Swal.fire(
        "Oops",
        "Something went wrong when deleting. Error:" + error.response,
        "error"
      );
    });
    Swal.fire(
      "Deleted!",
      "Your reservation has been deleted.",
      "success"
    ).then(() => window.location.reload(false));
  }


}
