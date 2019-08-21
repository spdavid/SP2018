declare interface IPersonLibraryStrings {
  PropertyPaneDescription: string;
  BasicGroupName: string;
  DescriptionFieldLabel: string;
}

declare module 'PersonLibraryStrings' {
  const strings: IPersonLibraryStrings;
  export = strings;
}
