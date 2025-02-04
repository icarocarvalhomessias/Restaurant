import React, { useContext } from "react";
import { IconButton, Tooltip } from "@mui/material";
import Icon from "@mui/material/Icon";
import MDBox from "components/MDBox";
import MDTypography from "components/MDTypography";
import AuthContext from "context/AuthContext";

function DashboardNavbar() {
  const { logout } = useContext(AuthContext);

  return (
    <MDBox display="flex" justifyContent="space-between" alignItems="center" p={2}>
      <MDTypography variant="h6" color="white">
        Dashboard
      </MDTypography>
      <MDBox display="flex" alignItems="center">
        <Tooltip title="Notifications">
          <IconButton color="inherit">
            <Icon>notifications</Icon>
          </IconButton>
        </Tooltip>
        <Tooltip title="Logout">
          <IconButton color="inherit" onClick={logout}>
            <Icon>logout</Icon>
          </IconButton>
        </Tooltip>
      </MDBox>
    </MDBox>
  );
}

export default DashboardNavbar;