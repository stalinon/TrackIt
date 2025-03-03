import React from "react";
import logo from "../original.png";
import { useKeycloak } from "@react-keycloak/web";
import { Button, theme, Flex } from "antd";
import "../styles/MainPage.css";

const MainPage: React.FC = () => {
  const { keycloak } = useKeycloak();
  const { token } = theme.useToken();

  return (
    <div className="main_page" style={{ backgroundColor: token.colorPrimary }}>
      <div className="main_page__logo_container">
        <img src={logo} alt="logo" className="main_page__logo" />
      </div>
      <div className="main_page__buttons">
        {keycloak.authenticated ? (
          <Button type="primary" onClick={() => keycloak.logout()}>
            Log Out
          </Button>
        ) : (
          <Flex gap="large">
            <Button
              type="primary"
              variant="outlined"
              onClick={() => keycloak.register()}
            >
              Sign Up
            </Button>
            <Button
              type="primary"
              variant="outlined"
              onClick={() => keycloak.login()}
            >
              Log In
            </Button>
          </Flex>
        )}
      </div>
    </div>
  );
};

export default MainPage;
