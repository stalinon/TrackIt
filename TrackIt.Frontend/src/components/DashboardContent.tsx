import BalanceCard from "./DashboardComponents/BalanceCard";
import MonthlyAverageCard from "./DashboardComponents/MonthlyAverageCard";
import TopCategoryCard from "./DashboardComponents/TopCategoryCard";
import DailySpendingCard from "./DashboardComponents/DailySpendingCard";
import CategorySpendingCard from "./DashboardComponents/CategorySpendingCard";
import { Flex } from "antd";
import { motion } from "framer-motion";

const cardVariants = {
  hidden: { opacity: 0, y: 20 },
  visible: { opacity: 1, y: 0, transition: { duration: 0.5 } },
};

const DashboardContent = () => {
  return (
    <Flex
      gap="large"
      wrap
      justify="space-around"
      style={{
        alignItems: "stretch",
        padding: "20px",
        backgroundColor: "#f0f0f0",
      }}
    >
      {[
        { component: TopCategoryCard, type: "small" },
        { component: BalanceCard, type: "small" },
        { component: MonthlyAverageCard, type: "small" },
        { component: DailySpendingCard, type: "wide" },
        { component: CategorySpendingCard, type: "wide" },
      ].map(({ component: Component, type }, index) => (
        <motion.div
          key={index}
          initial="hidden"
          animate="visible"
          variants={cardVariants}
          whileHover={{ scale: 1.01 }}
          style={{
            flex: type === "wide" ? "1 1 600px" : "1 1 300px",
            minHeight: "150px",
          }}
        >
          <Component />
        </motion.div>
      ))}
    </Flex>
  );
};

export default DashboardContent;
