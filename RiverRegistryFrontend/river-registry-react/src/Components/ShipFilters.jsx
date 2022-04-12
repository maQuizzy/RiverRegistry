import React from 'react'
import { useState } from 'react'
import { Input, Slider, Button } from 'antd'

function ShipFilters(props) {

    const [filter, setFilter] = useState();

    const handleChange = e => {
        const { name, value } = e.target;
        setFilter(prevState => ({
            ...prevState,
            [name]: value
        }));
    }

    const handleSliderChange = (name, value) => {
        setFilter(prevState => ({
            ...prevState,
            [name+"From"]: value[0],
            [name+"To"]: value[1]
        }));
    }

    const applyFilter = () => {
        let query = "";

        let project = filter.project ? "project=*" + filter.project : "" ;
        let buildNumber = filter.buildNumber ? "buildNumber=*" + filter.buildNumber : "" ;
        let category = filter.category ? "category=*" + filter.category : "" ;
        let yearFrom = filter.yearFrom ? "year>=" + filter.yearFrom : "" ;
        let yearTo = filter.yearFrom ? "year<=" + filter.yearTo: "" ;

        [project,buildNumber,category, yearFrom, yearTo].forEach(e =>{
            if(e){
                if(query){
                    query+=', '
                }
                query+= e;
            }
        })

        console.log(query);
        query = "?Filter=" + query;

        props.onApply(query)
    }

    return (
        <div style={{
            padding: 10
        }}>
            <span>Проект</span>
            <Input onChange={handleChange} name="project" placeholder="Проект" />
            <span>Строительный номер</span>
            <Input onChange={handleChange} name="buildNumber" placeholder="Стр. номер" />
            <span>Категория</span>
            <Input onChange={handleChange} name="category" placeholder="Категория" />
            <span>Год постройки</span>
            <Slider onChange={value => handleSliderChange("year", value)} range defaultValue={[1900, 2022]} max={2022} min={1900} />
            <Button onClick={applyFilter} type="primary">Применить</Button>
        </div>
    )
}
export default ShipFilters