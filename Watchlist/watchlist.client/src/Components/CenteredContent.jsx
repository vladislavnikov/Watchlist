import React from 'react';
import { useLocation } from 'react-router-dom';

const CenteredContent = ({ children }) => {
    const location = useLocation();
    const isCenteredPage = ["/login", "/register", "/"].includes(location.pathname);

    return (
        <main className={isCenteredPage ? 'center-align' : ''}>
            {children}
        </main>
    );
};

export default CenteredContent;
