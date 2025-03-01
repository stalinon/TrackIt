import React, { useEffect, useState } from "react";
import { AnalyticsApi, DailySpendingDto } from "../../api/generated";
import api from "../../api/api";
import { Card, Typography } from "antd";
import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  Tooltip,
  ResponsiveContainer,
} from "recharts";

const { Title } = Typography;

const analyticsApi = new AnalyticsApi(undefined, api.defaults.baseURL, api);

const DailySpendingCard = () => {
  const [spendings, setSpendings] = useState<DailySpendingDto[]>([]);

  useEffect(() => {
    const fetchSpendings = async () => {
      try {
        const response = await analyticsApi.apiAnalyticsMonthlyTrendGet();
        console.log("Дневные траты:", response.data);
        setSpendings(response.data || []);
      } catch (error) {
        console.error("Ошибка получения трат", error);
      }
    };

    fetchSpendings();
  }, []);

  return (
    <Card variant="borderless">
      <Title level={5} style={{ textAlign: "center", marginBottom: 8 }}>
        Daily Spendings (Last Month)
      </Title>
      <ResponsiveContainer width="100%" height={300}>
        <BarChart data={spendings}>
          <XAxis dataKey="day" tick={{ fontSize: 12 }} />
          <YAxis tick={{ fontSize: 12 }} />
          <Tooltip />
          <Bar dataKey="total_spent" fill="#3f8600" barSize={30} />
        </BarChart>
      </ResponsiveContainer>
    </Card>
  );
};

export default DailySpendingCard;
