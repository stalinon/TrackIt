import React, { useEffect, useState, useCallback } from "react";
import {
  Table,
  Button,
  Modal,
  Form,
  InputNumber,
  DatePicker,
  Select,
  message,
} from "antd";
import { PlusOutlined, EditOutlined, DeleteOutlined } from "@ant-design/icons";
import {
  PlannedPaymentApi,
  PlannedPaymentDto,
  CategoriesApi,
  CategoryDto,
} from "../../api/generated";
import api from "../../api/api";
import dayjs from "dayjs";

const plannedPaymentApi = new PlannedPaymentApi(
  undefined,
  api.defaults.baseURL,
  api
);
const categoryApi = new CategoriesApi(undefined, api.defaults.baseURL, api);

const PlannedPaymentsPage: React.FC = () => {
  const [plannedPayments, setPlannedPayments] = useState<PlannedPaymentDto[]>(
    []
  );
  const [categories, setCategories] = useState<CategoryDto[]>([]);
  const [loading, setLoading] = useState(false);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [editingPayment, setEditingPayment] =
    useState<PlannedPaymentDto | null>(null);

  const [selectedCategory, setSelectedCategory] = useState<string | undefined>(
    undefined
  );
  const [currentPage, setCurrentPage] = useState(1);
  const [totalCount, setTotalCount] = useState(0);

  const [form] = Form.useForm();

  const fetchPlannedPayments = useCallback(async () => {
    setLoading(true);
    try {
      const response = await plannedPaymentApi.apiPaymentsGet(
        selectedCategory,
        currentPage,
        10
      );
      setPlannedPayments(response.data.items);
      setTotalCount(response.data.total);
    } catch (error) {
      message.error("Error while fetching planned payments");
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

  const showModal = async (payment?: PlannedPaymentDto) => {
    setEditingPayment(payment || null);
    setIsModalVisible(true);

    if (payment) {
      try {
        const response = await plannedPaymentApi.apiPaymentsIdGet(payment.id);
        const detailedPayment = response.data;
        form.setFieldsValue({
          amount: detailedPayment.amount,
          description: detailedPayment.description,
          category_id: detailedPayment.category_id,
          due_date: dayjs(detailedPayment.due_date),
        });
      } catch (error) {
        message.error("Error while fetching payments");
      }
    } else {
      form.resetFields();
    }
  };

  const handleCancel = () => {
    setIsModalVisible(false);
    setEditingPayment(null);
    form.resetFields();
  };

  const handleOk = async () => {
    try {
      const values = await form.validateFields();
      const formattedValues = {
        ...values,
        due_date: values.due_date.toISOString(),
      };

      if (editingPayment) {
        await plannedPaymentApi.apiPaymentsIdPut(
          editingPayment.id,
          formattedValues
        );
        message.success("Planned payment updated!");
      } else {
        await plannedPaymentApi.apiPaymentsPost(formattedValues);
        message.success("Planned payment created!");
      }

      fetchPlannedPayments();
      handleCancel();
    } catch (error) {
      message.error("Error while saving payment");
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await plannedPaymentApi.apiPaymentsIdDelete(id);
      message.success("Planned payment removed!");
      fetchPlannedPayments();
    } catch (error) {
      message.error("Error while removing payment");
    }
  };

  useEffect(() => {
    fetchPlannedPayments();
    fetchCategories();
  }, [selectedCategory, currentPage, fetchPlannedPayments, fetchCategories]);

  return (
    <div>
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

        <Button
          type="primary"
          icon={<PlusOutlined />}
          onClick={() => showModal()}
        >
          Добавить платеж
        </Button>
      </div>

      <Table
        dataSource={plannedPayments}
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
            title: "Due date",
            dataIndex: "due_date",
            render: (text) => dayjs(text).format("YYYY-MM-DD"),
            sorter: (a, b) =>
              dayjs(a.due_date).unix() - dayjs(b.due_date).unix(),
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
      <Modal
        title={editingPayment ? "Edit" : "Add"}
        visible={isModalVisible}
        onOk={handleOk}
        onCancel={handleCancel}
      >
        <Form form={form} layout="vertical">
          <Form.Item
            name="amount"
            label="Сумма"
            rules={[{ required: true, message: "Enter the amount" }]}
          >
            <InputNumber style={{ width: "100%" }} />
          </Form.Item>
          <Form.Item
            name="due_date"
            label="Due date"
            rules={[{ required: true, message: "Choose a date" }]}
          >
            <DatePicker style={{ width: "100%" }} />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default PlannedPaymentsPage;
