import axios from "axios";

const instance = axios.create({
  baseURL: "http://localhost:5132", // Укажите URL вашего бэкенда
  timeout: 10000,
});

export default instance;
