import React from 'react'
import { Form, Input, Button, Checkbox } from 'antd';
import AccountService from '../API/AccountService';


function LoginPage() {

  const onFinish = (values) => {
    console.log('Success:', values);
    console.log(AccountService.Login(values.username, values.password));
  };

  const onFinishFailed = (errorInfo) => {
    console.log('Failed:', errorInfo);
  };

  return (
    <div
      style={
        {
          display: 'flex',
          justifyContent: 'center',
          paddingTop: 50,
          textAlign: 'left'
        }
      }>
      <Form
        name="basic"
        labelCol={{
          span: 8,
        }}
        wrapperCol={{
          span: 16,
        }}
        initialValues={{
          remember: true,
        }}
        onFinish={onFinish}
        onFinishFailed={onFinishFailed}
        autoComplete="off"
      >
        <h1 style={{
          textAlign: 'center'
        }}>
          Вход
        </h1>
        <Form.Item
          label="Никнейм"
          name="username"
          rules={[
            {
              required: true,
              message: 'Введите никнейм!',
            },
          ]}
        >
          <Input />
        </Form.Item>

        <Form.Item
          label="Пароль"
          name="password"
          rules={[
            {
              required: true,
              message: 'Введите пароль!',
            },
          ]}
        >
          <Input.Password />
        </Form.Item>

        <Form.Item
          name="remember"
          valuePropName="checked"
          wrapperCol={{
            offset: 8,
            span: 16,
          }}
        >
          <Checkbox>Remember me</Checkbox>
        </Form.Item>

        <Form.Item
          wrapperCol={{
            offset: 8,
            span: 16,
          }}
        >
          <Button type="primary" htmlType="submit">
            Войти
          </Button>
        </Form.Item>
      </Form>
    </div>
  )
}

export default LoginPage