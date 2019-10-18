import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { BaseDialog, IDialogConfiguration } from '@microsoft/sp-dialog';
import thePanel from './components/thePanel';
import { IListViewCommandSetExecuteEventParameters, ListViewCommandSetContext } from '@microsoft/sp-listview-extensibility';
import { WebPartContext } from '@microsoft/sp-webpart-base';



export default class PanelDialog extends BaseDialog {
    public event :IListViewCommandSetExecuteEventParameters;
    public ctx : ListViewCommandSetContext;

  public render(): void {

    var element = React.createElement(
      thePanel,
      {
            ctx : this.ctx,
            event : this.event,
            close : this.closeDialog,
            key : Math.random()
      }
    );

    ReactDOM.render(element, this.domElement);


  }

  private closeDialog = () => {
    ReactDOM.unmountComponentAtNode(this.domElement);
    this.close();
  }

}
