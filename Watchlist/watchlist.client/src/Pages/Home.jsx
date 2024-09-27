import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import '../Components/Lists.css';

const Home = ({ currentUser }) => {
    const navigate = useNavigate();

    useEffect(() => {
        if (currentUser) {
            navigate('/saved');
        }
    }, [currentUser, navigate]); 

    return (
        <>
                <div>
                    <h1>Welcome to the Home Page</h1>
                    <p>This is the main page of the application.</p>
                </div>
        </>
    );
};

export default Home;
