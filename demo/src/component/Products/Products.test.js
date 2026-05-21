import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import Products from './Products';

describe('<Products />', () => {
  test('should mount', () => {
    render(<Products />);

    const products = screen.getByTestId('Products');

    expect(products).toBeInTheDocument();
  });
});