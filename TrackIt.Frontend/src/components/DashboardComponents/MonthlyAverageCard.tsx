import React, { useEffect, useState } from "react";
import { AnalyticsApi, MonthlyAverageDto } from "../../api/generated";
import api from "../../api/api";
import type { StatisticProps } from "antd";
import { Card, Col, Row, Statistic } from "antd";
import CountUp from "react-countup";

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
    <Card variant="borderless">
      <Row
        gutter={42}
        justify="center"
        style={{ marginTop: 28, marginBottom: 20 }}
      >
        <Col>
          <Statistic
            title="Average monthly spending"
            value={average?.average_month_spent ?? 0}
            precision={2}
            formatter={formatter}
          />
        </Col>
      </Row>
    </Card>
  );
};

export default MonthlyAverageCard;
