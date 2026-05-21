import { lazy, Suspense } from 'react';

const LazyAddress = lazy(() => import('./Address'));

const Address = (props) => (
  <Suspense fallback={null}>
    <LazyAddress {...props} />
  </Suspense>
);

export default Address;
