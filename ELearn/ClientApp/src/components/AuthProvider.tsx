import React, {useEffect, useState} from 'react'
import {Alert} from "@material-ui/lab";
import {Snackbar} from "@material-ui/core";
import authService from "./api-authorization/AuthorizeService";

export interface Props {
    [key: string]: any
}

interface SnackbarMessage {
    message: string,
    type: "error" | "success" | "warning" | "info"
}

export const AuthContext = React.createContext<any>(null);
export const AuthProvider = ({ children, ...props }) => {
    const [loading, setLoading] = useState<boolean>(false);
    const [authenticated, setAuthenticated] = useState<boolean | null>(null);

    const getAuth = async () => {
        setLoading(true)
        const isLogged = await authService.isAuthenticated();
        setAuthenticated(isLogged);
        setLoading(false);
    }

    useEffect(() => {
        // authService.subscribe(() => getAuth());
        getAuth();

    }, [])
    
    if (authenticated === null) return <h2>ma uit daca esti logat wai pula</h2>

    return (
        <AuthContext.Provider value={{loading, authenticated}}>
            {children}
        </AuthContext.Provider>
    );
};

