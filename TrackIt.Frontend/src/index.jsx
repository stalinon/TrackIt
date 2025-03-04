import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import { Provider } from "react-redux";
import store from "./redux/store";
import reportWebVitals from "./reportWebVitals";

import { ReactKeycloakProvider } from "@react-keycloak/web";
import keycloak from "./keycloak.js";
import App from "./App";

const root = ReactDOM.createRoot(document.getElementById("root"));

const keycloakProviderInitConfig = {
  initOptions: {
    onLoad: "optional",
    checkLoginIframe: false,
    redirectUri: window.location.origin,
  },
};

root.render(
  <ReactKeycloakProvider
    authClient={keycloak}
    initOptions={keycloakProviderInitConfig.initOptions}
  >
    <React.StrictMode>
      <Provider store={store}>
        <App />
      </Provider>
    </React.StrictMode>
  </ReactKeycloakProvider>
);

reportWebVitals();
