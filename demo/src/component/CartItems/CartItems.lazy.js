import { lazy, Suspense } from 'react';

const LazyCartItems = lazy(() => import('./CartItems'));

const CartItems = (props) => (
  <Suspense fallback={null}>
    <LazyCartItems {...props} />
  </Suspense>
);

export default CartItems;
