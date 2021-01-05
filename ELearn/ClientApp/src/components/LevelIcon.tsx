import React from 'react'

export interface Props {
    level: "Begginer" | "Intermediate" | "Expert";
}
const levels = ["Begginer", "Intermediate", "Expert"];

const LevelIcon = ({level}: Props) => {
    let counter = levels.indexOf(level);
    return (
        <div className="levelIcon">
            {["a", "b", "c"].map((x, index) => (
                <div className = {x + (index <= counter ? " active" : "")}/>
            ))}
        </div>
    );
}

 export default LevelIcon;