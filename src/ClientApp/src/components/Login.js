import React from 'react';
import { useAuth } from 'oidc-react';

export const Login = () => {
    let auth = useAuth();
    if (auth && auth.userData) {
        return(
            <ul className="navbar-nav">
                <li className="nav-item">
                    <a className="nav-link text-dark" href="https://localhost:6001/Identity/Account/Manage">
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
                    <a className="nav-link text-dark" href="https://localhost:6001/Identity/Account/Register">Register</a>
                </li>
                <li className="nav-item">
                    <button className="btn btn-link nav-link text-dark" onClick={() => auth.signIn()}>Login</button>
                </li>
            </ul>
        );
    }
}