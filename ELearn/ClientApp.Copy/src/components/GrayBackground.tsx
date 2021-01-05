import React from 'react'
import {Helmet} from 'react-helmet';

export interface Props {
    [key: string]: any
}

const GrayBackground = (props: Props) => {

    return (
        <Helmet>
            <style>{'body { background-color: #F4F4F4; }'}</style>
        </Helmet>
    );
}

export default GrayBackground;