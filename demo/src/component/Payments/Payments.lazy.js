import { lazy, Suspense } from 'react';

const LazyPayments = lazy(() => import('./Payments'));

const Payments = (props) => (
  <Suspense fallback={null}>
    <LazyPayments {...props} />
  </Suspense>
);

export default Payments;
