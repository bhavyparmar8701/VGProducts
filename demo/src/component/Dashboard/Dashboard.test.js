import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import Dashboard from './Dashboard';

describe('<Dashboard />', () => {
  test('should mount', () => {
    render(<Dashboard />);

    const dashboard = screen.getByTestId('Dashboard');

    expect(dashboard).toBeInTheDocument();
  });
});