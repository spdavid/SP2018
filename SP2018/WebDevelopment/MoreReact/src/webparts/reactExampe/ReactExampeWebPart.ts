import * as React from 'react';
import * as ReactDom from 'react-dom';
import { Version } from '@microsoft/sp-core-library';
import { BaseClientSideWebPart } from '@microsoft/sp-webpart-base';
import {
  IPropertyPaneConfiguration,
  PropertyPaneTextField,
  PropertyPaneDropdown,
  IPropertyPaneDropdownOption,
  PropertyPaneDropdownOptionType
} from '@microsoft/sp-property-pane';

import * as strings from 'ReactExampeWebPartStrings';
import ReactExampe,{ IReactExampeProps } from './components/ReactExampe';
import { sp } from "@pnp/sp";

export interface IReactExampeWebPartProps {
  description: string;
  color : string;
}

export default class ReactExampeWebPart extends BaseClientSideWebPart<IReactExampeWebPartProps> {

  public onInit(): Promise<void> {

    return super.onInit().then(_ => {
      // other init code may be present
      sp.setup({
        spfxContext: this.context
      });
    });
  }



  public render(): void {
    const element: React.ReactElement<IReactExampeProps > = React.createElement(
      ReactExampe,
      {
        description: this.properties.description,
        color : this.properties.color
      }
    );

    ReactDom.render(element, this.domElement);
  }

  protected onDispose(): void {
    ReactDom.unmountComponentAtNode(this.domElement);
  }

  protected get dataVersion(): Version {
    return Version.parse('1.0');
  }

  protected getPropertyPaneConfiguration(): IPropertyPaneConfiguration {
    return {
      pages: [
        {
          header: {
            description: "description prop pane"
          },
          groups: [
            {
              groupName: strings.BasicGroupName,
              groupFields: [
                PropertyPaneTextField('description', {
                  label: "my description"
                }),
                PropertyPaneDropdown('color', {
                  label : "Color",
                  options : [
                    {
                      index : 0,
                      key : "Red",
                      text : "Red"

                    },
                    {
                      index : 1,
                      key : "Blue",
                      text : "Blue"
                    },
                    {
                      index : 2,
                      key : "Purple",
                      text : "Purple"
                    }
                  ]
                })

              ]
            }
          ]
        }
      ]
    };
  }
}
