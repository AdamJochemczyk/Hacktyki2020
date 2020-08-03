import React, { useEffect } from "react";
import { GoogleMap, Marker } from "@react-google-maps/api";
import useMap from "./Map.utils";

export default function App() {
  const {
    isLoaded,
    loadError,
    mapContainerStyle,
    center,
    options,
    onMapClick,
    onMapLoad,
    marker,
    fetchMarker,
  } = useMap();

  useEffect(()=>{
    fetchMarker()
  },[])

  if (loadError) return "Error";
  if (!isLoaded) return "Loading...";

  return (
    <div>
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
