import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import { AuthProvider } from 'oidc-react'
import { OidcConfig } from './authentication/OidcConfig'
import { Layout } from './components/Layout'
import { Home }  from './pages/Home'

import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css'

function App() {
  return (
    <AuthProvider {...OidcConfig}>
      <Router>
        <Switch>
          <Layout>
            <Route exact path='/' component={Home} />
          </Layout>
        </Switch>
      </Router>
    </AuthProvider>
  );
}

export default App;
