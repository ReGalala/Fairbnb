import { useState } from 'react';
import { isLoggedIn, removeToken } from './api';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';

export default function App() {
    const [loggedIn, setLoggedIn] = useState(isLoggedIn());
    const [page, setPage] = useState<'login' | 'register'>('login');

    function handleLogin() {
      setLoggedIn(true);
    }

    function handleLogout() {
      removeToken();
      setLoggedIn(false);
    }

    if (!loggedIn) {
      if (page === 'register') {
        return (
          <RegisterPage
            onLogin={handleLogin}
            onSwitchToLogin={() => setPage('login')}
          />
        );
      }
      return (
        <LoginPage
          onLogin={handleLogin}
          onSwitchToRegister={() => setPage('register')}
        />
      );
    }

    return (
      <div>
        <h1>Welcome to Fairbnb!</h1>
        <button onClick={handleLogout}>Logout</button>
      </div>
    );
  }
