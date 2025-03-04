import { useState } from "react";
import { Button, Layout } from "antd";
import logo from "../black_on_white.png";
import short_logo from "../black_on_white_short.png";

import DashboardContent from "./DashboardContent";
import TransactionsContent from "./TransactionsContent";
import LimitsContent from "./LimitsContent";
import PlannedPaymentsContent from "./PlannedPaymentsContent";
import CategoriesContent from "./CategoriesContent";

import "../styles/Dashboard.css";

// Импортируем иконки
import {
  MenuFoldOutlined,
  MenuUnfoldOutlined,
  HomeOutlined,
  CreditCardOutlined,
  SafetyCertificateOutlined,
  CalendarOutlined,
  AlignLeftOutlined,
} from "@ant-design/icons";

const { Header, Sider, Content } = Layout;

const Dashboard = () => {
  const [activePage, setActivePage] = useState("dashboard");
  const [collapsed, setCollapsed] = useState(false);

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
      case "categories":
        return <CategoriesContent />;
      default:
        return <DashboardContent />;
    }
  };

  return (
    <Layout className="layout">
      <Sider
        className="layout__sider"
        width={250}
        trigger={null}
        collapsible
        collapsed={collapsed}
      >
        <img
          className="layout__sider__logo"
          src={collapsed ? short_logo : logo}
          alt="logo"
          style={{ marginLeft: collapsed ? "15px" : "0" }}
        />
        <Button
          className="layout__sider__btn"
          type="text"
          style={{ justifyContent: collapsed ? "center" : "flex-start" }}
          onClick={() => setActivePage("dashboard")}
          icon={<HomeOutlined />}
        >
          &nbsp;
          {collapsed ? "" : "Dashboard"}
        </Button>
        <Button
          className="layout__sider__btn"
          type="text"
          style={{ justifyContent: collapsed ? "center" : "flex-start" }}
          onClick={() => setActivePage("categories")}
          icon={<AlignLeftOutlined />}
        >
          &nbsp;
          {collapsed ? "" : "Categories"}
        </Button>
        <Button
          className="layout__sider__btn"
          type="text"
          style={{ justifyContent: collapsed ? "center" : "flex-start" }}
          onClick={() => setActivePage("transactions")}
          icon={<CreditCardOutlined />}
        >
          &nbsp;
          {collapsed ? "" : "Transactions"}
        </Button>
        <Button
          className="layout__sider__btn"
          type="text"
          style={{ justifyContent: collapsed ? "center" : "flex-start" }}
          onClick={() => setActivePage("limits")}
          icon={<SafetyCertificateOutlined />}
        >
          &nbsp;
          {collapsed ? "" : "Limits"}
        </Button>
        <Button
          className="layout__sider__btn"
          type="text"
          style={{ justifyContent: collapsed ? "center" : "flex-start" }}
          onClick={() => setActivePage("plannedPayments")}
          icon={<CalendarOutlined />}
        >
          &nbsp;
          {collapsed ? "" : "Planned payments"}
        </Button>
      </Sider>
      <Header style={{ padding: 0, background: "#f0f0f0" }}>
        <Button
          type="text"
          icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
          onClick={() => setCollapsed(!collapsed)}
          style={{
            fontSize: "16px",
            width: 64,
            height: 64,
          }}
        />
      </Header>
      <Content className="layout__content">{renderContent()}</Content>
    </Layout>
  );
};

export default Dashboard;
