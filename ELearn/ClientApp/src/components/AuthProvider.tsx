import React, {useEffect, useState} from 'react'
import {Alert} from "@material-ui/lab";
import {Snackbar} from "@material-ui/core";
import authService from "./api-authorization/AuthorizeService";
import {Profile} from "oidc-client";

export interface Props {
    [key: string]: any
}

export const AuthContext = React.createContext<any>(null);
export const AuthProvider = ({ children, ...props }) => {
    const [loading, setLoading] = useState<boolean>(false);
    const [authenticated, setAuthenticated] = useState<boolean | null>(null);
    const [user, setUser] = useState<Profile | null>(null);

    const getAuth = async () => {
        setLoading(true)
        const isLogged = await authService.isAuthenticated();
        const usr = await authService.getUser();
        setUser(usr);
        setAuthenticated(isLogged);
        setLoading(false);
    }

    useEffect(() => {
        // authService.subscribe(() => getAuth());
        getAuth();

    }, [])
    
    if (authenticated === null) return <div/>

    return (
        <AuthContext.Provider value={{loading, authenticated, user}}>
            {children}
        </AuthContext.Provider>
    );
};

