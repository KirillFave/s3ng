import { instance } from "../api/axios.api";

export const ProductService = {
  /**
   * Получить список всех продуктов
   * @returns {Promise<Array>}
   */
  async getAllProducts() {
    const { data } = await instance.get("product");
    return data;
  },

  /**
   * Получить продукт по ID
   * @param {string} id
   * @returns {Promise<Object>}
   */
  async getProductById(id) {
    const { data } = await instance.get(`product/${id}`);
    return data;
  },
}