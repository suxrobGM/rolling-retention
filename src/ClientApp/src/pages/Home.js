import React, { Component } from 'react';
import { Grid } from '../components/Grid'
import { BarChart } from '../components/BarChart'

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <Grid></Grid>
        <div className="mt-4">
          <BarChart></BarChart>
        </div>
      </div>
    );
  }
}