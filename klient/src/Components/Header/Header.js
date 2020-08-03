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
              <NavLink href="/reserve-car">Reserve car</NavLink>
            </NavItem>
            <NavItem>
              <NavLink href="/sign-in">Sign in</NavLink>
            </NavItem>
            <NavItem>
              <NavLink href="/user-manager">User Manager</NavLink>
            </NavItem>
            <NavItem>
              <NavLink href="/fault-manager">Fault Manager</NavLink>
            </NavItem>
            <NavItem>
              <NavLink href="/car-manager">Car Manager</NavLink>
            </NavItem>
            <NavItem>
              <NavLink href="/add-user">Add User</NavLink>
            </NavItem>
            <NavItem>
              <NavLink href="/add-car">Add Car</NavLink>
            </NavItem>
            <NavItem>
              <NavLink href="/user-history">User history</NavLink>
            </NavItem>
            <NavItem>
              <NavLink href="/admin-history">Admin history</NavLink>
            </NavItem>
            <NavItem>
              <NavLink href="/map">Map</NavLink>
            </NavItem>
          </Nav>
        </Collapse>
      </Navbar>
    </div>
  );
}
export default Header