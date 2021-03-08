import React, { Component } from 'react';
import { OidcConfig } from '../authentication/OidcConfig';
import { ColumnDirective, ColumnsDirective, GridComponent, 
  Inject, Page, Sort, Edit, Toolbar } from '@syncfusion/ej2-react-grids';

export class Grid extends Component {
  static displayName = Grid.name;

  constructor(props) {
    super(props);
    this.state = {
      users: [],
      grid: {
        pageSettings: {
          pageSize: 10
        },
        editSettings: {
          allowEditing: true,
          mode: "Dialog"
        },
        toolbar: [
          "Edit"
        ],
      }
    }
  }

  getUsers = () => {
    let authUser = JSON.parse(localStorage.getItem("user"));
    
    fetch(`https://${OidcConfig.apiHost}/api/users`, {
      headers: {
        "Authorization": `Bearer ${authUser.access_token}`
      }
    })
    .then((res) => res.json())
    .then((data) => {
      this.setState({
        users: data
      });
    });
  }

  saveUserData = (user) => {
    let authUser = JSON.parse(localStorage.getItem("user"));

    fetch(`https://${OidcConfig.apiHost}/api/users/${user.id}`, {
      headers: {
        "Authorization": `Bearer ${authUser.access_token}`,
        "Content-Type": "application/json"
      },
      method: "PUT",
      body: JSON.stringify(user)

    })
    .then((res) => console.log(res));
  }

  gridActionComplete = (args) => {
    if ((args.requestType === "beginEdit" || args.requestType === "add")) {
      const dialog = args.dialog;
      dialog.header = args.requestType === "beginEdit" ?
        "Record of " +  args.rowData.userName : "New Customer";
    }

    if (args.requestType === "save") {
      this.saveUserData(args.rowData);
    }
  }

  componentDidMount() {
    this.getUsers();
  }

  render () {
    return (
      <GridComponent dataSource={this.state.users} allowPaging={true} allowSorting={true}
      pageSettings={this.state.grid.pageSettings} editSettings={this.state.grid.editSettings} 
      toolbar={this.state.grid.toolbar} actionComplete={this.gridActionComplete}>
        <ColumnsDirective>
            <ColumnDirective field="id" headerText="ID" isPrimaryKey={true} width="100"/>
            <ColumnDirective field="userName" headerText="User Name" allowEditing={false} width="100"/>
            <ColumnDirective field="registrationDate" headerText="Registration Date" editType="datepickeredit" type="date" format="dd/MM/yyyy" width="100"/>
            <ColumnDirective field="lastActivityDate" headerText="Last Activity Date" editType="datepickeredit" type="date" format="dd/MM/yyyy" width="100"/>
        </ColumnsDirective>
        <Inject services={[Page, Sort, Edit, Toolbar]} />
      </GridComponent>
    );
  }
}