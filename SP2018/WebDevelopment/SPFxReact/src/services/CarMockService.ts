import {ICar} from '../models/ICar';


export default class CarMockService
{
    private cars : Array<ICar> = [
      { Id : 1, Color : "Green", Title :"Volvo V60", Model : "V60"},
      { Id : 2, Color : "Black", Title :"Volvo XC90", Model : "V60"},
      { Id : 3, Color : "Blue", Title :"Tesla S", Model : "S"},
      { Id : 4, Color : "Red", Title :"Tesla 3", Model : "3"},
      { Id : 5, Color : "Purple", Title :"Audi A1", Model : "A1"},
      { Id : 6, Color : "Green", Title :"Audi A5", Model : "A5"},
      { Id : 7, Color : "Blue", Title :"Volvo XC60", Model : "XC60"}
    ];

    public SearchCars(searchText : string) : Array<ICar>
    {
       let filteredCars = this.cars.filter(
         (car) => {
           return car.Title.toLowerCase().startsWith(searchText.toLowerCase());
          }
        );
       return filteredCars;
    }

}
