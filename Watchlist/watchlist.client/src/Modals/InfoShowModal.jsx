import React, { useState, useEffect } from 'react';
import './InfoModals.css'; 

const InfoShowModal = ({ showId, onClose }) => {
    const [show, setShow] = useState(null);
    const [error, setError] = useState(null);
    const [isLoading, setLoading] = useState(true);

    useEffect(() => {
        const fetchShow = async () => {
            try {
                const response = await fetch(`/api/Show/${showId}`);
                if (!response.ok) {
                    throw new Error('Failed to fetch the show details');
                }
                const data = await response.json();
                setShow(data);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        if (showId) {
            fetchShow();
        }
    }, [showId]);

    const handleOverlayClick = (e) => {
        if (e.target.className === 'modal-overlay') {
            onClose();
        }
    };


    if (!show) {
        return null;
    }


    return (
        <div className="modal-overlay" onClick={handleOverlayClick}>
            <div className="modal-content">
                <button className="close-modal" onClick={onClose}>X</button>
                <div className="modal-body">
                    <div className="image-wrapper">
                        <img src={show.imageUrl} alt={show.title} className="modal-image" />
                    </div>
                    <div className="text-content">
                        <h2>{show.title}</h2>
                        <p><strong>Description:</strong> {show.description}</p>
                        <p><strong>Director:</strong> {show.directorName}</p>
                        <p><strong>Release Year:</strong> {show.releaseYear}</p>
                        <p><strong>Seasons:</strong> {show.seasons}</p>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default InfoShowModal;
