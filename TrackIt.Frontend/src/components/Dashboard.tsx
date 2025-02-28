import React from "react";
import "../styles/Dashboard.css";
import { Button, Layout } from "antd";
import logo from "../black_on_white.png";
import Sider from "antd/es/layout/Sider";
import { Content, Header } from "antd/es/layout/layout";

const Dashboard = () => {
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
