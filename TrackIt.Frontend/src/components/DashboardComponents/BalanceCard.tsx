import React, { useEffect, useState } from "react";
import { AnalyticsApi, BalanceDto } from "../../api/generated";
import api from "../../api/api";
import type { StatisticProps } from "antd";
import { Card, Statistic, Typography, Avatar, Flex } from "antd";
import {
  ArrowDownOutlined,
  ArrowUpOutlined,
  CreditCardOutlined,
} from "@ant-design/icons";
import CountUp from "react-countup";

const { Title } = Typography;

const analyticsApi = new AnalyticsApi(undefined, api.defaults.baseURL, api);

const formatter: StatisticProps["formatter"] = (value) => (
  <CountUp end={value as number} separator="." />
);

const BalanceCard = () => {
  const [balance, setBalance] = useState<BalanceDto | null>(null);

  useEffect(() => {
    const fetchBalance = async () => {
      try {
        const response = await analyticsApi.apiAnalyticsBalanceGet();
        setBalance(response.data);
      } catch (error) {
        console.error("Error while fetching balance", error);
      }
    };

    fetchBalance();
  }, []);

  return (
    <Card variant="borderless">
      <Flex gap="large" align="center">
        <Avatar
          size={60}
          icon={<CreditCardOutlined />}
          style={{
            backgroundColor: "#f5f5f5",
            borderRadius: "10px",
            padding: "10px",
            color: "black",
          }}
        />
        <Flex vertical>
          <Title level={5} style={{ marginBottom: 8, marginTop: 2 }}>
            Current balance
          </Title>
          <Flex gap="large">
            <Statistic
              title="Total income"
              value={balance?.total_income ?? 0}
              precision={2}
              formatter={formatter}
              valueStyle={{ color: "#3f8600" }}
              prefix={<ArrowUpOutlined />}
            />
            <Statistic
              title="Total expense"
              value={balance?.total_expense ?? 0}
              precision={2}
              formatter={formatter}
              valueStyle={{ color: "#cf1322" }}
              prefix={<ArrowDownOutlined />}
            />
            <Statistic
              title="Balance"
              value={balance?.balance ?? 0}
              precision={2}
              formatter={formatter}
            />
          </Flex>
        </Flex>
      </Flex>
    </Card>
  );
};

export default BalanceCard;
