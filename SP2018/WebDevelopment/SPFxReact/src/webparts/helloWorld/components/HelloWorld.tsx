import * as React from 'react';
import styles from './HelloWorld.module.scss';
import { escape } from '@microsoft/sp-lodash-subset';
import { WebPartContext } from '@microsoft/sp-webpart-base';
import {
  Environment,
  EnvironmentType
} from '@microsoft/sp-core-library';
import CarMockService from '../../../services/CarMockService';
import { ICar } from '../../../models/ICar';
import CarSPService from '../../../services/CarSPService';

export interface IHelloWorldProps {
  ctx: WebPartContext;
}

export interface IHelloWorldState {
  cars: Array<ICar>;
}


export default class HelloWorld extends React.Component<IHelloWorldProps, IHelloWorldState> {
  // private textInput : HTMLInputElement;
  private inputText: string;

  constructor(props: IHelloWorldProps) {
    super(props);
    this.state = {
      cars: []
    };
  }

  public render(): React.ReactElement<IHelloWorldProps> {
    return (
      <div className={styles.blue}>
        {/* <input ref={i => this.textInput = i} type='text'></input> */}
        <input onChange={this.textChanged} type='text'></input>
        <input type="button" value="search cars" onClick={this.getResults} ></input>
        {
          this.state.cars.map((car) => {
            return <div key={car.Id}>{car.Title}</div>;
          })
        }
      </div>
    );
  }

  private textChanged = (event) => {
    this.inputText = event.target.value;
    console.log(this.inputText);
    this.getResults();
  }

  private  getResults = async () => {
    console.log(this.inputText);

    if (Environment.type == EnvironmentType.Local) {
      let mockService = new CarMockService();
      let results = mockService.SearchCars(this.inputText);
      this.setState({ cars: results });
    }
    else {
      let spService = new CarSPService();
      // spService.SearchCarsWithPromise(this.inputText, this.props.ctx).then(results => {
      //   this.setState({ cars: results });
      // }).catch(err => {
      //   console.log(err);
      // });

      try {
        let results = await spService.SearchCarsWithAwait(this.inputText, this.props.ctx);
        this.setState({ cars: results });
      } catch (error) {
        console.log(error);
      }

    }

  }
}
