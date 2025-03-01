import React, { useEffect, useState } from "react";
import { AnalyticsApi, BalanceDto } from "../../api/generated";
import api from "../../api/api";
import type { StatisticProps } from "antd";
import { Card, Col, Row, Statistic, Typography } from "antd";
import { ArrowDownOutlined, ArrowUpOutlined } from "@ant-design/icons";
import CountUp from "react-countup";

const { Title } = Typography;

const analyticsApi = new AnalyticsApi(undefined, api.defaults.baseURL, api);

const formatter: StatisticProps["formatter"] = (value) => (
  <CountUp end={value as number} separator="," />
);

const BalanceCard = () => {
  const [balance, setBalance] = useState<BalanceDto | null>(null);

  useEffect(() => {
    const fetchBalance = async () => {
      try {
        const response = await analyticsApi.apiAnalyticsBalanceGet();
        console.log("Данные баланса:", response.data);
        setBalance(response.data);
      } catch (error) {
        console.error("Ошибка получения баланса", error);
      }
    };

    fetchBalance();
  }, []);

  return (
    <Card variant="borderless">
      <Title
        level={5}
        style={{ textAlign: "center", marginBottom: 8, marginTop: 2 }}
      >
        Current balance
      </Title>
      <Row gutter={42} justify="center">
        <Col>
          <Statistic
            title="Total income"
            value={balance?.total_income ?? 0}
            formatter={formatter}
            valueStyle={{ color: "#3f8600" }}
            prefix={<ArrowUpOutlined />}
          />
        </Col>
        <Col>
          <Statistic
            title="Total expense"
            value={balance?.total_expense ?? 0}
            precision={2}
            formatter={formatter}
            valueStyle={{ color: "#cf1322" }}
            prefix={<ArrowDownOutlined />}
          />
        </Col>
        <Col>
          <Statistic
            title="Balance"
            value={balance?.balance ?? 0}
            precision={2}
            formatter={formatter}
          />
        </Col>
      </Row>
    </Card>
  );
};

export default BalanceCard;
