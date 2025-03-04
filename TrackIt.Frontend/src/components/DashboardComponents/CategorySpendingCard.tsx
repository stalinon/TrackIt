import React, { useEffect, useState } from "react";
import { AnalyticsApi, CategorySpendingDto } from "../../api/generated";
import api from "../../api/api";
import { Card, Typography, Col } from "antd";
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

const CategorySpendingCard = () => {
  const [spendings, setSpendings] = useState<CategorySpendingDto[]>([]);

  useEffect(() => {
    const fetchSpendings = async () => {
      try {
        const response = await analyticsApi.apiAnalyticsCategorySpendingGet();
        setSpendings(response.data || []);
      } catch (error) {
        console.error("Error while fetching spendings", error);
      }
    };

    fetchSpendings();
  }, []);

  return (
    <Card variant="borderless">
      <Col span={24}>
        <Title level={5} style={{ textAlign: "center", marginBottom: 8 }}>
          Category Spendings (Last Month)
        </Title>
        <ResponsiveContainer width="100%" height={300}>
          <BarChart data={spendings}>
            <XAxis dataKey="category" tick={{ fontSize: 12 }} />
            <YAxis tick={{ fontSize: 12 }} />
            <Tooltip />
            <Bar dataKey="total_spent" fill="#1677ff" barSize={30} />
          </BarChart>
        </ResponsiveContainer>
      </Col>
    </Card>
  );
};

export default CategorySpendingCard;
