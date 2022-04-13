import logo from './logo.svg';
import './App.css';
import 'antd/dist/antd.css';
import React from "react";
import { Routes, Route, Link } from 'react-router-dom'
import classes from './App.module.css'

import HomePage from './pages/HomePage'
import ShipsPage from './pages/ShipsPage'
import LoginPage from './pages/LoginPage'

import { Layout, Menu, Typography } from 'antd';
import {
  UserOutlined,
  DatabaseOutlined,
  HomeOutlined,
  CodepenOutlined
} from '@ant-design/icons';
import ShipPage from './pages/ShipPage';

const { Sider, Content } = Layout
const { Title } = Typography

function App() {
  return (
    <div className="App" style={{ height: '100%' }}>
      <Layout style={{ height: '100%' }}>
        <Sider>
          <div className={classes.logo}>
            <CodepenOutlined style={{ color: 'white', marginRight: 8 }} />
            <Title
              level={4}
              style={{ color: "white", display: 'inline' }}
            >
              Регистр судов
            </Title>
          </div>
          <Menu theme='dark' mode='inline'>
            <Menu.Item key={1} icon={<HomeOutlined />}>
              <Link to="/">Главная</Link>
            </Menu.Item>
            <Menu.Item key={2} icon={<DatabaseOutlined />}>
              <Link to="/ships">Суда</Link>
            </Menu.Item>
            <Menu.Item key={3} icon={<UserOutlined />}>
              <Link to="/login">Вход</Link>
            </Menu.Item>
          </Menu>
        </Sider>
        <Content style={{ overflowY: 'scroll' }}>
          <Routes>
            <Route path='/' element={<HomePage />} />
            <Route path='/ships' element={<ShipsPage />} />
            <Route path='/login' element={<LoginPage />} />
            <Route path='/ship/:id' element={<ShipPage />} />
          </Routes>
        </Content>
      </Layout>
    </div>
  );
}

export default App;
