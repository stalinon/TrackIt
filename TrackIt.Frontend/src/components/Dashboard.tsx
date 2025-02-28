import React, { useEffect } from "react";
import "../styles/Dashboard.css";
import { Button, Layout } from "antd";
import logo from "../black_on_white.png";
import Sider from "antd/es/layout/Sider";
import { Content, Header } from "antd/es/layout/layout";
import { UserApi } from "../api/generated";
import api from "../api/api";

const userApi = new UserApi(undefined, api.defaults.baseURL, api);

const getUserProfile = async () => {
  try {
    const userProfile = await userApi.apiUsersProfileGet();
    console.log("Данные профиля:", userProfile.data);
  } catch (error) {
    console.error("Ошибка получения профиля", error);
  }
};

const Dashboard = () => {
  useEffect(() => {
    // Вызов функции для получения данных профиля при монтировании компонента
    getUserProfile();
  }, []); // Пустой массив означает, что эффект сработает только при первом рендере

  return (
    <Layout className="layout">
      <Sider className="layout__sider" width={350}>
        <img className="layout__sider__logo" src={logo} alt="logo" />
        <Button className="layout__sider__btn" type="text">
          Dashboard
        </Button>
        <Button className="layout__sider__btn" type="text">
          Transactions
        </Button>
        <Button className="layout__sider__btn" type="text">
          Limits
        </Button>
        <Button className="layout__sider__btn" type="text">
          Planned payments
        </Button>
      </Sider>
      <Content></Content>
    </Layout>
  );
};

export default Dashboard;
