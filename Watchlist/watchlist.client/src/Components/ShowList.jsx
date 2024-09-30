import React, { useEffect, useState } from 'react';
import './Lists.css';
import InfoShowModal from '../Modals/InfoShowModal';
import UpdateShowModal from '../Modals/UpdateShowModal';

const ShowList = ({ currentUser }) => {
    const [shows, setShows] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [selectedShow, setSelectedShow] = useState(null);
    const [isInfoModalOpen, setIsInfoModalOpen] = useState(false);
    const [isUpdateModalOpen, setIsUpdateModalOpen] = useState(false);
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

    const handleCardClick = (showId) => {
        setSelectedShow(showId);
        setIsInfoModalOpen(true);  
    };

    const handleInfoModalClose = () => {
        setIsInfoModalOpen(false);
        setSelectedShow(null);
    };

    const handleUpdateClick = (e, showId) => {
        e.stopPropagation();  
        const showToUpdate = shows.find(show => show.id === showId);
        setSelectedShow(showToUpdate);
        setIsUpdateModalOpen(true);
    };

    const handleUpdateModalClose = () => {
        setIsUpdateModalOpen(false);
        setSelectedShow(null);
    };

    const handleShowUpdate = (updatedShow) => {
        setShows(prevShows =>
            prevShows.map(show => show.id === updatedShow.id ? updatedShow : show)
        );
        setIsUpdated(prev => !prev);
        handleUpdateModalClose();  
    };

    const handleDelete = async (e, showId) => {
        e.stopPropagation();  
        try {
            const token = currentUser?.token;
            const response = await fetch(`/api/Show/${showId}`, {
                method: 'DELETE',
                headers: {
                    'Authorization': `Bearer ${token}`,
                },
            });
            if (!response.ok) {
                throw new Error('Failed to delete show');
            }
            setShows(shows.filter(show => show.id !== showId));
            alert('Show deleted!');
        } catch (err) {
            console.error(err);
            setError('Failed to delete show');
        }
    };

    const handleAddToCollection = async (e,showId) => {
        e.stopPropagation();  
        try {
            const token = currentUser.token;

            const response = await fetch('/api/Show/add', {
                method: 'POST',
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(showId),
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
                        <li key={show.id} onClick={() => handleCardClick(show.id)}>
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
                                            onClick={(e) => handleDelete(e, show.id)}
                                        >Delete</button>
                                        <button
                                            className="update-button"
                                            onClick={(e) => handleUpdateClick(e, show.id)}
                                        >Update</button>
                                    </>
                                )}
                                {currentUser && (
                                    <button
                                        className="add-button"
                                        onClick={(e) => handleAddToCollection(e, show.id)}
                                    >Add to Collection</button>
                                )}
                            </div>
                        </li>
                    ))}
                </ul>
            )}

            {isInfoModalOpen && selectedShow && (
                <InfoShowModal
                    showId={selectedShow}
                    onClose={handleInfoModalClose}
                />
            )}

            {isUpdateModalOpen && selectedShow && (
                <UpdateShowModal
                    show={selectedShow}
                    onUpdate={handleShowUpdate}
                    onClose={handleUpdateModalClose}
                />
            )}
        </div>
    );
};

export default ShowList;
