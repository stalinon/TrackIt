import React, { useEffect, useState } from "react";
import { AnalyticsApi, MonthlyAverageDto } from "../../api/generated";
import api from "../../api/api";
import type { StatisticProps } from "antd";
import { Card, Flex, Typography, Avatar, Statistic } from "antd";
import CountUp from "react-countup";
import { FallOutlined } from "@ant-design/icons";

const { Title } = Typography;

const analyticsApi = new AnalyticsApi(undefined, api.defaults.baseURL, api);

const formatter: StatisticProps["formatter"] = (value) => (
  <CountUp end={value as number} separator="," />
);

const MonthlyAverageCard = () => {
  const [average, setAverage] = useState<MonthlyAverageDto | null>(null);

  useEffect(() => {
    const fetchMonthlyAverage = async () => {
      try {
        const response = await analyticsApi.apiAnalyticsMonthlyAverageGet();
        console.log("Данные средних месячных:", response.data);
        setAverage(response.data);
      } catch (error) {
        console.error("Ошибка средних месячных", error);
      }
    };

    fetchMonthlyAverage();
  }, []);

  return (
    <Card variant="borderless" style={{ height: "10%" }}>
      <Flex gap="large" align="center">
        <Avatar
          size={60}
          icon={<FallOutlined />}
          style={{
            backgroundColor: "#f5f5f5",
            borderRadius: "10px",
            padding: "10px",
            color: "black",
          }}
        />
        <Flex vertical>
          <Title level={5} style={{ marginBottom: 8, marginTop: 2 }}>
            Average monthly spending
          </Title>
          <Statistic
            title="Money"
            value={average?.average_month_spent ?? 0}
            precision={2}
            formatter={formatter}
          />
        </Flex>
      </Flex>
    </Card>
  );
};

export default MonthlyAverageCard;
