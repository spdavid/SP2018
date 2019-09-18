import * as React from 'react';
import { IToDo } from '../../../Services/ToDoService';
import { TextField } from 'office-ui-fabric-react/lib/TextField';
import { DetailsList, DetailsListLayoutMode, Selection, IColumn } from 'office-ui-fabric-react/lib/DetailsList';
import { MarqueeSelection } from 'office-ui-fabric-react/lib/MarqueeSelection';
import { Fabric } from 'office-ui-fabric-react/lib/Fabric';
import { mergeStyles } from 'office-ui-fabric-react/lib/Styling';

export interface IListAllToDosProps {
  allItems : IToDo[];
}

export interface IListAllToDosState {
    items : IToDo[];
}

export default class ListAllToDos extends React.Component<IListAllToDosProps, IListAllToDosState> {
  private _selection: Selection;
  private _columns: IColumn[];

  constructor(props: IListAllToDosProps) {
    super(props);

    this.state = {
        items : this.props.allItems
    };

    this._columns = [
      { key: 'column1', name: 'Title', fieldName: 'Title', minWidth: 100, maxWidth: 200, isResizable: true },
      { key: 'column2', name: 'Due Date', fieldName: 'DueDate', minWidth: 100, maxWidth: 200, isResizable: true },
      { key: 'column2', name: 'Category', fieldName: 'Category', minWidth: 100, maxWidth: 200, isResizable: true }
    ];

  }


  public render(): React.ReactElement<IListAllToDosProps> {
    return (
      <Fabric>

        <TextField
          label="Filter by name:"
          onChange={this._onFilter}
          styles={{ root: { maxWidth: '300px' } }}
        />
        <MarqueeSelection selection={this._selection}>
          <DetailsList
            compact={true}
            items={this.state.items}
            columns={this._columns}
            setKey="set"
            layoutMode={DetailsListLayoutMode.justified}
            selection={this._selection}
            selectionPreservedOnEmptyClick={true}
            ariaLabelForSelectionColumn="Toggle selection"
            ariaLabelForSelectAllCheckbox="Toggle selection for all items"
            checkButtonAriaLabel="Row checkbox"
          />
        </MarqueeSelection>
      </Fabric>
    );
  }

  private _onFilter = (ev: React.FormEvent<HTMLInputElement | HTMLTextAreaElement>, text: string): void => {
    this.setState({
      items: text ? this.props.allItems.filter(i => i.Title.toLowerCase().indexOf(text) > -1) : this.props.allItems
    });
  };
}
