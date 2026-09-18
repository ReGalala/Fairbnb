import headerLeft from '../assets/headerLeftSide.png';
import headerMiddle from '../assets/headerMiddleSide.png';
import headerRight from '../assets/headerRightSide.png';
import searchbar from '../assets/searchbar.png';
import settingbutton from '../assets/settingbutton.png';
import notificationButton from '../assets/notificationButton.png';
import fotoframe from '../assets/fotoframe.png';

export default function Header() {
    return (
        <header className="header">
            <div className="header-bg">
                <img src={headerLeft} alt="" className="header-bg-left" />
                {Array.from({ length: 10 }).map((_, i) => (
                    <img key={i} src={headerMiddle} alt="" className="header-bg-middle" />
                ))}
                <img src={headerRight} alt="" className="header-bg-right" />
            </div>
            <div className="header-content">
                <img src={searchbar} alt="Search" className="header-searchbar" />
                <div className="header-right">
                    <img src={settingbutton} alt="Settings" className="header-icon-settings" />
                    <img src={notificationButton} alt="Notifications" className="header-icon-notification" />
                    <div className="header-profile">
                        <img src={fotoframe} alt="Profile" className="header-profile-pic" />
                        <div className="header-profile-info">
                            <span className="header-profile-name">User Name</span>
                            <span className="header-profile-role">Admin</span>
                        </div>
                    </div>
                </div>
            </div>
        </header>
    );
}
