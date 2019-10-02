

export interface IToDo
{
  Title : string;
  DueDate : string;
  Category : string;
}

export class ToDoService
{

  private ToDoList : IToDo[] =
  [
      {
        Title : "Feed The Dog",
        DueDate : "2019-09-18",
        Category : "Home"
      },
      {
        Title : "Sleep Good",
        DueDate : "2019-09-18",
        Category : "Home"
      },
      {
        Title : "Prepare Speach",
        DueDate : "2019-09-18",
        Category : "Community"
      },
      {
        Title : "Finish Project",
        DueDate : "2019-09-31",
        Category : "Work"
      }
  ];


public static doSomething()
{

}

  public async GetToDos() : Promise<IToDo[]>
  {
    return this.ToDoList;
  }


  public AddToDo(todo : IToDo) : Promise<void>
  {
      this.ToDoList.push(todo);
      return;
  }






}
