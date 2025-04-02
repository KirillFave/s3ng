import React, { useEffect, useState } from "react";
import { useSelector, useDispatch } from "react-redux";
import { CartService } from "../services/cart.service";
import { removeFromCart, setCart, addToCart, clearCart } from "../store/cart/cartSlice";
import { OrderInfo } from "../models/OrderInfo";
import { OrderService } from "../services/order.service";
import { toast } from "react-toastify";

const CartPage = () => {
  const cart = useSelector((state) => state.cart.cart);
  const userId = useSelector((state) => state.user.userData.authenticationId);
  const dispatch = useDispatch();

  useEffect(() => {
    CartService.onCartUpdate((receivedUserId, updatedCart) => {
      if (receivedUserId === userId) {
        if (JSON.stringify(updatedCart) !== JSON.stringify(cart)) {
          dispatch(setCart(updatedCart));
        }
      }
    });

    CartService.getCart(userId);

  }, [userId, dispatch]);

  const handleIncreaseQuantity = (product) => {
    dispatch(addToCart({ product: {id: product.productId, description: product.description, price: product.price, name: product.productName}, userId }));
  };

  const handleDecreaseQuantity = (productId) => {
    dispatch(removeFromCart({ id: productId, userId }));
  };

  const handleOrderSubmit = async () => {
    const orderItems = cart.map(item => ({
      productId: item.productId,
      pricePerUnit: item.price,
      count: item.quantity
    }));
    const orderData = new OrderInfo(userId, orderItems, 0, 1, "Новосибирск, ул. Мира 50");
    const response = await OrderService.createOrder(orderData);
    if(response) {
      dispatch(clearCart(userId))
      toast.success("Заказ оформлен")
    }
    else {
      toast.error("Ошибка при оформлении заказа")
    }
  };

  const totalPrice = cart ? cart.reduce((sum, item) => sum + item.price * item.quantity, 0) : null;

  return (
    <div className="container mx-auto p-4">
      <h2 className="text-2xl font-bold mb-4">Корзина</h2>
      { cart === undefined || cart.length === 0 ? (
        <p className="text-gray-600">Ваша корзина пуста</p>
      ) : (
        <div>
          {cart?.map((item) => (
            <div key={item.productId} className="border-b pb-4 mb-4">
              <h4 className="text-xl font-semibold">{item.productName}</h4>
              <p className="text-gray-700">Цена: {item.price} ₽</p>
              <p className="text-gray-700">Сумма: {item.price * item.quantity} ₽</p>
              <div className="flex items-center space-x-2 mt-2">
                <button
                  onClick={() => handleDecreaseQuantity(item.productId)}
                  className="btn btn-red mx-1 py-1 rounded"
                >
                  -
                </button>
                <span className="text-lg">{item.count}</span>
                <button
                  onClick={() => handleIncreaseQuantity(item)}
                  className="btn btn-green mx-1 py-1 rounded"
                >
                  +
                </button>
              </div>
            </div>
          ))}
          <h3 className="text-xl font-semibold mt-4">Общая стоимость: {totalPrice} ₽</h3>
          <button onClick={handleOrderSubmit} className="btn btn-blue mt-4 py-2 px-4 rounded">Оформить заказ</button>
        </div>
      )}
    </div>
  );
};

export default CartPage;