import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import Payments from './Payments';

describe('<Payments />', () => {
  test('should mount', () => {
    render(<Payments />);

    const payments = screen.getByTestId('Payments');

    expect(payments).toBeInTheDocument();
  });
});