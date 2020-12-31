import React, {useState} from 'react'
import {Alert} from "@material-ui/lab";
import {Snackbar} from "@material-ui/core";

export interface Props {
    [key: string]: any
}

interface SnackbarMessage {
    message: string,
    type: "error" | "success" | "warning" | "info"
}

export const SnackbarContext = React.createContext<any>(null);
export const SnackbarProvider = ({ children, ...props }) => {

    const [data, setData] = useState<SnackbarMessage | null>({message: "tst", type: "warning"});
    
    const closeSnackbar = () => {
        setData(null)
    }
    
    return (
        <SnackbarContext.Provider value={{setData}}>
            <>
                {data && 
                    <Snackbar anchorOrigin={{vertical: "top", horizontal: "center"}} open autoHideDuration={6000} onClose={closeSnackbar}>
                        <Alert onClose={closeSnackbar} severity={data.type}>
                            {data.message}
                        </Alert>
                    </Snackbar>
                }
                {children}
            </>
        </SnackbarContext.Provider>
    );
};

