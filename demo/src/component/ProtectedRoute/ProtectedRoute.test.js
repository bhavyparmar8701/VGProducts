import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import ProtectedRoute from './ProtectedRoute';

describe('<ProtectedRoute />', () => {
  test('should mount', () => {
    render(<ProtectedRoute />);

    const protectedRoute = screen.getByTestId('ProtectedRoute');

    expect(protectedRoute).toBeInTheDocument();
  });
});