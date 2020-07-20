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
import UserManager from './Components/UserManager/UserManager'
import FaultManager from './Components/FaultManager/FaultManager'
import CarManager from './Components/CarManager/CarManager'
import AddUser from './Components/AddUser/AddUser'
import AddCar from './Components/AddCar/AddCar'
import History from './Components/History/History'
import EditUser from './Components/EditUser/EditUser';
import EditCar from './Components/EditCar/EditCar';
import GenericNotFound from './Components/GenericNotFound/GenericNotFound'
import SetPassword from './Components/SetPassword/SetPassword';

function App() {
  return (
    <div>
    <Header />
    <Router>  
        <Switch>
          <Route exact path='/' component={Home} />  
          <Route path='/ReserveCar' component={ReserveCar} />  
          <Route path='/Signin' component={Login}/>
          <Route exact path='/UserManager' component={UserManager} />
          <Route path='/UserManager/Edit/:id' component={EditUser}/>
          <Route exact path='/FaultManager' component={FaultManager} />
          <Route exact path='/CarManager' component={CarManager} />
          <Route path='/CarManager/Edit/:id' component={EditCar} />
          <Route path='/AddUser' component={AddUser} />
          <Route path='/AddCar' component={AddCar} />
          <Route exact path='/History' component={History} />
          <Route path='/History/Edit/:id' component={UserReportFault} />
          <Route path='/SetPassword' component={SetPassword} />
          <Route component={GenericNotFound} />
        </Switch>    
    </Router>
    <Footer />
    </div>
  );
}

export default App;
