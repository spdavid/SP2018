import * as React from 'react';
import { graph } from '@pnp/graph';
import { Message } from '@microsoft/microsoft-graph-types';

export interface IgetMailProps {}

export interface IgetMailState {
    messages : Message[];
}

export default class getMail extends React.Component<IgetMailProps, IgetMailState> {
  constructor(props: IgetMailProps) {
    super(props);

    this.state = {
      messages : []
    };
  }

  public async componentDidMount() {
    let messages  = await graph.me.messages.top(50).get();
    let foo = await graph.me.drive.get();
    console.log("getting foo");
    console.log(foo);
    this.setState({messages : messages});
    console.log(messages);
  }

  public render(): React.ReactElement<IgetMailProps> {
    return (
      <div>
        {this.state.messages.map((message) =>{
            return <div>{message.subject}</div>
        })}
      </div>
    );
  }
}
