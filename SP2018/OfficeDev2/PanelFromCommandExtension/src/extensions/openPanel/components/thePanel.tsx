import * as React from 'react';
import { Panel, PanelType } from 'office-ui-fabric-react/lib/Panel';
import { ListViewCommandSetContext, IListViewCommandSetExecuteEventParameters } from '@microsoft/sp-listview-extensibility';

export interface IthePanelProps {
ctx : ListViewCommandSetContext;
event : IListViewCommandSetExecuteEventParameters;
close : () => void;

}

export interface IthePanelState {}

export default class thePanel extends React.Component<IthePanelProps, IthePanelState> {
  constructor(props: IthePanelProps) {
    super(props);

    this.state = {

    };
  }

  public render(): React.ReactElement<IthePanelProps> {
    return (
      <div>
<Panel
          isOpen={true}
          type={PanelType.smallFixedFar}
          onDismiss={this._hidePanel}
          headerText="Panel - Small, right-aligned, fixed, with footer"
          closeButtonAriaLabel="Close"
          //onRenderFooterContent={this._onRenderFooterContent}
        >
<div>
  {this.props.event.selectedRows[0].getValueByName("Title")}
</div>

        </Panel>
      </div>
    );
  }

  private _hidePanel = () => {
    this.props.close();
  };
}
