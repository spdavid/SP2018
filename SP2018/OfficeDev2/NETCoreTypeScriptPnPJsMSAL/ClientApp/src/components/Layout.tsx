import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import {MsalAuth} from '../auth/msalAuth';

export class Layout extends Component {
  static displayName = Layout.name;

constructor(props: any) {
  super(props);
}

  render () {
    MsalAuth.LogInUser();
    return (
      <div>
        <NavMenu />
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
