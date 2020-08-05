import axios from "axios";
import Swal from "sweetalert2";

export default class ReservationsApi {
  constructor() {
    this.reservationAxios = axios.create({
      baseURL: "https://localhost:44390/api",
    });
  }

  async fetchUsersHistory() {
    try {
      const { data } = this.reservationAxios.get('/reservations');
      return data;
    } catch (error) {
      Swal.fire("Oops...", "Something went wrong!", "error");
    }
  }
  
  async fetchUserHistory(id) {
    try {
      //FIXME:
      //rightuserID
      const res = this.reservationAxios.get("/reservations/users/" + 1);
      return res.data;
    } catch (error) {
      Swal.fire("Oops...", "Something went wrong!", "error");
    }
  }

  async cancelReservation(id) {
    try {
      await this.reservationAxios.delete("/reservations/" + id);
      Swal.fire(
        "Deleted!",
        "Your reservation has been deleted.",
        "success"
      ).then(() => window.location.reload(false));
    } catch (error) {
      console.log(error);
    }
  }

  async checkAvilable(carId, rentalDate, returnDate) {
    try {
      const response = await this.reservationAxios.get(
        "/terms/" + carId + "/" + rentalDate + "/" + returnDate
      );
      return response.data;
    } catch (error) {
      console.log(error);
    }
  }

  async addReservation(fields) {
    try {
      await this.reservationAxios.post('/reservations', fields);
      Swal.fire("Success", "You successfully reserved term", "success");
    } catch (error) {
      Swal.fire("Oops...", "Something went wrong...", "error");
      console.log(error);
    }
  }
}
