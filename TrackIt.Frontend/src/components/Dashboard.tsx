import React, { useEffect, useState } from "react";
import "../styles/Dashboard.css";
import { Button, Layout } from "antd";
import logo from "../black_on_white.png";
import Sider from "antd/es/layout/Sider";
import { Content } from "antd/es/layout/layout";
import { UserApi } from "../api/generated";
import api from "../api/api";

import DashboardContent from "./DashboardContent";
import TransactionsContent from "./TransactionsContent";
import LimitsContent from "./LimitsContent";
import PlannedPaymentsContent from "./PlannedPaymentsContent";

// Импортируем иконки
import {
  HomeOutlined,
  CreditCardOutlined,
  SafetyCertificateOutlined,
  CalendarOutlined,
} from "@ant-design/icons";

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
  const [activePage, setActivePage] = useState("dashboard");

  useEffect(() => {
    getUserProfile();
  }, []);

  // Функция для рендеринга контента в зависимости от активной страницы
  const renderContent = () => {
    switch (activePage) {
      case "dashboard":
        return <DashboardContent />;
      case "transactions":
        return <TransactionsContent />;
      case "limits":
        return <LimitsContent />;
      case "plannedPayments":
        return <PlannedPaymentsContent />;
      default:
        return <DashboardContent />;
    }
  };

  return (
    <Layout className="layout">
      <Sider className="layout__sider" width={350}>
        <img className="layout__sider__logo" src={logo} alt="logo" />
        <Button
          className="layout__sider__btn"
          type="text"
          onClick={() => setActivePage("dashboard")}
          icon={<HomeOutlined />}
        >
          Dashboard
        </Button>
        <Button
          className="layout__sider__btn"
          type="text"
          onClick={() => setActivePage("transactions")}
          icon={<CreditCardOutlined />}
        >
          Transactions
        </Button>
        <Button
          className="layout__sider__btn"
          type="text"
          onClick={() => setActivePage("limits")}
          icon={<SafetyCertificateOutlined />}
        >
          Limits
        </Button>
        <Button
          className="layout__sider__btn"
          type="text"
          onClick={() => setActivePage("plannedPayments")}
          icon={<CalendarOutlined />}
        >
          Planned payments
        </Button>
      </Sider>
      <Content className="layout__content">{renderContent()}</Content>
    </Layout>
  );
};

export default Dashboard;
