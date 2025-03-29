import { createSlice } from "@reduxjs/toolkit";
import { CartService } from "../../services/cart.service";

const initialState = {
  cart: [],
};

const cartSlice = createSlice({
  name: "cart",
  initialState,
  reducers: {
    setCart: (state, action) => {
      state.cart = action.payload;
    },
    addToCart: (state, action) => {
      const { id, description, price, name } = action.payload.product;
      const { userId } = action.payload;

      const item = {
        productId: id,
        productName: name,
        price: price,
        quantity: 1
      };

      CartService.addToCart(userId, item);
    },
    removeFromCart: (state, action) => {
      const { id, userId } = action.payload;
      CartService.removeFromCart(userId, id);

      if (state.cart.length == 1) {
        state.cart = [];
      }
    },
    updateQuantity: (state, action) => {
      const { id, quantity, userId } = action.payload;
      const item = state.cart.find((item) => item.id === id);
      if (item && quantity > 0) {
        item.quantity = quantity;
      }
      CartService.addToCart(userId, { productId: id, count: quantity });
    },
    clearCart: (state, action) => {
      CartService.clearCart(action.payload)
      state.cart = [];
    },
  },
});

export const { setCart, addToCart, removeFromCart, updateQuantity, clearCart } = cartSlice.actions;

export default cartSlice.reducer;