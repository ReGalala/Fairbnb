import menuWithButtons from '../assets/menuWithButtons.png';
import dashboardHighlighted from '../assets/dashboardHighlighted.png';
import unitsHighlighted from '../assets/unitsHighlighted.png';
import membersHighlighted from '../assets/membersHighlighted.png';
import expensesHighlighted from '../assets/expensesHighlighted.png';
import settlementsHighlighted from '../assets/settlementsHighlighted.png';
import incomeHighlighted from '../assets/incomeHighlighted.png';
import paymentsHighlighted from '../assets/paymentsHighlighted.png';
import reportsHighlighted from '../assets/reportsHighlighted.png';
import settingsHighlighted from '../assets/settingsHighlighted.png';

const menuItems = [
      { name: 'Dashboard', activeIcon: dashboardHighlighted },
      { name: 'Units', activeIcon: unitsHighlighted },
      { name: 'Members', activeIcon: membersHighlighted },
      { name: 'Expenses', activeIcon: expensesHighlighted },
      { name: 'Settlements', activeIcon: settlementsHighlighted },
      { name: 'Income', activeIcon: incomeHighlighted },
      { name: 'Payments', activeIcon: paymentsHighlighted },
      { name: 'Reports', activeIcon: reportsHighlighted },
      { name: 'Settings', activeIcon: settingsHighlighted },
  ];

interface SidebarProps {
    activePage: string;
    onNavigate: (page: string) => void;
}

export default function Sidebar({ activePage, onNavigate }: SidebarProps) {
    return (
        <nav className="sidebar">
            <img src={menuWithButtons} alt="Menu" className="sidebar-bg" />
            <div className="sidebar-menu">
                {menuItems.map((item) => (
                    <div
                        key={item.name}
                        className="sidebar-menu-item"
                        onClick={() => onNavigate(item.name)}
                    >
                        {activePage === item.name && (
                            <img src={item.activeIcon} alt={item.name} className="sidebar-highlight" />
                        )}
                    </div>
                ))}
            </div>
        </nav>
    );
}
