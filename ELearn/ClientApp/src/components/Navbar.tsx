import React, {useCallback, useContext, useEffect, useState} from 'react'
import {More, NotificationsNone as Bell, Search} from '@material-ui/icons';
import AddIcon from '@material-ui/icons/Add';
import { Link } from 'react-router-dom';
import {LoginMenu} from "./api-authorization/LoginMenu";
import {LoginMenuV2} from "./api-authorization/LoginMenuTs";
import {AuthContext} from "./AuthProvider";
import BrowseContext from "./BrowseContext";
import MenuIcon from '@material-ui/icons/Menu';
import SearchContext from "./SearchContext";
import {debounce, Menu} from "@material-ui/core";
import {breakpoints} from "../utils";
import Avatar from "./Avarat";

export interface Props {
    [key: string]: any
}

export default (props: Props) => {
    console.log(props);
    const {authenticated, user} = useContext(AuthContext);
    const [context, setContext] = useState <0 | 1 | 2> (0);
    const [search, setSearch] = useState("");
    const [mobileMenu, setMobileMenu] = useState(false);
    const [menuState, setMenuState] = useState(false);
    console.log(mobileMenu);
    
    const handleBrowseClick = () => {
        if (context != 1)
            setContext(1);
        else
            setContext(0);
    }
    
    const handleInputClick = () => {
        if (context != 2)
            setContext(2);
    }
    const handleInputChange = useCallback(debounce((e: any) => {
        console.log(e);
        if (context != 2)
            setContext(2);
        setSearch(e.target.value);
    }, 100), []);
    
    const handleMenuClick = () => {
        setMenuState(!menuState);
    }
    
    const close = () => {
        setContext(0);
    }

    useEffect(() => {
        const handleResize = () => {
            const w = window.innerWidth;
            const {mobile, tablet, smallScreen, largeScreen} = breakpoints;
            if (w <= tablet) {
                setMobileMenu(true);
            }
            else {
                setMobileMenu(false);
            }
        }
        const listener = (e) => {
            if (!e.target.classList.contains("exclude-1")) {
                close();
            }
        }
        handleResize();
        window.addEventListener("resize", handleResize);
        window.addEventListener("click", listener);
        return () => {
            window.removeEventListener("resize", handleResize);
            window.removeEventListener("click", listener);
        }
    }, [])
    const desktop = () => (
        <div className = "navbar desktop">
            <div className = "nav_1">
                <Link to = "/">
                    <h2>FakeShare</h2>
                </Link>
                <div className="browse-container">
                    <p className = "exclude-1" onClick={handleBrowseClick}>Browse</p>
                    <BrowseContext close = {close} open={context===1}/>
                </div>
                <div className = "input_box">
                    <Search className = "search_icon"/>
                    <input onClick={handleInputClick} onChange={handleInputChange} className = "navbar_input exclude-1" placeholder="Search for a course"/>
                    <SearchContext close = {close} open = {context===2} searchKey = {search}/>
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
    )
    const mobile = () => (
        <div className = "navbar mobile">
            <Link to = "/">
                <h2>FakeShare</h2>
            </Link>
            <MenuIcon onClick={handleMenuClick}/>
            <div className={`more ${menuState ? "disabled" : ""}`}>
                <div className="browse-container element top bot">
                    <p className = "exclude-1" onClick={handleBrowseClick}>Browse</p>
                    <BrowseContext close = {close} open={context===1}/>
                </div>
                <div className = "row bot">
                    <LoginMenuV2/>
                </div>
                <div className = "input_box bot">
                    <Search className = "search_icon"/>
                    <input onClick={handleInputClick} onChange={handleInputChange} className = "navbar_input exclude-1" placeholder="Search for a course"/>
                    <SearchContext close = {close} open = {context===2} searchKey = {search}/>
                </div>
            </div>
        </div>
    )
    return mobileMenu ? mobile() : desktop();
}