import React from 'react';
import { useAuth } from 'oidc-react';
import { Grid } from '../components/Grid'
import { BarChart } from '../components/BarChart'

export const Home = () => {
  let auth = useAuth();

  if (auth && auth.userData) {
    return(
      <div>
        <Grid></Grid>
        <div className="mt-4">
          <BarChart></BarChart>
        </div>
      </div>
    );
  }
  else {
    auth.signIn();
    return (
      <div></div>
    )
  }
}