import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import Electronics from './Electronics';

describe('<Electronics />', () => {
  test('should mount', () => {
    render(<Electronics />);

    const electronics = screen.getByTestId('Electronics');

    expect(electronics).toBeInTheDocument();
  });
});