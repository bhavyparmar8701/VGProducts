import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import ForgotPassword from './ForgotPassword';

describe('<ForgotPassword />', () => {
  test('should mount', () => {
    render(<ForgotPassword />);

    const forgotPassword = screen.getByTestId('ForgotPassword');

    expect(forgotPassword).toBeInTheDocument();
  });
});