import React, { useEffect, useState } from 'react'
import ShipsService from '../API/ShipsService';
import { Table } from "antd";
import ShipFilters from '../Components/ShipFilters';
import { Link } from 'react-router-dom'

function ShipsPage() {

  const [screenSize, getDimension] = useState({
    dynamicWidth: window.innerWidth,
    dynamicHeight: window.innerHeight
  });

  const setDimension = () => {
    getDimension({
      dynamicWidth: window.innerWidth,
      dynamicHeight: window.innerHeight
    })
  }

  useEffect(() => {
    window.addEventListener('resize', setDimension);

    return (() => {
      window.removeEventListener('resize', setDimension);
    })
  }, [screenSize])


  const [ships, setShips] = useState([])
  const [state, setState] = useState({
    data: [],
    pagination: {
      current: 1,
      pageSize: 10,
    },
    loading: false
  });
  const [query, setQuery] = useState();

  const columns = [
    {
      title: 'Рег. номер',
      dataIndex: 'regNumber',
      render: (text, row, index) => <Link to={`/ship/${ships[index]["shipId"]}`}>{text}</Link>
    },
    {
      title: 'Проект',
      dataIndex: 'project',

    },
    {
      title: 'Год постройки',
      sorter: true,
      dataIndex: 'year',
    },
  ];

  const getShips = (query, pagination) => {
    setState({
      ...state,
      loading: true
    })
    ShipsService.Get(query, (data) => updateShips(data, pagination))
  }

  const handleTableChange = (pagination, filters, sorter) => {

    let pagedQuery = query ? query + `&pageSize=${pagination.pageSize}&page=${pagination.current}` :
    `?pageSize=${pagination.pageSize}&page=${pagination.current}`;

    console.log(sorter.order)

    if(sorter.field)
    {
      pagedQuery = applySorter(pagedQuery, sorter);
      console.log("yes");
    }

    getShips(pagedQuery, pagination);
  };

  const applySorter = (query, sorter) =>{

    let order = sorter.order == 'ascend' ? 'asc' : 'desc'

    let sortedQuery = query ? query += `&OrderBy=${sorter.field} ${order}` : `?OrderBy=${sorter.field} ${order}`;
    return sortedQuery
  }

  const updateShips = (data, pagination) => {
    setShips(data.data);
    setState({
      ...state,
      loading: false,
      data: data.data,
      pagination: {
        ...pagination,
        total: data.count
      }
      
    })
  }

  useEffect(() => {
    ShipsService.Get(`?PageSize=${state.pagination.pageSize}`, (data) => updateShips(data, state.pagination))
  }, []);

  const { data, pagination, loading } = state;

  return (
    <div style={{
      width: '100%',
      height: '100%',
      minHeight: 600
    }}>
      <h1 style={{
        paddingTop: 10
      }}>Суда</h1>
      <div style={{
        display: 'flex',
        paddingTop: 10,
        height: screenSize.dynamicHeight - 54
      }}>
        <div
          style={{
            backgroundColor: 'white',
            height: '',
            width: "20%",
            borderRight: "1px solid #f0f0f0",
            height: screenSize.dynamicHeight - 64
          }}>
          <div style={{
            backgroundColor: "#fafafa",
            height: 55,
            fontWeight: 500,
            borderBottom: "1px solid #f0f0f0",
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center',
          }} >
            <span>Фильтры</span>
          </div>
          <ShipFilters onApply={(query) => {
            setQuery(query);
            getShips(query);
          }} />
        </div>
        <Table style={{
          width: "80%",
          backgroundColor: 'white',
        }}
          scroll={{ y: screenSize.dynamicHeight - 250 }}
          rowKey="shipId"
          dataSource={ships}
          columns={columns}
          pagination={pagination}
          loading={loading}
          onChange={handleTableChange}
        />
      </div>
    </div>
  )
}

export default ShipsPage