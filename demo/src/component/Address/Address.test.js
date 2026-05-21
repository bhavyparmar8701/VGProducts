import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import Address from './Address';

describe('<Address />', () => {
  test('should mount', () => {
    render(<Address />);

    const address = screen.getByTestId('Address');

    expect(address).toBeInTheDocument();
  });
});