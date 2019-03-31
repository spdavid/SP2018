import { override } from '@microsoft/decorators';
import { Log } from '@microsoft/sp-core-library';
import {
  BaseListViewCommandSet,
  Command,
  IListViewCommandSetListViewUpdatedParameters,
  IListViewCommandSetExecuteEventParameters
} from '@microsoft/sp-listview-extensibility';
import { Dialog } from '@microsoft/sp-dialog';

import * as strings from 'HelloWorld2CommandSetStrings';

/**
 * If your command set uses the ClientSideComponentProperties JSON input,
 * it will be deserialized into the BaseExtension.properties object.
 * You can define an interface to describe it.
 */
export interface IHelloWorld2CommandSetProperties {
  // This is an example; replace with your own properties
  sampleTextOne: string;
  sampleTextTwo: string;
}

const LOG_SOURCE: string = 'HelloWorld2CommandSet';

export default class HelloWorld2CommandSet extends BaseListViewCommandSet<IHelloWorld2CommandSetProperties> {

  @override
  public onInit(): Promise<void> {
    Log.info(LOG_SOURCE, 'Initialized HelloWorld2CommandSet');
    return Promise.resolve();
  }

  @override
  public onListViewUpdated(event: IListViewCommandSetListViewUpdatedParameters): void {
console.log(event);

    const compareOneCommand: Command = this.tryGetCommand('COMMAND_1');

    const compareTwoCommand: Command = this.tryGetCommand('COMMAND_2');
    compareTwoCommand.visible = false;


    if (compareOneCommand) {
      // This command should be hidden unless exactly one row is selected.
      compareOneCommand.visible = event.selectedRows.length === 1;
    }
    if (this.context.pageContext.list.title != "Fun List")
    compareOneCommand.visible = false;
    {
  }
}

  @override
  public onExecute(event: IListViewCommandSetExecuteEventParameters): void {
    console.log(event);
    switch (event.itemId) {
      case 'COMMAND_1':

     let title = event.selectedRows[0].getValueByName("Title");

        Dialog.alert(`you selected item with title : ${title}`);
        break;
      case 'COMMAND_2':
        Dialog.alert(`${this.properties.sampleTextTwo}`);
        break;
      default:
        throw new Error('Unknown command');
    }
  }
}
