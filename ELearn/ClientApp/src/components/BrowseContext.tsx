import history from "../history";
import React, {useEffect, useState} from 'react'
import {Category} from "../interfaces";
import {getCategories} from "../api";
import {Link} from "react-router-dom"

interface Props {
    open: boolean,
    className?: string,
    close: () => void,
}

const BrowseContext = ({open, close, className: classes}: Props) => {
    const [data, setData] = useState <null | Category[]> (null);
    
    const getData = async () => {
        let categories = await getCategories();
        console.log(categories);
        setData(categories);
    }
    
    useEffect(() => {
        getData();
    }, [])

    const goTo = (link) => (e) => {
        close();
        history.push(link);
    }

    if (!open || !data) return null;
    
    return (
        <div className={"menu-context " + (classes ? classes: "")}>
            <p className="title">
                Categories:
            </p>
            <div className="content">
                {data && data.map(x => (
                    <div onClick={goTo(`/courses/${x.name}`)} key = {x.id} className="element">
                        {x.name}
                    </div>
                ))}
            </div>
        </div>
    );
}

export default BrowseContext;