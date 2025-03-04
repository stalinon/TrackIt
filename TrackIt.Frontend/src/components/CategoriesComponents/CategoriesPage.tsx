import React, { useEffect, useState } from "react";
import { Table, Button, Modal, Form, Input, message } from "antd";
import { PlusOutlined, EditOutlined, DeleteOutlined } from "@ant-design/icons";
import { CategoriesApi, CategoryDto } from "../../api/generated";
import api from "../../api/api";

const categoryApi = new CategoriesApi(undefined, api.defaults.baseURL, api);

const CategoriesPage: React.FC = () => {
  const [categories, setCategories] = useState<CategoryDto[]>([]);
  const [loading, setLoading] = useState(false);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [editingCategory, setEditingCategory] = useState<CategoryDto | null>(
    null
  );
  const [form] = Form.useForm();

  useEffect(() => {
    fetchCategories();
  }, []);

  const fetchCategories = async () => {
    setLoading(true);
    try {
      const response = await categoryApi.apiCategoriesGet(1, 100);
      setCategories(response.data.items);
    } catch (error) {
      message.error("Error while fetching categories");
    }
    setLoading(false);
  };

  const showModal = (category?: CategoryDto) => {
    setEditingCategory(category || null);
    setIsModalVisible(true);
    form.setFieldsValue(category ? { name: category.name } : { name: "" });
  };

  const handleCancel = () => {
    setIsModalVisible(false);
    setEditingCategory(null);
    form.resetFields();
  };

  const handleOk = async () => {
    try {
      const values = await form.validateFields();
      if (editingCategory) {
        await categoryApi.apiCategoriesIdPut(editingCategory.id, values);
        message.success("Category updated!");
      } else {
        await categoryApi.apiCategoriesPost(values);
        message.success("Category added!");
      }
      fetchCategories();
      handleCancel();
    } catch (error) {
      message.error("Error while saving categories");
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await categoryApi.apiCategoriesIdDelete(id);
      message.success("Category removed!");
      fetchCategories();
    } catch (error) {
      message.error("Error while removing category");
    }
  };

  return (
    <div>
      <Button
        type="primary"
        icon={<PlusOutlined />}
        onClick={() => showModal()}
        style={{ marginBottom: 16 }}
      >
        Add category
      </Button>

      <Table
        dataSource={categories}
        rowKey="id"
        loading={loading}
        columns={[
          {
            title: "Name",
            dataIndex: "name",
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
        title={editingCategory ? "Edit" : "Add"}
        open={isModalVisible}
        onOk={handleOk}
        onCancel={handleCancel}
        okText="Save"
        cancelText="Cancel"
      >
        <Form form={form} layout="vertical">
          <Form.Item
            name="name"
            label="Name"
            rules={[{ required: true, message: "Enter the name" }]}
          >
            <Input />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default CategoriesPage;
