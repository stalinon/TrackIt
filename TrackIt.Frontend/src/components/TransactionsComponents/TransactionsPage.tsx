import React, { useCallback, useEffect, useState } from "react";
import {
  Table,
  Button,
  Modal,
  Form,
  Input,
  InputNumber,
  DatePicker,
  Select,
  message,
} from "antd";
import { PlusOutlined, EditOutlined, DeleteOutlined } from "@ant-design/icons";
import {
  TransactionApi,
  TransactionDto,
  CategoriesApi,
  CategoryDto,
} from "../../api/generated";
import api from "../../api/api";
import dayjs from "dayjs";

const transactionApi = new TransactionApi(undefined, api.defaults.baseURL, api);
const categoryApi = new CategoriesApi(undefined, api.defaults.baseURL, api);

const TransactionsPage: React.FC = () => {
  const [transactions, setTransactions] = useState<TransactionDto[]>([]);
  const [categories, setCategories] = useState<CategoryDto[]>([]);
  const [loading, setLoading] = useState(false);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [editingTransaction, setEditingTransaction] =
    useState<TransactionDto | null>(null);

  // Фильтры
  const [selectedCategory, setSelectedCategory] = useState<string | undefined>(
    undefined
  );

  // Пагинация
  const [currentPage, setCurrentPage] = useState(1);
  const [totalCount, setTotalCount] = useState(0);

  const [form] = Form.useForm();

  const fetchTransactions = useCallback(async () => {
    setLoading(true);
    try {
      const response = await transactionApi.apiTransactionsGet(
        selectedCategory,
        currentPage,
        10
      );
      setTransactions(response.data.items);
      setTotalCount(response.data.total);
    } catch (error) {
      message.error("Error while fetching transaction");
    }
    setLoading(false);
  }, [selectedCategory, currentPage]);

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

  const showModal = async (transaction?: TransactionDto) => {
    setEditingTransaction(transaction || null);
    setIsModalVisible(true);

    if (transaction) {
      try {
        const response = await transactionApi.apiTransactionsIdGet(
          transaction.id
        );
        const detailedTransaction = response.data;

        const categoryResponse = await categoryApi.apiCategoriesIdGet(
          detailedTransaction.category_id
        );
        const category = categoryResponse.data;

        form.setFieldsValue({
          amount: detailedTransaction.amount,
          description: detailedTransaction.description,
          category_id: category.id,
          date: dayjs(detailedTransaction.date),
        });
      } catch (error) {
        message.error("Error while fetching transaction");
      }
    } else {
      form.resetFields();
    }
  };

  const handleCancel = () => {
    setIsModalVisible(false);
    setEditingTransaction(null);
    form.resetFields();
  };

  const handleOk = async () => {
    try {
      const values = await form.validateFields();
      const formattedValues = { ...values, date: values.date.toISOString() };

      if (editingTransaction) {
        await transactionApi.apiTransactionsIdPut(
          editingTransaction.id,
          formattedValues
        );
        message.success("Transaction updated!");
      } else {
        await transactionApi.apiTransactionsPost(formattedValues);
        message.success("Transaction added!");
      }

      fetchTransactions();
      handleCancel();
    } catch (error) {
      message.error("Error while saving transaction");
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await transactionApi.apiTransactionsIdDelete(id);
      message.success("Transaction removed!");
      fetchTransactions();
    } catch (error) {
      message.error("Error while removing transaction");
    }
  };

  useEffect(() => {
    fetchTransactions();
    fetchCategories();
  }, [selectedCategory, currentPage, fetchCategories, fetchTransactions]);

  return (
    <div>
      {/* Панель фильтров */}
      <div
        style={{
          marginBottom: 16,
          display: "flex",
          justifyContent: "space-between",
        }}
      >
        <div style={{ display: "flex", gap: 10 }}>
          <Select
            style={{ width: 200 }}
            placeholder="Category filter"
            allowClear
            value={selectedCategory}
            onChange={(value) => {
              setSelectedCategory(value);
              setCurrentPage(1);
            }}
          >
            {categories.map((category) => (
              <Select.Option key={category.id} value={category.id}>
                {category.name}
              </Select.Option>
            ))}
          </Select>
        </div>

        {/* Кнопка добавления транзакции */}
        <Button
          type="primary"
          icon={<PlusOutlined />}
          onClick={() => showModal()}
          style={{ marginBottom: 16 }}
        >
          Add transaction
        </Button>
      </div>

      {/* Таблица транзакций */}
      <Table
        dataSource={transactions}
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
            title: "Date",
            dataIndex: "date",
            render: (text) => dayjs(text).format("YYYY-MM-DD"),
            sorter: (a, b) => dayjs(a.date).unix() - dayjs(b.date).unix(),
          },
          {
            title: "Amount",
            dataIndex: "amount",
            sorter: (a, b) => a.amount - b.amount,
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

      {/* Модальное окно для создания/редактирования транзакции */}
      <Modal
        title={editingTransaction ? "Edit" : "Add"}
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

          <Form.Item name="description" label="Description">
            <Input />
          </Form.Item>

          <Form.Item
            name="date"
            label="Date"
            rules={[{ required: true, message: "Choose date" }]}
          >
            <DatePicker style={{ width: "100%" }} />
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

export default TransactionsPage;
