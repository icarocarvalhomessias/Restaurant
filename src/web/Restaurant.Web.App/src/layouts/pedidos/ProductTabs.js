import React from "react";
import { TextField, List, ListItem, ListItemText, Typography, Grid } from "@mui/material";
import MDBox from "components/MDBox";

const ProductTabs = ({ productTypes, products, handleProductChange }) => {
  return (
    <>
      {productTypes.map((type, index) => (
        <MDBox key={index} p={3} mb={2}>
          <Typography variant="h6" gutterBottom>
            {type.name}
          </Typography>
          <List>
            {products
              .filter(product => product.typeId === type.id)
              .map((product, productIndex) => (
                <ListItem key={productIndex}>
                  <Grid container alignItems="center">
                    <Grid item xs={2}>
                      <ListItemText primary={product.name} />
                    </Grid>
                    <Grid item xs={1}>
                      <TextField
                        label="Quantidade"
                        type="number"
                        value={product.quantity}
                        onChange={(e) => handleProductChange(product.id, e)}
                        fullWidth
                      />
                    </Grid>
                  </Grid>
                </ListItem>
              ))}
          </List>
        </MDBox>
      ))}
    </>
  );
};

export default React.memo(ProductTabs);