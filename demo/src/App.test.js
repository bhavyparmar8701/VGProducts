import { render, screen } from '@testing-library/react';
import App from './App';
import Login from './Component/Login';

test('renders learn react link', () => {
  render(<App />,<Function/>,<Class/>,<Login/>);
  const linkElement = screen.getByText(/learn react/i);
  expect(linkElement).toBeInTheDocument();
});
