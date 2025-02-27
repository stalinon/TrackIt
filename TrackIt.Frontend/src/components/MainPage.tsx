import logo from "../original.png";
import { Button } from "antd";
import "../styles/MainPage.css";

function MainPage() {
  return (
    <div className="main_page">
      <header className="main_page__header">
        <img src={logo} />
        <div className="main_page__header__buttons">
          <Button type="primary" className="main_page__header__button sign-up">
            Sign Up
          </Button>
          <Button className="main_page__header__button log-in">Log In</Button>
        </div>
      </header>
      <div className="main_page__body">
        <div className="main_page__body__text">
          <p>Take control of your finances with ease!</p>
          <p>
            Track your income, expenses, and set smart limits – all in one
            simple app.
          </p>
          <p>
            Get personalized insights, plan ahead, and never miss a payment with
            instant reminders.
          </p>
          <p>Start managing your budget effortlessly today!</p>
        </div>
        <div className="main_page__body__photo"></div>
      </div>
    </div>
  );
}

export default MainPage;
