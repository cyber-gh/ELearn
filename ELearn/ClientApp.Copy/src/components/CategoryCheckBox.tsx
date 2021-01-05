import React from 'react'

export interface Props {
    checked: boolean,
    name: string,
    onClick: (e: any) => void
}

const CategoryCheckBox = ({checked, name, onClick}: Props) => {

    return (
        <div onClick={onClick} className="category-box">
            <input  type = "checkbox" checked={checked}/>
            <p>{name}</p>
        </div>
    );
}

export default CategoryCheckBox;