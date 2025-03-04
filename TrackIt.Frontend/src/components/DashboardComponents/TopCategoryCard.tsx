import React, { useEffect, useState } from "react";
import { AnalyticsApi, TopCategoryDto } from "../../api/generated";
import api from "../../api/api";
import { Card, Flex, Statistic, Typography, Avatar } from "antd";
import CountUp from "react-countup";
import { TrophyOutlined } from "@ant-design/icons";

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
      <Flex gap="large" align="center">
        <Avatar
          size={60}
          icon={<TrophyOutlined />}
          style={{
            backgroundColor: "#f5f5f5",
            borderRadius: "10px",
            padding: "10px",
            color: "black",
          }}
        />
        <Flex vertical>
          <Title level={5} style={{ marginBottom: 14, marginTop: 4 }}>
            Top categories by spend
          </Title>
          <Flex gap="large">
            {categories.length > 0 ? (
              categories.map((category, i) => (
                <Statistic
                  key={i}
                  title={category.category}
                  value={category.total_spent}
                  precision={2}
                  formatter={formatter}
                />
              ))
            ) : (
              <p>Нет данных</p>
            )}
          </Flex>
        </Flex>
      </Flex>
    </Card>
  );
};

export default TopCategoryCard;
