import * as React from 'react';
import { sp } from '@pnp/sp';


export interface IgetSiteListsProps {}

export interface IgetSiteListsState {
    lists : any[]
}

export default class getSiteLists extends React.Component<IgetSiteListsProps, IgetSiteListsState> {
  constructor(props: IgetSiteListsProps) {
    super(props);

    this.state = {
      lists : []
    };
  }

  public async componentDidMount() {
    let lists = await sp.web.lists.get();
    console.log(lists);
    this.setState({lists : lists});
  }

  public render(): React.ReactElement<IgetSiteListsProps> {
    return (
      <div>
        
      </div>
    );
  }
}
