import * as React from 'react';
import { ActionButton, IIconProps } from 'office-ui-fabric-react';
import { Panel, PanelType } from 'office-ui-fabric-react/lib/Panel';
import { DefaultButton, PrimaryButton } from 'office-ui-fabric-react/lib/Button';
import { TextField } from 'office-ui-fabric-react/lib/TextField';
import { DatePicker, DayOfWeek, IDatePickerStrings } from 'office-ui-fabric-react';
import { Dropdown, DropdownMenuItemType, IDropdownStyles, IDropdownOption } from 'office-ui-fabric-react/lib/Dropdown';
import { IToDo } from '../../../Services/ToDoService';

const addIcon: IIconProps = { iconName: 'Add' };

const options: IDropdownOption[] = [
  { key: 'Work', text: 'Work' },
  { key: 'Home', text: 'Home' },
  { key: 'Community', text: 'Community' }
];

export interface IAddToDoProps {
      onToDoAdded : (newToDo : IToDo) => void;
 }

export interface IAddToDoState {
  showPanel: boolean;
}

export default class AddToDo extends React.Component<IAddToDoProps, IAddToDoState> {
  private CurrentTitle : string = "";
  private CurrentDueDate : string = "";
  private CurrentCategory : string = "";


  constructor(props: IAddToDoProps) {
    super(props);

    this.state = {
      showPanel: false
    };
  }

  public render(): React.ReactElement<IAddToDoProps> {
    return (
      <div>
        <ActionButton onClick={this._showPanel} iconProps={addIcon} allowDisabledFocus>
          AddToDo
        </ActionButton>
        <Panel
          isOpen={this.state.showPanel}
          type={PanelType.smallFixedFar}
          onDismiss={this._hidePanel}
          headerText="Add ToDo"
          closeButtonAriaLabel="Close"
          onRenderFooterContent={this._onRenderFooterContent}
        >
          <TextField onChange={this.TitleChanged} placeholder="Enter Title" label="Title"></TextField>
          <DatePicker onSelectDate={this.DateChanged} label="DueDate" placeholder="Select a date..." ariaLabel="Select a date" />
          <Dropdown onChange={this.CategoryChanged}
            label="Category"
            options={options}
          />
        </Panel>
      </div>
    );
  }

  private _onRenderFooterContent = () => {
    return (
      <div>
        <PrimaryButton onClick={this._save} style={{ marginRight: '8px' }}>
          Save
        </PrimaryButton>
        <DefaultButton onClick={this._hidePanel}>Cancel</DefaultButton>
      </div>
    );
  };

  private TitleChanged = (event: React.FormEvent<HTMLInputElement | HTMLTextAreaElement>, newValue?: string) => {
    this.CurrentTitle = newValue;
    console.log(newValue);
  }

  private DateChanged = (date : Date) =>
  {
    this.CurrentDueDate = date.toLocaleDateString();
  }

  private CategoryChanged = (event: React.FormEvent<HTMLDivElement>, option?: IDropdownOption, index?: number) => {
    this.CurrentCategory = option.text;
  }

  private _save = () =>
  {
      let newToDo : IToDo = {
        Title : this.CurrentTitle,
        Category : this.CurrentCategory,
        DueDate : this.CurrentDueDate
      }

      this.props.onToDoAdded(newToDo);
      this._hidePanel();

  }


  private _showPanel = () => {
    this.setState({ showPanel: true });
  };

  private _hidePanel = () => {
    this.setState({ showPanel: false });
  };
}
