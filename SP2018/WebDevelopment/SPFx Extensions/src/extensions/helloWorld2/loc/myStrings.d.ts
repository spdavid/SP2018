declare interface IHelloWorld2CommandSetStrings {
  Command1: string;
  Command2: string;
}

declare module 'HelloWorld2CommandSetStrings' {
  const strings: IHelloWorld2CommandSetStrings;
  export = strings;
}
