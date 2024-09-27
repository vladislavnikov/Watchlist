import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import './Header.css';

const Header = ({ currentUser, onLogout }) => {
    const navigate = useNavigate();

    return (
        <header>
            <nav>
                <div className="nav-left">
                    {currentUser ? (
                        <div className="logo" onClick={() => navigate('/saved')}>Watchlist</div>
                    ) : (
                        <div className="logo" onClick={() => navigate('/')}>Watchlist</div>
                    )}
                    <p onClick={() => navigate('/movies')}>Movies</p>
                    <p onClick={() => navigate('/shows')}>Shows</p>
                </div>
                <ul className="nav-right nav-links">
                    {!currentUser ? (
                        <>
                            <li><Link to="/login">Login</Link></li>
                            <li><Link to="/register">Register</Link></li>
                        </>
                    ) : (
                        <>
                            <li>Hello, {currentUser.username}</li>
                            <li><a href="#" onClick={onLogout}>Logout</a></li>
                        </>
                    )}
                </ul>
            </nav>
        </header>
    );
};

export default Header;
