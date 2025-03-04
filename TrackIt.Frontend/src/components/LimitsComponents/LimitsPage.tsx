import React, { useCallback, useEffect, useState } from "react";
import { Table, Button, Modal, Form, InputNumber, Select, message } from "antd";
import { PlusOutlined, EditOutlined, DeleteOutlined } from "@ant-design/icons";
import {
  BudgetApi,
  BudgetDto,
  CategoriesApi,
  CategoryDto,
} from "../../api/generated";
import api from "../../api/api";

const budgetApi = new BudgetApi(undefined, api.defaults.baseURL, api);
const categoryApi = new CategoriesApi(undefined, api.defaults.baseURL, api);

const LimitsPage: React.FC = () => {
  const [limits, setLimits] = useState<BudgetDto[]>([]);
  const [categories, setCategories] = useState<CategoryDto[]>([]);
  const [loading, setLoading] = useState(false);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [editingLimit, setEditingLimit] = useState<BudgetDto | null>(null);

  // Пагинация
  const [currentPage, setCurrentPage] = useState(1);
  const [totalCount, setTotalCount] = useState(0);

  const [form] = Form.useForm();

  const fetchLimits = useCallback(async () => {
    setLoading(true);
    try {
      const response = await budgetApi.apiBudgetsGet(null, currentPage, 10);
      setLimits(response.data.items);
      setTotalCount(response.data.total);
    } catch (error) {
      message.error("Error while fetching limits");
    }
    setLoading(false);
  }, [currentPage]);

  const fetchCategories = useCallback(async () => {
    try {
      let allCategories: CategoryDto[] = [];
      let currentPage = 1;
      let pageSize = 100;
      let total = 0;

      do {
        const response = await categoryApi.apiCategoriesGet(
          currentPage,
          pageSize
        );
        allCategories = [...allCategories, ...response.data.items];
        total = response.data.total;
        currentPage++;
      } while (allCategories.length < total);

      setCategories(allCategories);
    } catch (error) {
      message.error("Error while fetching categories");
    }
  }, []);

  const showModal = async (limit?: BudgetDto) => {
    setEditingLimit(limit || null);
    setIsModalVisible(true);

    if (limit) {
      try {
        const response = await budgetApi.apiBudgetsIdGet(limit.id);
        form.setFieldsValue({
          amount: response.data.amount,
          category_id: response.data.category_id,
        });
      } catch (error) {
        message.error("Error while fetching limit");
      }
    } else {
      form.resetFields();
    }
  };

  const handleCancel = () => {
    setIsModalVisible(false);
    setEditingLimit(null);
    form.resetFields();
  };

  const handleOk = async () => {
    try {
      const values = await form.validateFields();

      if (editingLimit) {
        await budgetApi.apiBudgetsIdPut(editingLimit.id, values);
        message.success("Limit updated!");
      } else {
        await budgetApi.apiBudgetsPost(values);
        message.success("Limit added!");
      }

      fetchLimits();
      handleCancel();
    } catch (error) {
      message.error("Error while saving limit");
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await budgetApi.apiBudgetsIdDelete(id);
      message.success("Limit removed!");
      fetchLimits();
    } catch (error) {
      message.error("Error while removing limit");
    }
  };

  useEffect(() => {
    fetchLimits();
    fetchCategories();
  }, [currentPage, fetchLimits, fetchCategories]);

  return (
    <div>
      <Button
        type="primary"
        icon={<PlusOutlined />}
        onClick={() => showModal()}
        style={{ marginBottom: 16 }}
      >
        Add limit
      </Button>

      <Table
        dataSource={limits}
        rowKey="id"
        loading={loading}
        pagination={{
          current: currentPage,
          pageSize: 10,
          showSizeChanger: false,
          total: totalCount,
          onChange: (page) => {
            setCurrentPage(page);
          },
        }}
        columns={[
          {
            title: "Amount",
            dataIndex: "amount",
            sorter: (a, b) => a.amount - b.amount,
          },
          {
            title: "Category",
            dataIndex: "category_id",
            render: (categoryId) =>
              categories.find((cat) => cat.id === categoryId)?.name ||
              "Unknown",
          },
          {
            title: "Actions",
            render: (_, record) => (
              <>
                <Button
                  icon={<EditOutlined />}
                  onClick={() => showModal(record)}
                  style={{ marginRight: 8 }}
                />
                <Button
                  icon={<DeleteOutlined />}
                  danger
                  onClick={() => handleDelete(record.id)}
                />
              </>
            ),
          },
        ]}
      />

      <Modal
        title={editingLimit ? "Edit" : "Add"}
        open={isModalVisible}
        onOk={handleOk}
        onCancel={handleCancel}
        okText="Save"
        cancelText="Cancel"
      >
        <Form form={form} layout="vertical">
          <Form.Item
            name="amount"
            label="Amount"
            rules={[{ required: true, message: "Enter the amount" }]}
          >
            <InputNumber style={{ width: "100%" }} />
          </Form.Item>

          <Form.Item
            name="category_id"
            label="Category"
            rules={[{ required: true, message: "Choose category" }]}
          >
            <Select>
              {categories.map((category) => (
                <Select.Option key={category.id} value={category.id}>
                  {category.name}
                </Select.Option>
              ))}
            </Select>
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default LimitsPage;
