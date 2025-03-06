import React, { useState } from "react";
import { FloatButton, Modal, Input, message, Timeline } from "antd";
import { SendOutlined, CheckCircleOutlined } from "@ant-design/icons";
import { QRCode } from "antd";
import { UserApi } from "../../api/generated";
import api from "../../api/api";

const userApi = new UserApi(undefined, api.defaults.baseURL, api);

const LinkTelegram: React.FC = () => {
  const [visible, setVisible] = useState(false);
  const [code, setCode] = useState<string>("");
  const [isLinked, setIsLinked] = useState(false); // Состояние успешной привязки

  const handleSetCode = async () => {
    try {
      await userApi.apiUsersLinkPost({ code });
      message.success("Code successfully set");
      setIsLinked(true); // Успешная привязка
      setVisible(false);
    } catch (error) {
      message.error("Couldn't set Telegram code");
    }
  };

  return (
    <>
      <FloatButton
        icon={<SendOutlined />}
        type="primary"
        onClick={() => {
          setVisible(true);
          setIsLinked(false); // Сбрасываем состояние при открытии
        }}
        tooltip={<div>Link Telegram</div>}
      />

      <Modal
        title="Linking Telegram"
        open={visible}
        onCancel={() => setVisible(false)}
        onOk={isLinked ? () => setVisible(false) : handleSetCode} // Если успешно, закрываем
        okText={isLinked ? "Close" : "Send code"} // Меняем текст кнопки
        cancelButtonProps={{ style: { display: isLinked ? "none" : "inline-block" } }} // Прячем "Cancel"
      >
        <br />
        <Timeline
          items={[
            {
              children: (
                <>
                  Open <b>Telegram</b> and send command <b>/link</b> to the bot
                </>
              ),
            },
            {
              children: (
                <div>
                  Go to the bot:{" "}
                  <a
                    href="https://t.me/trackit_notify_bot"
                    target="_blank"
                    rel="noopener noreferrer"
                  >
                    @trackit_notify_bot
                  </a>
                  <QRCode
                    value="https://t.me/trackit_notify_bot"
                    size={150}
                    style={{ display: "flex", margin: "10px auto" }}
                  />
                </div>
              ),
            },
            {
              children: isLinked ? ( // Если успешно, показываем галочку и текст
                <div style={{ textAlign: "center", fontSize: 16, color: "green" }}>
                  <CheckCircleOutlined style={{ fontSize: 32, marginBottom: 8 }} />
                  <br />
                  Successfully linked Telegram
                </div>
              ) : (
                <>
                  Enter code here:
                  <Input
                    value={code}
                    onChange={(e) => setCode(e.target.value)}
                    style={{ marginTop: 8 }}
                  />
                </>
              ),
            },
          ]}
        />
      </Modal>
    </>
  );
};

export default LinkTelegram;
