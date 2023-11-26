import { Home } from "./components/Home";
import GamesPage from "./components/GamesPage";
import { default as LoginPage } from "./components/LoginPage";

const AppRoutes = [
  {
   index: true,
   element: <LoginPage />
  },
  {
    path: '/home',
    element: <Home />
  },
  {    
    path: '/loginpage',
    element: <LoginPage />
  },
  {
    path: '/gamespage',
    element: <GamesPage />
  },
];

export default AppRoutes;
