import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import "./styles/custom-theme.less";
import MainPage from "./components/MainPage";
import { Provider } from "react-redux";
import store from "./redux/store";
import reportWebVitals from "./reportWebVitals";

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <Provider store={store}>
      <MainPage />
    </Provider>
  </React.StrictMode>
);

reportWebVitals();
