import BalanceCard from "./DashboardComponents/BalanceCard";
import MonthlyAverageCard from "./DashboardComponents/MonthlyAverageCard";
import TopCategoryCard from "./DashboardComponents/TopCategoryCard";
import DailySpendingCard from "./DashboardComponents/DailySpendingCard";
import CategorySpendingCard from "./DashboardComponents/CategorySpendingCard";
import { Col, Row } from "antd";

import "../styles/DashboardContent.css";

const DashboardContent = () => {
  return (
    <>
      <Row
        gutter={16}
        style={{
          margin: 0,
          marginBottom: 10,
          maxWidth: "100vw",
          overflowX: "hidden",
        }}
      >
        <Col span={10}>
          <TopCategoryCard />
        </Col>
        <Col span={10}>
          <BalanceCard />
        </Col>
        <Col span={4}>
          <MonthlyAverageCard />
        </Col>
      </Row>
      <Row
        gutter={16}
        style={{
          margin: 0,
          marginBottom: 10,
          maxWidth: "100vw",
          overflowX: "hidden",
        }}
      >
        <Col span={24}>
          <DailySpendingCard />
        </Col>
      </Row>
      <Row
        gutter={16}
        style={{
          margin: 0,
          marginBottom: 10,
          maxWidth: "100vw",
          overflowX: "hidden",
        }}
      >
        <Col span={24}>
          <CategorySpendingCard />
        </Col>
      </Row>
    </>
  );
};

export default DashboardContent;
