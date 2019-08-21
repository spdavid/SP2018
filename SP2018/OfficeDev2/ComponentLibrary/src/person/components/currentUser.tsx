import * as React from 'react';
export interface IcurrentUserProps {}
export interface IcurrentUserState {}
export  class currentUser extends React.Component<IcurrentUserProps, IcurrentUserState> {
  constructor(props: IcurrentUserProps) {
    super(props);
    this.state = {
    };
  }
  public render(): React.ReactElement<IcurrentUserProps> {
    return (
      <div>
        this is where the current person will be.
      </div>
    );
  }
}
