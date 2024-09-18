import React, { useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import './Header.css';

const Header = ({ currentUser, onLogout }) => {
    const navigate = useNavigate();

    const handleLogoClick = () => {
        navigate('/');
    };

    return (
        <header>
            <nav>
                <div className="logo" onClick={handleLogoClick}>Watchlist</div>
                <ul className="nav-links">
                    {!currentUser ? (
                        <>
                            <li><Link to="/login">Login</Link></li>
                            <li><Link to="/register">Register</Link></li>
                        </>
                    ) : (
                        <>
                            <li>Hello, {currentUser.username} ({currentUser.role})</li>
                            <li><a href="#" onClick={onLogout}>Logout</a></li>
                        </>
                    )}
                </ul>
            </nav>
        </header>
    );
};

export default Header;
