const API_URL = 'http://localhost:5207/api';

export async function register(email: string, password: string){
    const res = await fetch(`${API_URL}/auth/register`,{
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body:JSON.stringify({email, password}),
    });
    if(!res.ok) throw new Error('Registration failed');
    return res.json();
}

export async function login(email: string, password: string){
    const res = await fetch(`${API_URL}/auth/login`,{
        method: 'POST',
        headers: { 'Content-Type' : 'application/json'},
        body: JSON.stringify({email, password}),
    });
    if(!res.ok) throw new Error('Invalid email or password');
    return res.json();

}

export function getToken(): string | null{
    return localStorage.getItem('token');
}

export function saveToken(token: string){
    localStorage.setItem('token', token);
}

export function removeToken(){
    localStorage.removeItem('token');
}

export function isLoggedIn() : boolean {
    return !!getToken();
}

export async function fetchWithAuth(url: string, options: RequestInit = {}){
    const token = getToken();
    const res = await fetch(`${API_URL}${url}`,{
        ...options,
        headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${token}`,
            ...options.headers,
        },
    });
    return res;
}