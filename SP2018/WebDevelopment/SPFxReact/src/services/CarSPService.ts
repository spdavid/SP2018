import { ICar } from '../models/ICar';
import { WebPartContext } from '@microsoft/sp-webpart-base';
import { SPHttpClient } from '@microsoft/sp-http';
import { sp } from "@pnp/sp";

export default class CarSPService {


  public async SearchCarsWithPnP(searchText: string)
  {
    let items = await sp.web.lists.getByTitle('Cars').items.filter(`startswith(Title, '${searchText}')`).get();

    let results: Array<ICar> = items.map(car => {
      return { Id: car.Id, Color: car.F_CarColor, Model: car.F_CarModel, Title: car.Title } as ICar;
    });
    return results;
  }


  public SearchCarsWithPromise(searchText: string, ctx: WebPartContext): Promise<Array<ICar>> {
    return new Promise((resolve, reject) => {
      ctx.spHttpClient.get(
        ctx.pageContext.web.absoluteUrl + `/_api/Web/lists/getbytitle('Cars')/items?$filter=startswith(Title, '${searchText}')')`,
        SPHttpClient.configurations.v1).then(response => {
          if (response.ok) {
            response.json().then(data => {
              let items = data.value as Array<any>;

              let results: Array<ICar> = items.map(car => {
                return { Id: car.Id, Color: car.F_CarColor, Model: car.F_CarModel, Title: car.Title } as ICar;
              });

              resolve(results);

            });
          }
          else {
            reject(response.status);
          }
        });
    });
  }

  public async SearchCarsWithAwait(searchText: string, ctx: WebPartContext): Promise<Array<ICar>> {

    console.log("sdfs");
    let response = await ctx.spHttpClient.get(ctx.pageContext.web.absoluteUrl + `/_api/Web/lists/getbytitle('Cars')/items?$filter=startswith(Title, '${searchText}')')`, SPHttpClient.configurations.v1);
    if (response.ok) {
      let data = await response.json();
      let items = data.value as Array<any>;
      let results: Array<ICar> = items.map(car => {
        return { Id: car.Id, Color: car.F_CarColor, Model: car.F_CarModel, Title: car.Title } as ICar;
      });
      return results;
    }
    else {
      throw response.status;
    }


  }

}
