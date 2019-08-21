import * as React from 'react';
import {currentUser} from './components/currentUser';

export default class PersonLibrary {
  public getCurrentUserComponent(): React.ReactNode {
    // let user = new currentUser({});
    // return user;
    return currentUser;
  }


}

