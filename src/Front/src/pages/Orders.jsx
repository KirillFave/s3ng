import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { OrderService } from "../services/order.service";

const OrdersPage = () => {
  const [orders, setOrders] = useState([]);
  const [error, setError] = useState(null);
  const userId = useSelector((state) => state.user.userData?.authenticationId);

  useEffect(() => {
    const fetchOrders = async () => {
      try {
        if (!userId) throw new Error("Пользователь не авторизован");
        const data = await OrderService.getAllOrders(userId);
        if (!data) throw new Error("Сервер не вернул данные");
        setOrders(data);
      } catch (err) {
        console.error("Ошибка загрузки заказов:", err);
        setError("Ошибка загрузки заказов. Попробуйте позже.");
        setOrders([]);
      }
    };
    if (userId) fetchOrders();
  }, [userId]);

  if (error) return <div className="text-red-500">{error}</div>;

  return (
    <div className="container mx-auto p-4">
      <h2 className="text-2xl font-bold mb-4">Ваши заказы</h2>
      {orders.length > 0 ? (
        orders.map((order) => (
          <div key={order.id} className="border-b pb-4 mb-4">
            <h4 className="text-xl font-semibold">Заказ ID: {order.id}</h4>
            <p className="text-gray-700">Статус: В работе</p>
            <p className="text-gray-700">Адрес доставки: {order.shipAddress}</p>
            <p className="text-gray-700">Дата: {order.createdTimestamp}</p>
          </div>
        ))
      ) : (
        <p className="text-gray-600">Заказов пока нет</p>
      )}
    </div>
  );
};

export default OrdersPage;
