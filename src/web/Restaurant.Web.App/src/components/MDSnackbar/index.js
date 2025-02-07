/**
=========================================================
* Material Dashboard 2 React - v2.2.0
=========================================================

* Product Page: https://www.creative-tim.com/product/material-dashboard-react
* Copyright 2023 Creative Tim (https://www.creative-tim.com)

Coded by www.creative-tim.com

 =========================================================

* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
*/

import React from "react";
import PropTypes from "prop-types";
import Snackbar from "@mui/material/Snackbar";
import MDBox from "components/MDBox";
import MDTypography from "components/MDTypography";

function MDSnackbar({ color, icon, title, content, open, onClose, close, bgWhite }) {
  return (
    <Snackbar
      anchorOrigin={{ vertical: "top", horizontal: "right" }}
      open={open}
      onClose={onClose}
      message={
        <MDBox display="flex" alignItems="center" color={color}>
          {icon && <MDBox mr={1}>{icon}</MDBox>}
          <MDBox>
            <MDTypography variant="h6" color="inherit">
              {title}
            </MDTypography>
            <MDTypography variant="body2" color="inherit">
              {content}
            </MDTypography>
          </MDBox>
        </MDBox>
      }
      action={close}
      ContentProps={{
        style: {
          backgroundColor: bgWhite ? "white" : undefined,
        },
      }}
    />
  );
}

MDSnackbar.propTypes = {
  color: PropTypes.string,
  icon: PropTypes.node,
  title: PropTypes.string.isRequired,
  content: PropTypes.string.isRequired,
  open: PropTypes.bool.isRequired,
  onClose: PropTypes.func.isRequired,
  close: PropTypes.node,
  bgWhite: PropTypes.bool,
};

export default MDSnackbar;
