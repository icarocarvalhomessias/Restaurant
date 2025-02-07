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

/**
 * The base border styles for the Material Dashboard 2 React.
 * You can add new border width, border color or border radius using this file.
 * You can customized the borders value for the entire Material Dashboard 2 React using thie file.
 */

// Material Dashboard 2 React Base Styles
// import colors from "assets/theme/base/colors";
import colors from "../base/colors";

// Material Dashboard 2 React Helper Functions
import pxToRem from "../functions/pxToRem";

const { grey } = colors;

const borders = {
  borderColor: "grey",
  borderWidth: {
    0: 0,
    1: "1px",
    2: "2px",
    3: "3px",
    4: "4px",
    5: "5px",
  },
  borderRadius: {
    xs: "1.6px",
    sm: "2px",
    md: "6px",
    lg: "8px",
    xl: "12px",
    xxl: "16px",
    section: "160px",
  },
};

export default borders;
