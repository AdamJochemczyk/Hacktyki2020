import React, { useEffect, useCallback } from "react";
import { GoogleMap, Marker } from "@react-google-maps/api";
import useMap from "./Map.utils";
import LocationApi from "../API/LocationApi";

export default function App({history}) {
  
  const {
    isLoaded,
    loadError,
    mapContainerStyle,
    center,
    options,
    onMapLoad,
    marker,
    fetchMarker,
    setMarker
  } = useMap();

  const onMapClick = useCallback((e) => {

    let props={
      latitude: e.latLng.lat(),
      longitude: e.latLng.lng(),
      reservationid: history.location.state
    }

    let api=new LocationApi()
    api.setLocalization(props)

    setMarker({
      latitude: e.latLng.lat(),
      longitude: e.latLng.lng(),
    });
  }, []);

  useEffect(()=>{
    fetchMarker(history.location.state)
  },[])

  if (loadError) return "Error";
  if (!isLoaded) return "Loading...";

  return (
    <div className="d-flex justify-content-center">
      <GoogleMap
        id="map"
        mapContainerStyle={mapContainerStyle}
        zoom={15}
        center={center}
        options={options}
        onClick={onMapClick}
        onLoad={onMapLoad}
      >
        {marker && (
          <Marker
            key={`${marker.latitude}-${marker.longitude}`}
            position={{ lat: marker.latitude, lng: marker.longitude }}
            icon={{
              url: `https://upload.wikimedia.org/wikipedia/commons/6/65/Circle-icons-car.svg`,
              origin: new window.google.maps.Point(0, 0),
              anchor: new window.google.maps.Point(15, 15),
              scaledSize: new window.google.maps.Size(30, 30),
            }}
          />
        )}
      </GoogleMap>
    </div>
  );
}
