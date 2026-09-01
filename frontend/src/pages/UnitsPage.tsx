import { useState, useEffect } from 'react';
import { fetchWithAuth } from '../api';

interface Unit {
    id: number;
    name: string;
    address: string;

    status: string;
    createdAt: string;
}

interface UnitsPageProps{
    onLogout: () => void;
}

export default function UnitsPage({ onLogout}: UnitsPageProps){
    const [units, setUnits] = useState<Unit[]>([]);
    const [name, setName] = useState('');
    const [address, setAddress] = useState('');

    const [message, setMessage] = useState('');

    async function loadUnits(){
        const res = await fetchWithAuth('/units');
        if (res.ok){
            const data = await res.json();
            setUnits(data);
        }
    }

    useEffect(()=>{
        loadUnits();
    }, []);

    async function handleSubmit(e: React.FormEvent){
        e.preventDefault();
        setMessage('');
        const res = await fetchWithAuth('/units',{
            method: 'POST',
            body: JSON.stringify({name, address}),
        });
        if(res.ok){
            setName('');
            setAddress('');
            setMessage('Unit created!');
            loadUnits();
        } else {
            setMessage('Failed to create unit');
        }
    }
    return (
        <div>
            <div>
                <h1>Fairbnb</h1>
                <button onClick={onLogout}>Logout</button>
            </div>

            <h2>Create Unit</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Name</label>
                    <input
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Address</label>
                    <input
                        value={address}
                        onChange={(e) => setAddress(e.target.value)}
                        required
                    />
                </div>

                <button type="submit">Create</button>
            </form>
            {message && <p>{message}</p>}

            <h2>Your Units</h2>
            {units.length === 0 ? (
                <p>No units yet. Create your first one!</p> ) : (
                <ul>
                    {units.map((unit) => (
                        <li key={unit.id}>
                            <strong>{unit.name}</strong> — {unit.address}
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
}