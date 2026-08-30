import { useState } from 'react';
import { register, saveToken } from '../api';

interface RegisterPageProps {
    onLogin: () => void;
    onSwitchToLogin: () => void;
}

export default function RegisterPage({ onLogin, onSwitchToLogin}: RegisterPageProps){
    const [email,setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');

    async function handleSubmit(e: React.FormEvent){
        e.preventDefault();
        setError('');
        try{
            const data = await register(email, password);
            saveToken(data.token);
            onLogin();
        }catch{
            setError('Registration failed. Try a stronger password.');
        }
    }
    return(
        <div>
            <h1>Register</h1>
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
                <button type="submit">Register</button>
            </form>
            <p>
                Already have an account? <button 
                onClick={onSwitchToLogin}>Login</button>
            </p>
        </div>
    );
}