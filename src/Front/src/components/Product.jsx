import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { fetchProductDetails } from "../store/product/productSlice";
import { addToCart } from "../store/cart/cartSlice";

const Product = ({ id: productId }) => {
  const { selectedProduct, status } = useSelector((state) => state.product);
  const dispatch = useDispatch();
  const userId = useSelector((state) => state.user.userData.authenticationId);

  useEffect(() => {
    if (productId) {
      dispatch(fetchProductDetails(productId));
    }
  }, [productId, dispatch]);

  console.log("selectedProduct:", selectedProduct); 
  const product = selectedProduct;

  if (status === "loading") return <p>Loading...</p>;
  if (status === "error") return <p>Error loading product</p>;

  return (
    <section className="text-gray-700 body-font overflow-hidden bg-white">
      <div className="container px-5 py-24 mx-auto">
        <div className="lg:w-4/5 mx-auto flex flex-wrap">
          <img
            alt="ecommerce"
            className="lg:w-1/2 w-full object-cover object-center rounded border border-gray-200"
            src={product.imageUrl?.replace("http://minio:9000//", "http://localhost/minio/")}
          />
          <div className="lg:w-1/2 w-full lg:pl-10 lg:py-6 mt-6 lg:mt-0">
            <h1 className="text-gray-900 text-3xl title-font font-medium mb-1">
              {product.name || "Product Name"}
            </h1>
            <p className="leading-relaxed">{product.description || "No description available"}</p>
            <div className="flex">
              <span className="title-font font-medium text-2xl text-gray-900">
                Rs. {product.price || "N/A"}
              </span>
              <button
                className="flex ml-auto text-white bg-red-500 border-0 py-2 px-6 focus:outline-none hover:bg-red-600 rounded"
                onClick={() => {
                  console.log("Adding to cart:", product);
                  if (product?.name) {
                    dispatch(addToCart({product, userId}));
                  }
                }}
                disabled={!product?.name}
              >
                Add To Cart
              </button>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default Product;