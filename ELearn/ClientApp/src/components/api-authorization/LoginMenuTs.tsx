import React, {Component, Fragment, useContext} from 'react';
import {AuthContext} from "../AuthProvider";
import {ApplicationPaths} from "./ApiAuthorizationConstants";
import {Link} from "react-router-dom";
import {Login} from "./Login";
import Avarat from "../Avarat";
import Avatar from "../Avarat";


export const LoginMenuV2 = () => {
    const {authenticated, user} = useContext(AuthContext);
    if (!authenticated || !user) {
        const registerPath = `${ApplicationPaths.Register}`;
        const loginPath = `${ApplicationPaths.Login}`;
        
        return (
            <>
                <Link to={registerPath}>Register</Link>
                <Link to={loginPath}>Login</Link>
            </>
        ) 
    } else {
        console.log(user);
        const profilePath = `${ApplicationPaths.Profile}`;
        const logoutPath = { pathname: `${ApplicationPaths.LogOut}`, state: { local: true } };
        
        console.log("user is")
        console.log(Object.keys(user))
        return (
            <>
                <Link to={profilePath} className = "avatar-container">
                    <Avatar letter = {"P"}/>
                </Link>
                {/*<Link to={profilePath}>Profile</Link>*/}
                <Link to={logoutPath}>Logout</Link>
            </>
        )
    }
}