import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import  getMail  from './components/getMail';
import  getSiteLists  from './components/getSiteLists';



export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Switch>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data' component={FetchData} />
        <Route path='/messages' component={getMail} />
        <Route path='/lists' component={getSiteLists} />
        <Route path="*" component={Home} />
        </Switch>
      </Layout>
    );
  }
}
