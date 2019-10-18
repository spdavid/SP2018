import { override } from '@microsoft/decorators';
import { Log } from '@microsoft/sp-core-library';
import {
  BaseListViewCommandSet,
  Command,
  IListViewCommandSetListViewUpdatedParameters,
  IListViewCommandSetExecuteEventParameters
} from '@microsoft/sp-listview-extensibility';
import { Dialog } from '@microsoft/sp-dialog';
import PanelDialog from './PanelDialog';

import * as strings from 'OpenPanelCommandSetStrings';

/**
 * If your command set uses the ClientSideComponentProperties JSON input,
 * it will be deserialized into the BaseExtension.properties object.
 * You can define an interface to describe it.
 */
export interface IOpenPanelCommandSetProperties {

}

const LOG_SOURCE: string = 'OpenPanelCommandSet';

export default class OpenPanelCommandSet extends BaseListViewCommandSet<IOpenPanelCommandSetProperties> {

  @override
  public onInit(): Promise<void> {
    console.log("my app");
    Log.info(LOG_SOURCE, 'Initialized OpenPanelCommandSet');
    return Promise.resolve();
  }

  @override
  public onListViewUpdated(event: IListViewCommandSetListViewUpdatedParameters): void {
    const compareOneCommand: Command = this.tryGetCommand('COMMAND_1');
    if (compareOneCommand) {
      // This command should be hidden unless exactly one row is selected.
      compareOneCommand.visible = event.selectedRows.length == 1;
      compareOneCommand.title = "Open Panel";

    }
  }

  @override
  public onExecute(event: IListViewCommandSetExecuteEventParameters): void {
    switch (event.itemId) {
      case 'COMMAND_1':
        const dialog: PanelDialog = new PanelDialog();

        dialog.event = event;
        dialog.ctx = this.context;
        dialog.show().then(() => { console.log("pressed")});

        break;
      default:
        throw new Error('Unknown command');
    }
  }
}
