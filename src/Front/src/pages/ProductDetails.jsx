import React from "react";

import { useParams } from "react-router-dom";
import Product from "../components/Product";
import Review from "../components/Review";

const ProductDetails = () => {
  const { id } = useParams();

  return (
    <>
      <Product id={id} />
      <Review />
    </>
  );
};

export default ProductDetails;