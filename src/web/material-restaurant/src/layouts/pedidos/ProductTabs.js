import React from "react";
import { AppBar, Tabs, Tab, Icon, TextField, List, ListItem, ListItemText } from "@mui/material";
import MDBox from "components/MDBox";

const ProductTabs = ({ productTypes, products, tabValue, handleSetTabValue, handleProductChange }) => {
  return (
    <>
      <AppBar position="static">
        <Tabs value={tabValue} onChange={handleSetTabValue}>
          {productTypes.map((type, index) => (
            <Tab
              key={index}
              label={type.name}
              icon={
                <Icon fontSize="small" sx={{ mt: -0.25 }}>
                  category
                </Icon>
              }
            />
          ))}
        </Tabs>
      </AppBar>
      {productTypes.map((type, index) => (
        tabValue === index && (
          <MDBox key={index} p={3}>
            <List>
              {products
                .filter(product => product.typeId === type.id)
                .map((product, productIndex) => (
                  <ListItem key={productIndex}>
                    <ListItemText primary={product.name} />
                    <TextField
                      label="Quantidade"
                      type="number"
                      value={product.quantity}
                      onChange={(e) => handleProductChange(product.id, e)}
                    />
                  </ListItem>
                ))}
            </List>
          </MDBox>
        )
      ))}
    </>
  );
};

export default React.memo(ProductTabs);