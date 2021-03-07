import React, { Component } from 'react';
import { Container } from 'react-bootstrap'
import { NavMenu } from './NavMenu'

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <NavMenu></NavMenu>
        <main className="content">
          <Container>
            {this.props.children}
          </Container>
        </main>
      </div>
    );
  }
}