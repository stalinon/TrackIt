import React, { useState } from "react";
import { FloatButton, Modal, Input, message, Timeline } from "antd";
import { SendOutlined } from "@ant-design/icons";
import { QRCode } from "antd";
import { UserApi } from "../../api/generated";
import api from "../../api/api";

const userApi = new UserApi(undefined, api.defaults.baseURL, api);

const LinkTelegram: React.FC = () => {
  const [visible, setVisible] = useState(false);
  const [code, setCode] = useState<string>("");

  const handleSetCode = async () => {
    try {
      await userApi.apiUsersLinkPost({ code });
      message.success("Code successfully set");
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
        onClick={() => setVisible(true)}
        tooltip={<div>Link Telegram</div>}
      />

      <Modal
        title="Linking Telegram"
        open={visible}
        onCancel={() => setVisible(false)}
        onOk={handleSetCode} // Выставление кода
        okText="Send code"
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
              children: (
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
