import React from 'react'
import {NotificationsNone as Bell, Search} from '@material-ui/icons';
import AddIcon from '@material-ui/icons/Add';
import { Link } from 'react-router-dom';

export interface Props {
    [key: string]: any
}

export default (props: Props) => {

    return (
        <div className = "navbar">
            <div className = "nav_1">
                <Link to = "/">
                    <h2>FakeShare</h2>
                </Link>
                <p>Browse</p>
                <div className = "input_box">
                    <Search className = "search_icon"/>
                    <input className = "navbar_input"/>
                </div>
            </div>



            
            <div className = "nav_2">
                <p>My Classes</p>
                <Link to="/add-course">
                    <AddIcon className="notifications"/>
                </Link>
            </div>

            

           
        </div>
    );
}