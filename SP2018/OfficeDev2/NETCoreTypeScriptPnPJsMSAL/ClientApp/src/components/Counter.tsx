import React, { Component } from 'react';

export interface ICounterState
{
  currentCount : number;
}

export interface ICounterProps
{
}

export class Counter extends Component<ICounterProps, ICounterState> {
  static displayName = Counter.name;

  constructor (props : ICounterProps) {
    super(props);
    this.state = { currentCount: 0 };
    this.incrementCounter = this.incrementCounter.bind(this);
  }

  incrementCounter () {
    this.setState({
      currentCount: this.state.currentCount + 1
    });
  }

  render () {
    return (
      <div>
        <h1>Counter</h1>

        <p>This is a simple example of a React component.</p>

        <p>Current count: <strong>{this.state.currentCount}</strong></p>

        <button className="btn btn-primary" onClick={this.incrementCounter}>Increment</button>
      </div>
    );
  }
}
