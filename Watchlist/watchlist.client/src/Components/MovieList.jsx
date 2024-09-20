import React, { useState, useEffect } from 'react';
import './Lists.css';
import UpdateMovieModal from '../Modals/UpdateMovieModal';

const MovieList = ({ currentUser }) => {
    const [movies, setMovies] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [selectedMovie, setSelectedMovie] = useState(null); 
    const [isModalOpen, setIsModalOpen] = useState(false);    
    const [isUpdated, setIsUpdated] = useState(false); 

    useEffect(() => {
        const fetchMovies = async () => {
            try {
                const response = await fetch('/api/Movie');
                if (!response.ok) {
                    throw new Error('Failed to fetch movies');
                }
                const data = await response.json();
                setMovies(data);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchMovies();
    }, [isUpdated]);

    const handleUpdate = (movieId) => {
        const movieToUpdate = movies.find(movie => movie.id === movieId);
        setSelectedMovie(movieToUpdate);  
        setIsModalOpen(true);              
    };

    const handleModalClose = () => {
        setIsModalOpen(false);
        setSelectedMovie(null);          
    };

    const handleMovieUpdate = (updatedMovie) => {
        setMovies(prevMovies =>
            prevMovies.map(movie => movie.id === updatedMovie.id ? updatedMovie : movie)
        );
        setIsUpdated(prev => !prev);
        handleModalClose();               
    };

    if (loading) {
        return <div>Loading movies...</div>;
    }

    if (error) {
        return <div>Error: {error}</div>;
    }

    return (
        <div>
            {movies.length === 0 ? (
                <p>No movies found.</p>
            ) : (
                <ul className="media-card">
                    {movies.map((movie) => (
                        <li key={movie.id}>
                            <img src={movie.imageUrl} alt={movie.title} />
                            <h3>{movie.title}</h3>
                            <p>Director: {movie.directorName}</p>
                            <p>Genre: {movie.genre}</p>
                            <p>Release Year: {movie.releaseYear}</p>
                            <div className="button-group">
                                {currentUser && currentUser.role === 'Admin' && (
                                    <>
                                        <button
                                            className="delete-button"
                                            onClick={() => handleDelete(movie.id)}
                                        >Delete</button>
                                        <button
                                            className="update-button"
                                            onClick={() => handleUpdate(movie.id)}
                                        >Update</button>
                                    </>
                                )}
                                {currentUser && (
                                    <button
                                        className="add-button"
                                        onClick={() => handleAddToCollection(movie.id)}
                                    >Add to Collection</button>
                                )}
                            </div>
                        </li>
                    ))}
                </ul>
            )}

            {isModalOpen && selectedMovie && (
                <UpdateMovieModal
                    movie={selectedMovie}
                    onUpdate={handleMovieUpdate}  
                    onClose={handleModalClose}
                />
            )}
        </div>
    );
};

export default MovieList;
