import BalanceCard from "./DashboardComponents/BalanceCard";
import MonthlyAverageCard from "./DashboardComponents/MonthlyAverageCard";
import TopCategoryCard from "./DashboardComponents/TopCategoryCard";
import DailySpendingCard from "./DashboardComponents/DailySpendingCard";
import CategorySpendingCard from "./DashboardComponents/CategorySpendingCard";
import { Flex } from "antd";

import "../styles/DashboardContent.css";

const DashboardContent = () => {
  return (
    <Flex gap="large" wrap justify="space-around">
      <TopCategoryCard />
      <BalanceCard />
      <MonthlyAverageCard />
      <DailySpendingCard />
      <CategorySpendingCard />
    </Flex>
  );
};

export default DashboardContent;
