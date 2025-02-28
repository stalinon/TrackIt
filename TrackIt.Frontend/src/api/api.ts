import axios from "axios";
import apiConfig from "../configs/apiConfig";

const api = axios.create({
  baseURL: apiConfig.baseURL,
  timeout: apiConfig.timeout,
});

export default api;
