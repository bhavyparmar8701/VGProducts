import { lazy, Suspense } from 'react';

const LazyElectronics = lazy(() => import('./Electronics'));

const Electronics = (props) => (
  <Suspense fallback={null}>
    <LazyElectronics {...props} />
  </Suspense>
);

export default Electronics;
