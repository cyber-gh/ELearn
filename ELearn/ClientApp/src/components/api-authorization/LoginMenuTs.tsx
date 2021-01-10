import React, {Component, Fragment, useContext} from 'react';
import {AuthContext} from "../AuthProvider";
import {ApplicationPaths} from "./ApiAuthorizationConstants";
import {Link} from "react-router-dom";
import {Login} from "./Login";
import Avarat from "../Avarat";
import Avatar from "../Avarat";

export interface Props {
    mobile?: boolean
}


export const LoginMenuV2 = ({mobile}: Props) => {
    const {authenticated, user} = useContext(AuthContext);
    if (!authenticated || !user) {
        const registerPath = `${ApplicationPaths.Register}`;
        const loginPath = `${ApplicationPaths.Login}`;
        
        return (
            <>
                <Link to={registerPath} className = "bot">Register</Link>
                <Link to={loginPath} className = "bot">Login</Link>
            </>
        ) 
    } else if (!mobile) {
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
    else{
        const profilePath = `${ApplicationPaths.Profile}`;
        const logoutPath = { pathname: `${ApplicationPaths.LogOut}`, state: { local: true } };
        
        console.log("user is")
        console.log(Object.keys(user))
        return (
            <>
                <Link to={profilePath} className="bot">Profile</Link>
                <Link to={logoutPath} className="bot">Logout</Link>
            </>
        )
    }
}