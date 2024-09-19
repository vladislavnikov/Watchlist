import React, { useState, useEffect } from 'react';
import './Lists.css'

const MovieList = () => {
    const [movies, setMovies] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

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
    }, []);

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
                            <img src={movie.imageUrl}></img>
                            <h3>{movie.title}</h3>
                            <p>Director: {movie.directorName}</p>
                            <p>Genre: {movie.genre}</p>
                            <p>Release Year: {movie.releaseYear}</p>
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
};

export default MovieList;