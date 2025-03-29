import { createBrowserRouter } from "react-router-dom"
import Layout from "../pages/Layout"
import ErrorPage from "../pages/ErrorPage"
import HomePage from "../pages/HomePage"
import Orders from "../pages/Orders"
import Products from "../pages/Products"
import Auth from "../pages/Auth"
import ProductDetails from "../pages/ProductDetails"
import Cart from "../pages/Cart"

export const router = createBrowserRouter([
    {
        path: "/",
        element: <Layout/>,
        errorElement: <ErrorPage/>,
        children: [
            {
                index: true,
                element: <HomePage/>
            },
            {
                path: "products",
                element: <Products/>
            },
            {
                path: "orders",
                element: <Orders/>
            },
            {
                path: "auth",
                element: <Auth/>
            },
            {
                path: "*",
                element: <ErrorPage/>
            },
            {
                path: "/productDetails/:id",
                element: <ProductDetails/>
            },
            {
                path: "/cart",
                element: <Cart/>
            }
        ]
    }
])