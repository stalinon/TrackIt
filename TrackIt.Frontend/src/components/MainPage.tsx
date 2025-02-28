import React from "react";
import logo from "../original.png";
import { useKeycloak } from "@react-keycloak/web";
import { Button } from "antd";
import "../styles/MainPage.css";

const MainPage: React.FC = () => {
  const { keycloak } = useKeycloak();
  return (
    <div className="main_page">
      <header className="main_page__header">
        <img src={logo} alt="logo" />
        <div className="main_page__header__buttons">
        {keycloak.authenticated ? (
            <Button onClick={() => keycloak.logout()}>Log Out</Button>
          ) : (
            <>
              <Button type="primary" onClick={() => keycloak.login()}>Log In</Button>
              <Button onClick={() => keycloak.register()}>Sign Up</Button>
            </>
          )}
        </div>
      </header>

      <div className="main_page__body">
        <div className="main_page__body__text">
          <p>Take control of your finances with ease!</p>
          <p>Track your income, expenses, and set smart limits – all in one simple app.</p>
          <p>Get personalized insights, plan ahead, and never miss a payment with instant reminders.</p>
          <p>Start managing your budget effortlessly today!</p>
        </div>
        <div className="main_page__body__photo"></div>
      </div>
    </div>
  );
};

export default MainPage;
