import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5263/api", // Update with your API base URL
});

export const getOrders = () => api.get("/orders");

export const createOrder = (orderData) => {
  return api.post("/orders", orderData);
};

export const getProducts = () => api.get("/products");

export const getOrderById = (id) => api.get(`/orders/${id}`);

export const updateOrder = (id, order) => {
  console.log("Updating order:", order);
  return api.put(`/Orders/${id}`, order);
};

export const deleteOrder = (id) => api.delete(`/orders/${id}`);

// New login function
export const login = (email, password) =>
  api.post("/auth/authenticate", {
    email,
    password,
  });

export default api;
