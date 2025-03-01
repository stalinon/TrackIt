import axios from "axios";
import apiConfig from "../configs/apiConfig";
import keycloak from "../keycloak"; // Импортируем Keycloak

const api = axios.create({
  baseURL: apiConfig.baseURL,
  timeout: apiConfig.timeout,
});

api.interceptors.request.use(async (config) => {
  if (keycloak.authenticated) {
    await keycloak.updateToken(30); // Обновляем токен, если истекает через 30 сек
    config.headers.Authorization = `Bearer ${keycloak.token}`;
  }
  return config;
});

export default api;
