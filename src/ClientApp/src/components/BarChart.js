import React, { Component } from 'react';
import { ChartComponent, SeriesCollectionDirective, SeriesDirective, Inject, Legend, 
  Category, Tooltip, DataLabel, BarSeries, DateTime } from '@syncfusion/ej2-react-charts';

export class BarChart extends Component {
  static displayName = BarChart.name;

  constructor(props) {
    super(props);
    let minDate = new Date();
    minDate.setDate(minDate.getDate() - 7);

    this.state = {
      chartData: [],

      yAxis: {
        minimum: 0,
        maximum: 25, 
        interval: 2, 
        title: "Live Users"
      },

      xAxis: {
        valueType: "DateTime",
        intervalType: "Days",
        interval: 1,
        minimum: minDate,
        maximum: new Date(),
        labelFormat: "dd/MM/yyyy",
        title: "Days",
      },
    }
  }

  getUserRetentions = () => {
    let authUser = JSON.parse(localStorage.getItem("user"));
    
    // get last 7 days user retentions
    fetch("https://localhost:5001/api/users/retentions/7", {
      headers: {
        "Authorization": `Bearer ${authUser.access_token}`
      }
    })
    .then((res) => res.json())
    .then((data) => {
      data.forEach(element => {
        element.day = new Date(element.day)
      });

      this.setState({
        chartData: data
      });

      console.log(this.state.chartData);
    });
  }

  // componentDidMount() {
  //   this.getUserRetentions();
  // }

  render () {
    return (
      <div>
        <ChartComponent primaryXAxis={this.state.xAxis} primaryYAxis={this.state.yAxis} title="User Retentions last 7 days">
          <Inject services={[BarSeries, Legend, Tooltip, DataLabel, Category, DateTime]}/>
          <SeriesCollectionDirective>
            <SeriesDirective dataSource={this.state.chartData} xName="day" yName="liveUsers" type="Bar">
            </SeriesDirective>
          </SeriesCollectionDirective>
        </ChartComponent>
        <button className="btn btn-primary" onClick={this.getUserRetentions}>Calculate</button>
      </div>
    );
  }
}