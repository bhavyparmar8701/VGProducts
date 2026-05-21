import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import Favourites from './Favourites';

describe('<Favourites />', () => {
  test('should mount', () => {
    render(<Favourites />);

    const favourites = screen.getByTestId('Favourites');

    expect(favourites).toBeInTheDocument();
  });
});