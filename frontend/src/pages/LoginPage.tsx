import { useState } from 'react';
import { login, saveToken } from '../api';

interface LoginPageProps {
    onLogin: () => void;
    onSwitchToRegister: () => void;
}

export default function LoginPage({ onLogin, onSwitchToRegister}: LoginPageProps){
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');

    async function handleSubmit(e: React.FormEvent){
        e.preventDefault();
        setError('');
        try{
            const data = await login(email,password);
            saveToken(data.token);
            onLogin();
        }catch{
            setError('invalid email or password');
        }
    }
    return(
        <div>
            <h1>Login</h1>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Email</label>
                    <input
                        type="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Password</label>
                    <input
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>
                {error && <p style={{ color: 'red' }}>{error}</p>}
                <button type="submit">Login</button>
            </form>
            <p>
                No account? <button onClick={onSwitchToRegister}>Register</button>
            </p>
        </div>
    );
}
