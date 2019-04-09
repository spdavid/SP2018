import * as React from 'react';
import styles from './DisplayLists.module.scss';
import { escape } from '@microsoft/sp-lodash-subset';
import List from './List';
import { sp } from "@pnp/sp";


export interface IList
{
  Id: string;
  Title : string;
}

export interface IDisplayListsProps {
  description: string;
}

export interface IDisplayListState {
  spLists: Array<IList>;
}

export default class DisplayLists extends React.Component<IDisplayListsProps, IDisplayListState> {

  constructor(props: IDisplayListsProps) {
    super(props);
    this.state = {
      spLists: []
    };
  }

  public async componentDidMount() {
    let lists = await sp.web.lists.filter("Hidden eq false").select("Title,Id").get();
    console.log(lists);

    this.setState({spLists : lists});
  }

  public render(): React.ReactElement<IDisplayListsProps> {
    return (
      <div>
        {this.state.spLists.map(l => {
          return <List Title={l.Title} Id={l.Id}></List>;
        })}
      </div>
    );
  }
}
