import React from 'react'

export interface Props {
    [key: string]: any
}

const Trash = (props: Props) => {

    return (
        <div className="container">
            <div className="item">1</div>
            <div className="item">2</div>
            <div className="item">3</div>
            <div className="item">4</div>
            <div className="item">5</div>
        </div>
    );
}

export default Trash;