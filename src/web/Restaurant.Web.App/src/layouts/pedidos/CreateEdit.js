import React, { useState, useContext, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Grid, Card, TextField, Button } from "@mui/material";
import { NotificationContext } from "context/NotificationContext";
import DashboardLayout from "examples/LayoutContainers/DashboardLayout";
import DashboardNavbar from "examples/Navbars/DashboardNavbar";
import MDBox from "components/MDBox";
import MDTypography from "components/MDTypography";
import ProductTabs from "./ProductTabs";
import { createOrder, updateOrder, getOrderById, getProducts } from "services/api";

// Enum for product types
const ProductType = {
  FOOD: 0,
  DRINK: 1,
};

const CreateEdit = () => {
  const { showNotification } = useContext(NotificationContext);
  const navigate = useNavigate();
  const { id } = useParams();
  const [orderData, setOrderData] = useState({
    clientName: "",
    clientAddress: "",
    clientPhone: "",
    orderProducts: [],
    usuarioId: "",
  });
  const [productTypes, setProductTypes] = useState([
    { id: ProductType.FOOD, name: "Food" },
    { id: ProductType.DRINK, name: "Drink" },
  ]);
  const [products, setProducts] = useState([]);
  const [tabValue, setTabValue] = useState(0);

  useEffect(() => {
    
    const fetchData = async () => {
      const pedido = await getOrderById(id);
      const products = await getProducts();
      await fetchOrderData(pedido);
      await fetchProductData(products, pedido);
    };
  

    const fetchOrderData = async (pedido) => {
      if (id) {
        try {
          
          const { clientName, clientAddress, clientPhone, orderProducts, usuarioId } = pedido.data;
          setOrderData({ clientName, clientAddress, clientPhone, orderProducts, usuarioId });
        } catch (error) {
          showNotification("error", "warning", "Erro", "Erro ao carregar dados do pedido.");
        }
      }
    };

    const fetchProductData = async (products, pedido) => {
      try {
        var ordedeProducts = pedido.data.orderProducts;
        const productsWithQuantities = products.data.map(product => {
          
          const orderProduct = ordedeProducts.find(p => p.productId === product.id);
          
          return {
            ...product,
            quantity: orderProduct ? orderProduct.quantity : 0,
            typeId: product.type, // Ensure typeId is set correctly
          };
        });

        setProducts(productsWithQuantities);
      } catch (error) {
        showNotification("error", "warning", "Erro", "Erro ao carregar dados dos produtos.");
      }
    };

    fetchData();
  }, [id, showNotification]);

  const handleSetTabValue = (event, newValue) => {
    setTabValue(newValue);
  };

  const handleProductChange = (productId, event) => {
    const newProducts = products.map(product =>
      product.id === productId ? { ...product, quantity: event.target.value } : product
    );
    setProducts(newProducts);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const updatedOrderData = {
        clientName: orderData.clientName,
        clientAddress: orderData.clientAddress,
        clientPhone: orderData.clientPhone,
        orderProduct: products.filter(product => product.quantity > 0).map(product => ({
          product: { id: product.id },
          quantity: product.quantity,
        })),
        usuarioId: orderData.usuarioId,
      };
      if (id) {
        await updateOrder(id, updatedOrderData);
        showNotification("success", "check", "Pedido", "Pedido atualizado com sucesso!");
      } else {
        await createOrder(updatedOrderData);
        showNotification("success", "check", "Pedido", "Pedido criado com sucesso!");
      }
      navigate("/pedidos");
    } catch (error) {
      showNotification("error", "warning", "Erro", "Erro ao salvar pedido.");
    }
  };

  return (
    <DashboardLayout>
      <DashboardNavbar />
      <MDBox pt={6} pb={3}>
        <Grid container spacing={6}>
          <Grid item xs={12}>
            <Card>
              <MDBox p={3}>
                <MDTypography variant="h6" color="white" gutterBottom>
                  {id ? "Editar Pedido" : "Criar Pedido"}
                </MDTypography>
                <form onSubmit={handleSubmit}>
                  <Grid container spacing={4}>
                    <Grid item xs={6}>
                      <TextField
                        fullWidth
                        label="Nome do Cliente"
                        name="clientName"
                        value={orderData.clientName}
                        onChange={(e) => setOrderData({ ...orderData, clientName: e.target.value })}
                      />
                    </Grid>
                    <Grid item xs={6}>
                      <TextField
                        fullWidth
                        label="Endereço do Cliente"
                        name="clientAddress"
                        value={orderData.clientAddress}
                        onChange={(e) => setOrderData({ ...orderData, clientAddress: e.target.value })}
                      />
                    </Grid>
                    <Grid item xs={6}>
                      <TextField
                        fullWidth
                        label="Telefone do Cliente"
                        name="clientPhone"
                        value={orderData.clientPhone}
                        onChange={(e) => setOrderData({ ...orderData, clientPhone: e.target.value })}
                      />
                    </Grid>
                    <Grid item xs={12}>
                      <ProductTabs
                        productTypes={productTypes}
                        products={products}
                        handleProductChange={handleProductChange}
                      />
                    </Grid>
                    <Grid item xs={12} style={{ textAlign: 'left' }}>
                      <Button variant="contained" style={{ backgroundColor: 'black', color: 'white' }} type="submit">
                        Salvar Pedido
                      </Button>
                    </Grid>
                  </Grid>
                </form>
              </MDBox>
            </Card>
          </Grid>
        </Grid>
      </MDBox>
    </DashboardLayout>
  );
};

export default CreateEdit;