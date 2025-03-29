import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { STATUSES } from "../../helpers/statuses";
import { ProductService } from "../../services/product.service";

const productSlice = createSlice({
    name: "product",
    initialState: {
        data: [],
        status: STATUSES.LOADING,
        selectedProduct: {},
    },
    reducers: {
        setProducts(state, action) {
            state.data = action.payload;
        },
        setStatus(state, action) {
            state.status = action.payload;
        },
        setselectedProduct(state, action) {
            state.selectedProduct = action.payload;
        },
    },
    extraReducers: (builder) => {
        builder
          .addCase(fetchProducts.pending, (state) => {
            state.status = STATUSES.LOADING;
          })
          .addCase(fetchProducts.fulfilled, (state, action) => {
            state.data = action.payload;
            state.status = STATUSES.SUCCESS;
          })
          .addCase(fetchProducts.rejected, (state) => {
            state.status = STATUSES.ERROR;
          });
      },
});

export const { setProducts, setStatus, setselectedProduct } = productSlice.actions;

export default productSlice.reducer;

export const fetchProducts = createAsyncThunk("products/fetch", async () => {
    const response = await ProductService.getAllProducts();
    const data = response;
    return data;
});

export function fetchProductDetails(productId) {
    return async function fetchProductDetailsThunk(dispatch) {
      dispatch(setStatus(STATUSES.LOADING));
      try {
        const response = await ProductService.getProductById(productId);
        dispatch(setselectedProduct(response));
        dispatch(setStatus(STATUSES.SUCCESS));
      } catch (error) {
        console.log(error);
        dispatch(setStatus(STATUSES.ERROR));
      }
    };
}