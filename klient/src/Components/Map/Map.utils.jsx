import { useState, useCallback, useRef } from "react";
import { useLoadScript } from "@react-google-maps/api";
import LocationApi from "../API/LocationApi";

export default function useMap() {
  const libraries = ["places"];
  const mapContainerStyle = {
    height: "90vh",
    width: "90vw",
  };

  const options = {
    disableDefaultUI: true,
    zoomControl: true,
  };

  const [center,setCenter]=useState({
    lat: 50.290971,
    lng: 18.704721,
  })

  const [marker, setMarker] = useState();

  const { isLoaded, loadError } = useLoadScript({
    googleMapsApiKey: process.env.REACT_APP_GOOGLE_MAPS_API_KEY,
    libraries,
  });

  const mapRef = useRef();
  const onMapLoad = useCallback((map) => {
    mapRef.current = map;
  }, []);

  async function fetchMarker(reservationid) {
    let api = new LocationApi();
    const response=await api.fetchLocalization(reservationid);
    setCenter({
      lat: response.latitude,
      lng: response.longitude,
    })
    setMarker(response)
  }

  return {
    isLoaded,
    loadError,
    mapContainerStyle,
    center,
    options,
    onMapLoad,
    marker,
    fetchMarker,
    setMarker
  };
}
