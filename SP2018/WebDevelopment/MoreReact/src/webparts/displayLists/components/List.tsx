import * as React from 'react';
import styles from './DisplayLists.module.scss';
import { sp } from "@pnp/sp";

export interface IListProps {
  Title : string;
  Id : string;
}

export interface IListState {
    view : {
      ServerRelativeUrl : string
    };
}

export default class List extends React.Component<IListProps, IListState> {
  constructor(props: IListProps) {
    super(props);

    this.state = {
     view : {ServerRelativeUrl : ""}
    };
  }

  public async componentDidMount()
  {
    let spList = await sp.web.lists.getById(this.props.Id).defaultView.select("ServerRelativeUrl").get();
    console.log(spList);
    this.setState({view : spList});
  }

  public render(): React.ReactElement<IListProps> {
    return (
      <div>
         <a href={this.state.view.ServerRelativeUrl}> {this.props.Title}</a>
      </div>
    );
  }
}
