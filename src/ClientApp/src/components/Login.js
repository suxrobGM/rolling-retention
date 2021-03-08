import React from 'react';
import { useAuth } from 'oidc-react';
import { OidcConfig } from '../authentication/OidcConfig';

export const Login = () => {
    let auth = useAuth();
    let manageAccountLink = `https://${OidcConfig.authority}/Identity/Account/Manage`;
    let registerAccountLink = `https://${OidcConfig.authority}/Identity/Account/Register`;
    
    if (auth && auth.userData) {
        return(
            <ul className="navbar-nav">
                <li className="nav-item">
                    <a className="nav-link text-dark" href={manageAccountLink}>
                        <img src="/default_user_avatar.png" width="30" height="30" />
                        &nbsp; {auth.userData.profile.name}
                    </a>
                </li>
                <li className="nav-item">
                    <button className="btn btn-link nav-link text-dark" onClick={() => auth.signOut()}>Logout</button>
                </li>
            </ul>
        );
    }
    else {
        return(
            <ul className="navbar-nav">
                <li className="nav-item">
                    <a className="nav-link text-dark" href={registerAccountLink}>Register</a>
                </li>
                <li className="nav-item">
                    <button className="btn btn-link nav-link text-dark" onClick={() => auth.signIn()}>Login</button>
                </li>
            </ul>
        );
    }
}