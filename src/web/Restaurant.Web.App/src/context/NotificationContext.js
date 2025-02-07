import React, { createContext, useState, useEffect } from "react";
import MDSnackbar from "components/MDSnackbar"; // Ensure this is the correct path
import Icon from "@mui/material/Icon";

const NotificationContext = createContext();

const NotificationProvider = ({ children }) => {
  const [notification, setNotification] = useState({
    open: false,
    color: "info",
    icon: null,
    title: "",
    content: "",
  });

  const showNotification = (color, icon, title, content) => {
    setNotification({ open: true, color, icon, title, content });
  };

  const closeNotification = () => {
    setNotification({ ...notification, open: false });
  };

  useEffect(() => {
    if (notification.open) {
      const timer = setTimeout(() => {
        setNotification({ ...notification, open: false });
      }, 8000);

      return () => clearTimeout(timer);
    }
  }, [notification]);

  const renderNotification = (
    <MDSnackbar
      color={notification.color}
      icon={<Icon>{notification.icon}</Icon>}
      title={notification.title}
      content={notification.content}
      open={notification.open}
      onClose={closeNotification}
      close={
        <button
          onClick={closeNotification}
          style={{
            background: "none",
            border: "none",
            color: "inherit",
            cursor: "pointer",
            fontSize: "16px",
          }}
        >
          &times;
        </button>
      }
      bgWhite
    />
  );

  return (
    <NotificationContext.Provider value={{ showNotification }}>
      {children}
      {renderNotification}
    </NotificationContext.Provider>
  );
};

export { NotificationProvider, NotificationContext };