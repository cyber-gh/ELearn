import React from 'react'

export interface Props {
    level: "Beginner" | "Intermediate" | "Expert";
}
const levels = ["Beginner", "Intermediate", "Expert"];

const LevelIcon = ({level}: Props) => {
    let counter = levels.indexOf(level);
    return (
        <div className="levelIcon">
            {["a", "b", "c"].map((x, index) => (
                <div key={index} className = {x + (index <= counter ? " active" : "")}/>
            ))}
        </div>
    );
}

 export default LevelIcon;