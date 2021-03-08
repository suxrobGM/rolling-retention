import React, { Component } from 'react';
import { Container, Navbar, NavbarBrand, } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { Login } from './Login'

//import './NavMenu.css';

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render () {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/">AB Test Real</NavbarBrand>
            <Login></Login>
          </Container>
        </Navbar>
      </header>
    );
  }
}
