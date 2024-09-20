import React, { useState, useEffect } from 'react';
import './Lists.css'
import UpdateShowModal from '../Modals/UpdateShowModal';

const ShowList = ({ currentUser }) => {
    const [shows, setShows] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [selectedShow, setSelectedShow] = useState(null);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [isUpdated, setIsUpdated] = useState(false); 

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
    }, [isUpdated]);  

    const handleUpdate = (showId) => {
        const showToUpdate = shows.find(show => show.id === showId);
        setSelectedShow(showToUpdate);
        setIsModalOpen(true);
    };

    const handleModalClose = () => {
        setIsModalOpen(false);
        setSelectedShow(null);
    };

    const handleShowUpdate = (updatedShow) => {
        setShows(prevShows =>
            prevShows.map(show => show.id === updatedShow.id ? updatedShow : show)
        );
        setIsUpdated(prev => !prev); 
        handleModalClose();
    };

    if (loading) {
        return <div>Loading shows...</div>;
    }

    if (error) {
        return <div>Error: {error}</div>;
    }

    const handleDelete = async (showId) => {
        try {
            const response = await fetch(`/api/Show/${showId}`, {
                method: 'DELETE',
            });
            if (!response.ok) {
                throw new Error('Failed to delete show');
            }
            setShows(shows.filter(show => show.id !== showId));
        } catch (err) {
            console.error(err);
            setError('Failed to delete show');
        }
    };

    const handleAddToCollection = async (showId) => {
        try {
            const response = await fetch('/api/Show/add', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ showId }),
            });
            if (!response.ok) {
                throw new Error('Failed to add show to collection');
            }
            alert('Show added to your collection!');
        } catch (err) {
            console.error(err);
            setError('Failed to add show to collection');
        }
    };

    return (
        <div>
            {shows.length === 0 ? (
                <p>No shows found.</p>
            ) : (
                <ul className="media-card">
                    {shows.map((show) => (
                        <li key={show.id}>
                            <img src={show.imageUrl} alt={show.title} />
                            <h3>{show.title}</h3>
                            <p>Broadcaster: {show.directorName}</p>
                            <p>Seasons: {show.seasons}</p>
                            <p>Release Year: {show.releaseYear}</p>
                            <div className="button-group">
                                {currentUser && currentUser.role === 'Admin' && (
                                    <>
                                        <button
                                            className="delete-button"
                                            onClick={() => handleDelete(show.id)}
                                        >Delete</button>
                                        <button
                                            className="update-button"
                                            onClick={() => handleUpdate(show.id)}
                                        >Update</button>
                                    </>
                                )}
                                {currentUser && (
                                    <button
                                        className="add-button"
                                        onClick={() => handleAddToCollection(show.id)}
                                    >Add to Collection</button>
                                )}
                            </div>
                        </li>
                    ))}
                </ul>
            )}

            {isModalOpen && selectedShow && (
                <UpdateShowModal
                    show={selectedShow}
                    onUpdate={handleShowUpdate}
                    onClose={handleModalClose}
                />
            )}

        </div>
    );
};


export default ShowList;
