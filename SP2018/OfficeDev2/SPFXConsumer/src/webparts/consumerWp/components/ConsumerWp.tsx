import * as React from 'react';
import styles from './ConsumerWp.module.scss';
import { IConsumerWpProps } from './IConsumerWpProps';
import { escape } from '@microsoft/sp-lodash-subset';
import {currentUser} from 'component-library';


export default class ConsumerWp extends React.Component<IConsumerWpProps, {}> {
  public render(): React.ReactElement<IConsumerWpProps> {


    // let instance = new myLibrary.PersonLibrary;
    // let currentUser = instance.getCurrentUserComponent();

    return (
      <div>
     <currentUser key="test"></currentUser>

      </div>
    );
  }
}
