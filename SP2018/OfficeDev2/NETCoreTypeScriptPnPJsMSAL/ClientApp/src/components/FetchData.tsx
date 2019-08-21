import React, { Component } from 'react';

export interface IFetchDataState {
  forecasts: IForecast[];
  loading: boolean;
}
export interface IFetchDataProps {
}

interface IForecast {
  dateFormatted: string,
  temperatureC: number,
  temperatureF: number,
  summary: string
}

export class FetchData extends Component<IFetchDataProps, IFetchDataState> {
  static displayName = FetchData.name;

  constructor(props: IFetchDataProps) {
    super(props);
    this.state = { forecasts: [], loading: true };
  }

  public componentDidMount(): void {
    fetch('api/SampleData/WeatherForecasts')
      .then(response => response.json())
      .then(data => {
        this.setState({ forecasts: data, loading: false });
      });
  }

  render() {
    return (
      <div>
        <h1>Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {this.state.loading ? (
          <p><em>Loading...</em></p>
        ) : (
            <table className='table table-striped'>
              <thead>
                <tr>
                  <th>Date</th>
                  <th>Temp. (C)</th>
                  <th>Temp. (F)</th>
                  <th>Summary</th>
                </tr>
              </thead>
              <tbody>
                {this.state.forecasts.map(forecast =>
                  <tr key={forecast.dateFormatted}>
                    <td>{forecast.dateFormatted}</td>
                    <td>{forecast.temperatureC}</td>
                    <td>{forecast.temperatureF}</td>
                    <td>{forecast.summary}</td>
                  </tr>
                )}
              </tbody>
            </table>
          )}

      </div>
    );
  }
}
