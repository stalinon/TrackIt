import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import { Provider } from "react-redux";
import store from "./redux/store";
import reportWebVitals from "./reportWebVitals";

import { ReactKeycloakProvider } from "@react-keycloak/web";
import keycloak from "./keycloak.js";
import App from "./App"; // Создадим App.jsx, который будет решать, что показывать

const root = ReactDOM.createRoot(document.getElementById("root"));

const keycloakProviderInitConfig = {
  initOptions: {
    onLoad: "optional",
    checkLoginIframe: false,
    redirectUri: window.location.origin,
  },
};

const handleEvent = (event, error) => {
  console.log("Keycloak event:", event, error);
};

const handleTokens = (tokens) => {
  console.log("Keycloak token:", tokens.token);
};

root.render(
  <ReactKeycloakProvider
    authClient={keycloak}
    initOptions={keycloakProviderInitConfig.initOptions}
    onEvent={handleEvent}
    onTokens={handleTokens}
  >
    <React.StrictMode>
      <Provider store={store}>
        <App /> {/* Показываем либо MainPage, либо Dashboard */}
      </Provider>
    </React.StrictMode>
  </ReactKeycloakProvider>
);

reportWebVitals();
