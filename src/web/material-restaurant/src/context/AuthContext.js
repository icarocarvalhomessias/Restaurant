import React, { createContext, useState, useEffect } from "react";
import {jwtDecode} from "jwt-decode";
import Cookies from "js-cookie";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const token = Cookies.get("token");
    if (token && typeof token === "string") {
      try {
        const decodedToken = jwtDecode(token);
        setUser(decodedToken); // Ensure the entire decoded token is set
        setIsAuthenticated(true);
      } catch (error) {
        console.error("Invalid token:", error);
        Cookies.remove("token");
      }
    }
    setLoading(false);
  }, []);

  const login = (token) => {
    if (token && typeof token === "string") {
      try {
        const decodedToken = jwtDecode(token);
        Cookies.set("token", token, { expires: 7 }); // Set cookie to expire in 7 days
        setUser(decodedToken); // Ensure the entire decoded token is set
        setIsAuthenticated(true);
      } catch (error) {
        console.error("Invalid token login:", error);
      }
    } else {
      console.error("Invalid token format");
    }
  };

  const logout = () => {
    Cookies.remove("token");
    setUser(null);
    setIsAuthenticated(false);
  };

  if (loading) {
    return <div>Loading...</div>; // You can replace this with a loading spinner if you prefer
  }

  return (
    <AuthContext.Provider value={{ isAuthenticated, user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContext;
