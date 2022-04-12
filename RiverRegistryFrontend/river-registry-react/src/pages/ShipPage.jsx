import React from 'react'
import ShipsService from '../API/ShipsService';
import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react'
import { Collapse, Card } from 'antd';

const { Panel } = Collapse;

function ShipPage() {

    const params = useParams();

    const [ship, setShip] = useState()

    const contentList = ship ? {
        tab1: <div>
            <p>{`Регистрационный номер: ${ship.regNumber}`}</p>
            <p>{`Строительный номер: ${ship.buildNumber}`}</p>
            <p>{`Проект: ${ship.project}`}</p>
            <p>{`Тип: ${ship.type}`}</p>
            <p>{`Дата постройки: ${ship.buildDate}`}</p>
            <p>{`Год: ${ship.year}`}</p>
            <p>{`Место постройки: ${ship.buildPlace}`}</p>
            <p>{`Название: ${ship.name}`}</p>
            <p>{`Категория: ${ship.category}`}</p>
            <p>{`Материал корпуса: ${ship.bodyMaterial}`}</p>
            <p>{`Материал надстройки: ${ship.superStructMaterial}`}</p>
            <p>{`Водяной балласт: ${ship.waterBallast}`}</p>
            <p>{`Условия плавания (район и сезон): ${ship.sailingConditions}`}</p>
        </div>,
        tab2: <div>
            <p>{`Валовая вместимость: ${ship.shipCapacity.grossTonnage}`}</p>
            <p>{`Чистая вместимость: ${ship.shipCapacity.netTonnage}`}</p>
            <p>{`Дедвейт: ${ship.shipCapacity.deadweight}`}</p>
            <p>{`Водоизмещение: ${ship.shipCapacity.displacement}`}</p>
            <p>{`Грузоподъемность (т): ${ship.shipCapacity.carrying}`}</p>
            <p>{`Переборок поперечных: ${ship.shipCapacity.transBulk}`}</p>
            <p>{`Переборок продольных: ${ship.shipCapacity.longBulk}`}</p>
            <p>{`Пассажировместимость: ${ship.shipCapacity.passenger}`}</p>
            <p>{`Экипаж: ${ship.shipCapacity.crew}`}</p>
            <p>{`Орг. группа: ${ship.shipCapacity.orgGroup}`}</p>
            <p>{`Кол-во аливных танков: ${ship.shipCapacity.bulkTanks}`}</p>
            <p>{`Сумм. объем танков: ${ship.shipCapacity.volumeTanks}`}</p>
            <p>{`Г/п 1-ой стрелы: ${ship.shipCapacity.boom1}`}</p>
            <p>{`Г/п 2-ой стрелы: ${ship.shipCapacity.boom2}`}</p>
            <p>{`Г/п 3-ой стрелы: ${ship.shipCapacity.boom3}`}</p>
        </div>,
        tab3: <div>
            <p>{`Длина габаритная: ${ship.shipDimensions.overLength}`}</p>
            <p>{`Длина конструктивная: ${ship.shipDimensions.constrLength}`}</p>
            <p>{`Ширина габаритная: ${ship.shipDimensions.overWidth}`}</p>
            <p>{`Ширина конструктивная: ${ship.shipDimensions.constrWidth}`}</p>
            <p>{`Надводный борт: ${ship.shipDimensions.freeboard}`}</p>
            <p>{`Высота борта: ${ship.shipDimensions.boardHeight}`}</p>
        </div>,
        tab4: <div>
            <p>{`Тип гл. ДВС: ${ship.shipEngines.mainICEType}`}</p>
            <p>{`Марка гл. ДВС: ${ship.shipEngines.mainICEBrand}`}</p>
            <p>{`Мощность гл. ДВС (кВт): ${ship.shipEngines.mainICEPower}`}</p>
            <p>{`Кол. главных ДВС: ${ship.shipEngines.mainICECount}`}</p>
            <p>{`ГЭД, всего: ${ship.shipEngines.rmCount}`}</p>
            <p>{`ГЭД, кВт всех: ${ship.shipEngines.rmPower}`}</p>
            <p>{`ГЭС, кВт всех: ${ship.shipEngines.hdPower}`}</p>
            <p>{`Запас топлива: ${ship.shipEngines.fuelCapacity}`}</p>
        </div>,
    } : {};

    const tabList = [
        {
            key: 'tab1',
            tab: 'Основная информация',
        },
        {
            key: 'tab2',
            tab: 'Вместимость',
        },
        {
            key: 'tab3',
            tab: 'Габариты',
        },
        {
            key: 'tab4',
            tab: 'Двигатели',
        },
    ];

    const [activeTabKey, setActiveTabKey] = useState('tab1');

    const onTabChange = key => {
        setActiveTabKey(key);
    };

    useEffect(() => {
        ShipsService.Get(`?Filter=shipId=${params.id}`, (data) => setShip(data.data[0]))
    }, []);

    return (
        ship ?
            <div style={{
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center'
            }}>
                <div style={{
                    width: '80%',
                }}>
                    <Card
                        style={{ width: '100%', marginTop: 10 }}
                        title={`Судно (Рег.номер ${ship.regNumber})`}
                        tabList={tabList}
                        activeTabKey={activeTabKey}
                        onTabChange={key => {
                            onTabChange(key);
                        }}
                    >
                        {contentList[activeTabKey]}
                    </Card>
                </div>
            </div> :
            <div>
                <span>Not found</span>
            </div>
    )
}

export default ShipPage