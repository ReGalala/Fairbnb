import { useState } from 'react';
import Sidebar from './Sidebar';
import Header from './Header';
import hamburger from '../assets/hamburger.png';

interface LayoutProps {
    activePage: string;
    onNavigate: (page: string) => void;
    children: React.ReactNode;
}

export default function Layout({ activePage, onNavigate, children }: LayoutProps){
    const [sidebarOpen, setSidebarOpen] = useState(false);

    return (
        <div className={`layout ${sidebarOpen ? 'sidebar-open' : ''}`}>
            <button className="hamburger-btn" onClick={() => setSidebarOpen(!sidebarOpen)}>
                <img src={hamburger} alt="Menu" className="hamburger-icon" />
            </button>
            <Sidebar activePage={activePage} onNavigate={onNavigate}/>
            <div className="main-wrapper">
                <Header />
                <main className="main-content">
                    {children}
                </main>
            </div>
        </div>
    );
}