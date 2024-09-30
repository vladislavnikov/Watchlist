import React, { useState, useEffect } from 'react';
import './InfoModals.css'; 

const InfoMovieModal = ({ movieId, onClose }) => {
    const [movie, setMovie] = useState(null);
    const [error, setError] = useState(null);
    const [isLoading, setLoading] = useState(true);

    useEffect(() => {
        const fetchMovie = async () => {
            try {
                const response = await fetch(`/api/Movie/${movieId}`);
                if (!response.ok) {
                    throw new Error('Failed to fetch the movie details');
                }
                const data = await response.json();
                setMovie(data);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        if (movieId) {
            fetchMovie();
        }
    }, [movieId]);

    const handleOverlayClick = (e) => {
        if (e.target.className === 'modal-overlay') {
            onClose();
        }
    };

    
    if (!movie) {
        return null;
    }


    return (
        <div className="modal-overlay" onClick={handleOverlayClick}>
            <div className="modal-content">
                <button className="close-modal" onClick={onClose}>X</button>
                <div className="modal-body">
                    <div className="image-wrapper">
                        <img src={movie.imageUrl} alt={movie.title} className="modal-image" />
                    </div>
                    <div className="text-content">
                        <h2>{movie.title}</h2>
                        <p><strong>Description:</strong> {movie.description}</p>
                        <p><strong>Director:</strong> {movie.directorName}</p>
                        <p><strong>Release Year:</strong> {movie.releaseYear}</p>
                        <p><strong>Duration (Minutes):</strong> {movie.durationMins}</p>
                        <p><strong>Genre:</strong> {movie.genre}</p>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default InfoMovieModal;
