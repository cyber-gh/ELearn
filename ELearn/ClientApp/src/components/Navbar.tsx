import React, {useContext} from 'react'
import {NotificationsNone as Bell, Search} from '@material-ui/icons';
import AddIcon from '@material-ui/icons/Add';
import { Link } from 'react-router-dom';
import {LoginMenu} from "./api-authorization/LoginMenu";
import {LoginMenuV2} from "./api-authorization/LoginMenuTs";
import {AuthContext} from "./AuthProvider";

export interface Props {
    [key: string]: any
}

export default (props: Props) => {

    const {authenticated, user} = useContext(AuthContext);
    
    return (
        <div className = "navbar">
            <div className = "nav_1">
                <Link to = "/">
                    <h2>FakeShare</h2>
                </Link>
                <p>Browse</p>
                <div className = "input_box">
                    <Search className = "search_icon"/>
                    <input className = "navbar_input" placeholder="Search for a course"/>
                </div>
            </div>
            <div className = "nav_2">
                {authenticated && 
                <>
                    <Link to="/my-classes">
                        <p className={"left"}>My Classes</p>
                    </Link>
                    <Link to="/add-course">
                        <p>Add class</p>
                    </Link>
                </>}
                <LoginMenuV2/>
            </div>
        </div>
    );
}