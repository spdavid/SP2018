import { Version } from '@microsoft/sp-core-library';
import {
  BaseClientSideWebPart,
  IPropertyPaneConfiguration,
  PropertyPaneTextField
} from '@microsoft/sp-webpart-base';
import { escape } from '@microsoft/sp-lodash-subset';

import styles from './HelloWorldWebPart.module.scss';
import * as strings from 'HelloWorldWebPartStrings';
import { SPHttpClient } from '@microsoft/sp-http';

export interface IHelloWorldWebPartProps {
  description: string;
}

export default class HelloWorldWebPart extends BaseClientSideWebPart<IHelloWorldWebPartProps> {

  public render(): void {
      this.domElement.innerHTML += `

      <input type='text' id='text' />
      <input id='button' type='Button' value='search' />
      <div id="result"></div>`;

      let button = document.getElementById("button") as HTMLInputElement;
      button.onclick = this.RegisterResults;

  }




private RegisterResults = () =>
{
  let divelement = document.getElementById("result");
  divelement.innerHTML= "";

  let year = (document.getElementById("text") as HTMLInputElement).value;
console.log(year);
 this.context.spHttpClient.get(
      this.context.pageContext.web.absoluteUrl + "/_api/Web/lists/getbytitle('Cars')/items?$filter=F_CarYear eq " + year,
      SPHttpClient.configurations.v1).then(response => {
        response.json().then(data => {
          let items = data.value as Array<any>;
          items.forEach(val => {
            divelement.innerHTML += `<div>${val.Title} : ${val.F_CarYear}</div>`;
          });
          console.log(data);

        });
      });

}


  protected get dataVersion(): Version {
    return Version.parse('1.0');
  }

  protected getPropertyPaneConfiguration(): IPropertyPaneConfiguration {
    return {
      pages: [
        {
          header: {
            description: strings.PropertyPaneDescription
          },
          groups: [
            {
              groupName: strings.BasicGroupName,
              groupFields: [
                PropertyPaneTextField('description', {
                  label: strings.DescriptionFieldLabel
                })
              ]
            }
          ]
        }
      ]
    };
  }
}
