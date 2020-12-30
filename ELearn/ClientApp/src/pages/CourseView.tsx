import { AnyARecord } from 'dns';
import React from 'react'
import { RouteComponentProps } from 'react-router-dom';

export interface Props {
    match: {
        params: {
            id: string
        }
    }
}

const CourseView = ({match}: Props) => {
    return (
        <h2>{match.params.id }</h2>
    );
}

 export default CourseView;