import React from 'react'

export interface Props {
    letter: string,
}

const Avatar = (props: Props) => {

    return (
        <div className="avatar">
            {props.letter}
        </div>
    );
}

export default Avatar;