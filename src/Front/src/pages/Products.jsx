import { useEffect } from 'react'
import { fetchProducts } from '../store/product/productSlice';
import { useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';

export default function Product() {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const { data: products, status } = useSelector((state) => state.product);

  useEffect(() => {
    dispatch(fetchProducts());
  }, []);

  if (status == "loading") {
    return <h1>Loading....</h1>;
  }
  if (status == "error") {
    return <h1>Error ! Something went wrong</h1>;
  }

  return (
    <div className="relative w-full">
      <div className="relative bg-white-50">
        <div className="container m-auto px-6 pt-32 md:px-12 lg:pt-[4.8rem] lg:px-7">
          <h1 className="text-2xl font-bold text-yellow-900 md:text-3xl lg:w-10/12">
            Our TOP products
          </h1>

          <div className="flex flex-wrap justify-between">
            {products.map((product) => {
              return (
                <div
                  onClick={() => navigate(`/productdetails/${product.id}`)}
                  key={product.id}
                  className="mx-auto overflow-hidden duration-300 transform bg-white rounded-lg shadow-md mt-11 w-80 dark:bg-slate-800 hover:scale-105 hover:shadow-lg"
                >
                  <img
                    className="object-cover object-center w-full h-48"
                    src={product.imageUrl?.replace("http://minio:9000//", "http://localhost/minio/")}
                    alt="Product Image"
                  />
                  <div className="p-4">
                    <h2 className="mb-2 text-lg font-medium text-gray-900 dark:text-white">
                      {product.name}
                    </h2>
                    <p className="mb-2 text-base text-gray-700 dark:text-gray-300">
                      {product.description}.
                    </p>
                    <div className="flex items-center">
                      <p className="mr-2 text-lg font-semibold text-gray-900 dark:text-white">
                        Rs.{product.price}
                      </p>
                      <p className="text-base font-medium text-gray-500 line-through dark:text-gray-300">
                        $25.00
                      </p>
                    </div>
                  </div>
                </div>
              );
            })}
          </div>
        </div>
      </div>
    </div>
  );
}