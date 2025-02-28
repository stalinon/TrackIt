import Keycloak from "keycloak-js";

// Оставляем объект конфигурации
const keycloakConfig = {
  url: "http://localhost:8180",
  realm: "TrackIt",
  clientId: "trackit-api"
};

// Создаем один экземпляр (Singleton)
const keycloak = new Keycloak(keycloakConfig);

export default keycloak;
