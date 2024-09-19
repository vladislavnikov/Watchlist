import React, { useState, useEffect } from 'react';
import './Lists.css'

const ShowList = () => {
    const [shows, setShows] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchShows = async () => {
            try {
                const response = await fetch('/api/Show');
                if (!response.ok) {
                    throw new Error('Failed to fetch shows');
                }
                const data = await response.json();
                setShows(data);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchShows();
    }, []);

    if (loading) {
        return <div>Loading shows...</div>;
    }

    if (error) {
        return <div>Error: {error}</div>;
    }

    return (
        <div>
            {shows.length === 0 ? (
                <p>No shows found.</p>
            ) : (
                    <ul className="media-card">
                    {shows.map((show) => (
                        <li key={show.id}>
                            <img src={show.imageUrl}></img>
                            <h3>{show.title}</h3>
                            <p>Broadcaster: {show.directorName}</p>
                            <p>Seasons: {show.seasons}</p>
                            <p>Release Year: {show.releaseYear}</p>
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
};

export default ShowList;