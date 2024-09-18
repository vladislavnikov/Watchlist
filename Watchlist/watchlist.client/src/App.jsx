import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Login from './Pages/Login';
import Register from './Pages/Register';
import Home from './Pages/Home';
import Header from './Components/Header';
import Footer from './Components/Footer';
import './App.css';

const App = () => {
    const [currentUser, setCurrentUser] = useState(null);

    useEffect(() => {
        const user = localStorage.getItem('user');
        if (user) {
            setCurrentUser(JSON.parse(user));
        }
    }, currentUser);

    const handleLogout = () => {
        localStorage.removeItem('user');
        setCurrentUser(null);
        window.location.href = '/';
    };

    return (
        <Router>
            <Header currentUser={currentUser} onLogout={handleLogout} />
            <main>
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/login" element={<Login setCurrentUser={setCurrentUser} />} />
                    <Route path="/register" element={<Register />} />
                </Routes>
            </main>
            <Footer/>
        </Router>
    );
};

export default App;

