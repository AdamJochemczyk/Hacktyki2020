import React from 'react';
import './App.css';
import { BrowserRouter as Router, Switch, Route} from 'react-router-dom'; 
import Header from './Components/Header/Header'
import Footer from './Components/Footer/Footer'
import 'bootstrap/dist/css/bootstrap.css';
import UserReportFault from './Components/UserReportFault/UserReportFault'
import ReserveCar from './Components/ReserveCar/ReserveCar'
import Home from './Components/Home/Home'
import Login from './Components/Login/Login'
import FaultManager from './Components/FaultManager/FaultManager'
import CarManager from './Components/CarManager/CarManager'
import EditUser from './Components/EditUser/EditUser';
import EditCar from './Components/EditCar/EditCar';
import GenericNotFound from './Components/GenericNotFound/GenericNotFound'
import SetPassword from './Components/SetPassword/SetPassword';
import UserManager from './Components/UserManager/UserManager';
import Booking from './Components/Booking/Booking'
import UserHistory from './Components/UserHistory/UserHistory'
import AdminHistory from './Components/AdminHistory/AdminHistory'
import Map from "./Components/Map/Map"

function App() {
  return (
    <div>
    <Header />
    <Router>  
        <Switch>
          <Route exact path='/' component={Home} />  
          <Route exact path='/reserve-car' component={ReserveCar} />
          <Route path='/reserve-car/booking' component={Booking} />  
          <Route path='/sign-in' component={Login}/>
          <Route exact path='/user-manager' component={UserManager} />
          <Route path='/user-manager/edit' component={EditUser}/>
          <Route exact path='/fault-manager' component={FaultManager} />
          <Route exact path='/car-manager' component={CarManager} />
          <Route path='/car-manager/edit' component={EditCar} />
          <Route path='/add-user' component={EditUser} />
          <Route path='/add-car' component={EditCar} />
          <Route path='/user-history' component={UserHistory} />
          <Route path='/admin-history' component={AdminHistory} />
          <Route path='/history/edit' component={UserReportFault} />
          <Route path='/set-password/:code' component={SetPassword} />
          <Route path='/map' component={Map} />
          <Route component={GenericNotFound} />
        </Switch>    
    </Router>
    <Footer />
    </div>
  );
}

export default App;
