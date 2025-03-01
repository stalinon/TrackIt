import React, { useEffect, useState } from "react";
import { AnalyticsApi, TopCategoryDto } from "../../api/generated";
import api from "../../api/api";
import { Card, Col, Row, Statistic, Typography } from "antd";
import CountUp from "react-countup";

const { Title } = Typography;

const analyticsApi = new AnalyticsApi(undefined, api.defaults.baseURL, api);

const formatter = (value: number) => <CountUp end={value} separator="," />;

const TopCategoryCard = () => {
  const [categories, setCategories] = useState<TopCategoryDto[]>([]);

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const response = await analyticsApi.apiAnalyticsTopCategoriesGet();
        console.log("Топ категории:", response.data);
        setCategories(response.data || []);
      } catch (error) {
        console.error("Ошибка получения топовых категорий", error);
      }
    };

    fetchCategories();
  }, []);

  return (
    <Card variant="borderless">
      <Title
        level={5}
        style={{ textAlign: "center", marginBottom: 8, marginTop: 2 }}
      >
        Top categories by spend
      </Title>
      <Row gutter={42} justify="center">
        {categories.length > 0 ? (
          categories.map((category, i) => (
            <Col key={i}>
              <Statistic
                title={category.category}
                value={category.total_spent}
                precision={2}
                formatter={formatter}
              />
            </Col>
          ))
        ) : (
          <p>Нет данных</p>
        )}
      </Row>
    </Card>
  );
};

export default TopCategoryCard;
