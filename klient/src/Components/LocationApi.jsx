import Api from "./API"
import axios from "axios"

const BASE_URL=process.env.REACT_APP_LOCATIONS_API

export default class LocationApi extends Api{

    async fetchLocalization(reservationid){
        try {
            const res = await axios({
                //FIXME:
                //right reservation ID
              url: BASE_URL + "/" + 2,
              method: "GET",
            }).catch((error) => {
              console.log(error);
            });
            return res.data;
          } catch (error) {
            console.log(error.message);
          }
    }

    async setLocalization(props){
        try {
            await axios({
              url: BASE_URL,
              method: "POST",
              data: props,
            })
          } catch (error) {
              console.log(error)
          }
    }
}