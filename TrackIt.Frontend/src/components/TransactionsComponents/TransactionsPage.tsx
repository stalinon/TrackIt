import React, { useEffect, useState } from "react";
import { Table, Button, Modal, Form, Input, InputNumber, DatePicker, Select, message } from "antd";
import { PlusOutlined, EditOutlined, DeleteOutlined, FilterOutlined } from "@ant-design/icons";
import { TransactionApi, TransactionDto, CategoriesApi, CategoryDto } from "../../api/generated";
import api from "../../api/api";
import dayjs from "dayjs";

const transactionApi = new TransactionApi(undefined, api.defaults.baseURL, api);
const categoryApi = new CategoriesApi(undefined, api.defaults.baseURL, api);

const TransactionsPage: React.FC = () => {
  const [transactions, setTransactions] = useState<TransactionDto[]>([]);
  const [categories, setCategories] = useState<CategoryDto[]>([]);
  const [loading, setLoading] = useState(false);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [editingTransaction, setEditingTransaction] = useState<TransactionDto | null>(null);

  // Фильтры
  const [selectedCategory, setSelectedCategory] = useState<string | undefined>(undefined);
  
  // Пагинация
  const [currentPage, setCurrentPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalCount, setTotalCount] = useState(0);

  const [form] = Form.useForm();

  useEffect(() => {
    fetchTransactions();
    fetchCategories();
  }, [selectedCategory, currentPage, pageSize]);

  const fetchTransactions = async () => {
    setLoading(true);
    try {
      const response = await transactionApi.apiTransactionsGet(selectedCategory, currentPage, pageSize);
      setTransactions(response.data.items);
      setTotalCount(response.data.total);
    } catch (error) {
      message.error("Ошибка при загрузке транзакций");
    }
    setLoading(false);
  };

  const fetchCategories = async () => {
    try {
      const response = await categoryApi.apiCategoriesGet();
      setCategories(response.data);
    } catch (error) {
      message.error("Ошибка при загрузке категорий");
    }
  };

  const showModal = (transaction?: TransactionDto) => {
    setEditingTransaction(transaction || null);
    setIsModalVisible(true);
    if (transaction) {
      form.setFieldsValue({
        ...transaction,
        date: dayjs(transaction.date),
      });
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
        await transactionApi.apiTransactionsIdPut(editingTransaction.id, formattedValues);
        message.success("Транзакция обновлена!");
      } else {
        await transactionApi.apiTransactionsPost(formattedValues);
        message.success("Транзакция создана!");
      }

      fetchTransactions();
      handleCancel();
    } catch (error) {
      message.error("Ошибка при сохранении транзакции");
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await transactionApi.apiTransactionsIdDelete(id);
      message.success("Транзакция удалена!");
      fetchTransactions();
    } catch (error) {
      message.error("Ошибка при удалении транзакции");
    }
  };

  return (
    <div>
      {/* Панель фильтров */}
      <div style={{ marginBottom: 16, display: "flex", gap: 10 }}>
        <Select
          style={{ width: 200 }}
          placeholder="Фильтр по категории"
          allowClear
          value={selectedCategory}
          onChange={(value) => {
            setSelectedCategory(value);
            setCurrentPage(1);
          }}
        >
          {categories.map(category => (
            <Select.Option key={category.id} value={category.id}>
              {category.name}
            </Select.Option>
          ))}
        </Select>

        <Button type="primary" icon={<FilterOutlined />} onClick={fetchTransactions}>
          Применить фильтры
        </Button>
      </div>

      {/* Кнопка добавления транзакции */}
      <Button type="primary" icon={<PlusOutlined />} onClick={() => showModal()} style={{ marginBottom: 16 }}>
        Добавить транзакцию
      </Button>

      {/* Таблица транзакций */}
      <Table
        dataSource={transactions}
        rowKey="id"
        loading={loading}
        pagination={{
          current: currentPage,
          pageSize: pageSize,
          total: totalCount,
          onChange: (page, pageSize) => {
            setCurrentPage(page);
            setPageSize(pageSize);
          },
        }}
        columns={[
          {
            title: "Дата",
            dataIndex: "date",
            render: (text) => dayjs(text).format("YYYY-MM-DD"),
            sorter: (a, b) => dayjs(a.date).unix() - dayjs(b.date).unix(),
          },
          {
            title: "Категория",
            dataIndex: "category_id",
            render: (categoryId) => categories.find(cat => cat.id === categoryId)?.name || "—",
          },
          {
            title: "Сумма",
            dataIndex: "amount",
            sorter: (a, b) => a.amount - b.amount,
          },
          {
            title: "Описание",
            dataIndex: "description",
          },
          {
            title: "Действия",
            render: (_, record) => (
              <>
                <Button icon={<EditOutlined />} onClick={() => showModal(record)} style={{ marginRight: 8 }} />
                <Button icon={<DeleteOutlined />} danger onClick={() => handleDelete(record.id)} />
              </>
            ),
          },
        ]}
      />

      {/* Модальное окно для создания/редактирования транзакции */}
      <Modal
        title={editingTransaction ? "Редактировать транзакцию" : "Добавить транзакцию"}
        open={isModalVisible}
        onOk={handleOk}
        onCancel={handleCancel}
        okText="Сохранить"
        cancelText="Отмена"
      >
        <Form form={form} layout="vertical">
          <Form.Item name="amount" label="Сумма" rules={[{ required: true, message: "Введите сумму" }]}>
            <InputNumber style={{ width: "100%" }} />
          </Form.Item>

          <Form.Item name="description" label="Описание">
            <Input />
          </Form.Item>

          <Form.Item name="date" label="Дата" rules={[{ required: true, message: "Выберите дату" }]}>
            <DatePicker style={{ width: "100%" }} />
          </Form.Item>

          <Form.Item name="category_id" label="Категория" rules={[{ required: true, message: "Выберите категорию" }]}>
            <Select>
              {categories.map(category => (
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
