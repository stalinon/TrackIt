import React from "react";
import { useKeycloak } from "@react-keycloak/web";
import MainPage from "./components/MainPage";
import Dashboard from "./components/Dashboard";

const App = () => {
  const { keycloak } = useKeycloak();

  return keycloak.authenticated ? <Dashboard /> : <MainPage />;
};

export default App;
