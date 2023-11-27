import { Home } from "./components/Home";
import GamesPage from "./components/GamesPage";
import GamePage from "./components/GamePage";

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
  {
   path: '/game/:id',
   element: <GamePage />
  },
];

export default AppRoutes;
