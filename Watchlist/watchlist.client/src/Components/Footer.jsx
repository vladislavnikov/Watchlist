import React from 'react';
import './Footer.css';

function Footer() {

    const d = new Date();
    let year = d.getFullYear();

    return (
        <footer>
            <p>©{year} Watchlist App. All rights reserved.</p>
        </footer>
    );
};

export default Footer;