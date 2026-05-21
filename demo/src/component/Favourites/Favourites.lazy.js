import { lazy, Suspense } from 'react';

const LazyFavourites = lazy(() => import('./Favourites'));

const Favourites = (props) => (
  <Suspense fallback={null}>
    <LazyFavourites {...props} />
  </Suspense>
);

export default Favourites;
