import * as React from 'react';
import styles from './Todo.module.scss';
import { ITodoProps } from './ITodoProps';
import { escape } from '@microsoft/sp-lodash-subset';
import AddToDo from './AddToDo';
import ListAllToDos from './ListAllToDos';
import { ToDoService, IToDo } from '../../../Services/ToDoService';


export interface ITodDoState {
  items: IToDo[];
  loading: boolean;
}


export default class Todo extends React.Component<ITodoProps, ITodDoState> {

  private service: ToDoService;


  constructor(props: ITodoProps) {
    super(props);

    this.state = {
      items: [],
      loading: true
    };

    this.service = new ToDoService();
  }

  public async componentDidMount() {
    let todoItems = await this.service.GetToDos();
    this.setState({ items: todoItems, loading: false });
  }



  public render(): React.ReactElement<ITodoProps> {
    return (
      <div>
        {this.state.loading ? (
          <div>
            loading ...
          </div>
        ) : (
            <div>
              <AddToDo onToDoAdded={this.onAdded}></AddToDo>
              <ListAllToDos key={this.state.items.length} allItems={this.state.items}></ListAllToDos>
            </div>
          )}
      </div>
    );
  }

private onAdded = async (newToDo : IToDo) => {
  await this.service.AddToDo(newToDo);
  let refreshedItems = await this.service.GetToDos();
  this.setState({items : refreshedItems});
}

}
