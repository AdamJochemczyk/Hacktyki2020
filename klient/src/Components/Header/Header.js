import React, { useState } from 'react';
import { Collapse, Navbar, NavbarToggler, NavbarBrand, Nav, NavItem, NavLink } from 'reactstrap';
import logo from '../img/euvic-logo.PNG';
import styles from '../Header/Headerstyle.module.css'

const Header = (props) => {
  const [collapsed, setCollapsed] = useState(true);

  const toggleNavbar = () => setCollapsed(!collapsed);

  return (
    <div className={styles.fittotop}>
      <Navbar color="faded" light>
        <NavbarBrand href="/" className="mr-auto"><img src={logo} alt='euvic-logo'></img></NavbarBrand>
        <NavbarToggler onClick={toggleNavbar} className="mr-2" />
        <Collapse isOpen={!collapsed} navbar>
          <Nav navbar>
            <NavItem>
              <NavLink href="/ReserveCar">Reserve car</NavLink>
            </NavItem>
            <NavItem>
              <NavLink href="/Signin">Sign in</NavLink>
            </NavItem>
            <NavItem>
              <NavLink href="/UserManager">User Manager</NavLink>
            </NavItem>
            <NavItem>
              <NavLink href="/FaultManager">Fault Manager</NavLink>
            </NavItem>
            <NavItem>
              <NavLink href="/CarManager">Car Manager</NavLink>
            </NavItem>
            <NavItem>
              <NavLink href="/AddUser">Add User</NavLink>
            </NavItem>
            <NavItem>
              <NavLink href="/AddCar">Add Car</NavLink>
            </NavItem>
            <NavItem>
              <NavLink href="/History">History</NavLink>
            </NavItem>
          </Nav>
        </Collapse>
      </Navbar>
    </div>
  );
}
export default Header