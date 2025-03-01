import React, { useEffect, useState } from "react";
import { AnalyticsApi, DailySpendingDto } from "../../api/generated";
import api from "../../api/api";
import { Card, Typography, Col } from "antd";
import {
  LineChart,
  Line,
  XAxis,
  YAxis,
  Tooltip,
  ResponsiveContainer,
  CartesianGrid,
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
    <Card variant="borderless" style={{ width: "40%" }}>
      <Col span={24}>
        <Title level={5} style={{ textAlign: "center", marginBottom: 8 }}>
          Daily Spendings (Last Month)
        </Title>
        <ResponsiveContainer width="100%" height={300}>
          <LineChart data={spendings}>
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="day" tick={{ fontSize: 12 }} />
            <YAxis tick={{ fontSize: 12 }} />
            <Tooltip />
            <Line
              type="monotone"
              dataKey="total_spent"
              stroke="#1677ff"
              strokeWidth={2}
              dot={{ r: 4 }}
            />
          </LineChart>
        </ResponsiveContainer>
      </Col>
    </Card>
  );
};

export default DailySpendingCard;
