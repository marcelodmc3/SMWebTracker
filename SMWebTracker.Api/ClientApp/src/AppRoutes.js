import { Home } from "./components/Home";
import { default as LoginPage } from "./components/LoginPage";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/loginpage',
    element: <LoginPage />
  }
];

export default AppRoutes;
