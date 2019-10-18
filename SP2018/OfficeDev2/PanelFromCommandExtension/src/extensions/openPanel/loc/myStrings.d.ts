declare interface IOpenPanelCommandSetStrings {
  Command1: string;
  Command2: string;
}

declare module 'OpenPanelCommandSetStrings' {
  const strings: IOpenPanelCommandSetStrings;
  export = strings;
}
