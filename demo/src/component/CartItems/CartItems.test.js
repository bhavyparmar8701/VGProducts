import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import CartItems from './CartItems';

describe('<CartItems />', () => {
  test('should mount', () => {
    render(<CartItems />);

    const cartItems = screen.getByTestId('CartItems');

    expect(cartItems).toBeInTheDocument();
  });
});