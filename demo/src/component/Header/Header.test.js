import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import Header from './Header';

describe('<Header />', () => {
  test('should mount', () => {
    render(<Header />);

    const header = screen.getByTestId('Header');

    expect(header).toBeInTheDocument();
  });
});