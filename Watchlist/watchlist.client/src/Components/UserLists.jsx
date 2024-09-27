import React, { useState, useEffect } from 'react';
import '../Components/Lists.css';

const Home = ({ currentUser }) => {
    const [movies, setMovies] = useState([]);
    const [shows, setShows] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [isUpdated, setIsUpdated] = useState(false);

    useEffect(() => {
        const fetchMovies = async () => {
            try {
                const token = currentUser.token;
                const response = await fetch('/api/Movie/usermovies', {
                    headers: { Authorization: `Bearer ${token}` }
                });

                if (!response.ok) {
                    throw new Error('Failed to fetch movies');
                }

                const data = await response.json();
                setMovies(data);
            } catch (err) {
                setError(err.message);
            }
        };

        const fetchShows = async () => {
            try {
                const token = currentUser.token;
                const response = await fetch('/api/Show/usershows', {
                    headers: { Authorization: `Bearer ${token}` }
                });

                if (!response.ok) {
                    throw new Error('Failed to fetch shows');
                }

                const data = await response.json();
                setShows(data);
            } catch (err) {
                setError(err.message);
            }
        };

        const fetchData = async () => {
            setLoading(true);
            await Promise.all([fetchMovies(), fetchShows()]);
            setLoading(false);
        };

        fetchData();
    }, [isUpdated]); 

    const handleRemoveMovie = async (movieId) => {
        try {
            const token = currentUser.token;

            const response = await fetch('/api/Movie/remove', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify(movieId),
            });

            if (!response.ok) {
                throw new Error('Failed remove movie from your collection!');
            }

            setIsUpdated(prev => !prev);
        } catch (err) {
            console.error('Error removing movie from your collection!:', err);
        }
    };

    const handleRemoveShow = async (showId) => {
        try {
            const token = currentUser.token;

            const response = await fetch('/api/Show/remove', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify(showId), 
            });

            if (!response.ok) {
                throw new Error('Failed remove show from your collection!');
            }

            setIsUpdated(prev => !prev);
        } catch (err) {
            console.error('Error removing show from your collection!:', err);
        }
    };

    if (loading) {
        return <p>Loading...</p>;
    }

    if (error) {
        return <p>Error: {error}</p>;
    }

    return (
        <>
            {!currentUser ? (
                <div>
                    <h1>Welcome to the Home Page</h1>
                    <p>This is the main page of the application.</p>
                </div>
            ) : (
                <div>
                    <h2>Your Movies</h2>
                    {movies.length === 0 ? <p>No movies found.</p> : (
                        <ul className="media-card">
                            {movies.map((movie) => (
                                <li key={movie.id}>
                                    <img src={movie.imageUrl} alt={movie.title} />
                                    <h3>{movie.title}</h3>
                                    <p>Director: {movie.directorName}</p>
                                    <p>Genre: {movie.genre}</p>
                                    <p>Release Year: {movie.releaseYear}</p>
                                    <div className="button-group">
                                        <button
                                            className="delete-button"
                                            onClick={() => handleRemoveMovie(movie.id)}
                                        >
                                            Remove from Collection
                                        </button>
                                    </div>
                                </li>
                            ))}
                        </ul>
                    )}

                    <h2>Your Shows</h2>
                    {shows.length === 0 ? <p>No shows found.</p> : (
                        <ul className="media-card">
                            {shows.map((show) => (
                                <li key={show.id}>
                                    <img src={show.imageUrl} alt={show.title} />
                                    <h3>{show.title}</h3>
                                    <p>Broadcaster: {show.directorName}</p>
                                    <p>Seasons: {show.seasons}</p>
                                    <p>Release Year: {show.releaseYear}</p>
                                    <div className="button-group">
                                        <button
                                            className="delete-button"
                                            onClick={() => handleRemoveShow(show.id)}
                                        >
                                            Remove from Collection
                                        </button>
                                    </div>
                                </li>
                            ))}
                        </ul>
                    )}
                </div>
            )}
        </>
    );
};

export default Home;
