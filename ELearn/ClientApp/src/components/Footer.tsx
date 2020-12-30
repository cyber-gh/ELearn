import React from 'react'
import Linkedin from '@material-ui/icons/LinkedIn';
import Github from '@material-ui/icons/GitHub';
import Facebook from '@material-ui/icons/Facebook';
import { Link } from 'react-router-dom';

export interface Props {
    [key: string]: any
}

const Footer = (props: Props) => {

    return (
        <section className="footer">
            <div className = "first">
                <p className="copyright">
                    © FakeShare, Inc. 2020
                </p>
            </div>
            <div className="second">
                <Link to = "/">
                    <Linkedin className = "icon"/> 
                </Link>
                <Link to = "/">
                    <Github className = "icon"/> 
                </Link>
                <Link to = "/"> 
                    <Facebook className = "icon"/> 
                </Link>
            </div>
        </section>
    );
}

 export default Footer;