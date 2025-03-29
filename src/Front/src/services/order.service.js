import { instance } from "../api/axios.api";
import { OrderInfo } from "../models/OrderInfo";

export const OrderService = {
    /**
     * Получить список всех заказов пользователя
     * @param {string} userId
     * @returns {Promise<Array>}
     */
    async getAllOrders(userId) {
        const { data } = await instance.get(`order/get-orders/user/${userId}`);
        return data;
    },
  
    /**
     * Создать заказ
     * @param {OrderInfo} orderData
     * @returns {Promise<Object>}
     */
    async createOrder(orderData) {
        const { data } = await instance.put(`order/create`, orderData);
        return data;
    }
};