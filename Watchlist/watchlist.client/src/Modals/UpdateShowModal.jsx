import React, { useState } from 'react';
import './Modals.css';

const UpdateShowModal = ({ show, onUpdate, onClose }) => {
    const [formData, setFormData] = useState({
        id: show.id || "",
        title: show.title || "",
        description: show.description || "",
        directorId: show.directorId || "",
        releaseYear: show.releaseYear || "",
        seasons: show.seasons || "",
        imageUrl: show.imageUrl || ""
    });

    const [error, setError] = useState(null);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData({
            ...formData,
            [name]: value
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError(null);

        try {
            const response = await fetch(`/api/Show/${formData.id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(formData)
            });

            if (response.status === 204) {
                if (onUpdate) {
                    onUpdate(formData);
                }
            } else if (response.status === 404) {
                setError('Show not found.');
            } else {
                setError('Failed to update the show.');
            }
        } catch (error) {
            setError('An error occurred while updating the show.');
        }
    };

    return (
        <div className="modal">
            <div onClick={onClose} className="overlay"></div>
            <div className="modal-content">
                <h2>Update Show</h2>
                <form onSubmit={handleSubmit}>
                    <div>
                        <label>Title</label>
                        <input
                            type="text"
                            name="title"
                            value={formData.title}
                            onChange={handleInputChange}
                            required
                        />
                    </div>
                    <div>
                        <label>Description</label>
                        <input
                            type="text"
                            name="description"
                            value={formData.description}
                            onChange={handleInputChange}
                            required
                        />
                    </div>
                    <div>
                        <label>Broadcaster</label>
                        <input
                            type="number"
                            name="directorId"
                            value={formData.directorId}
                            onChange={handleInputChange}
                            required
                        />
                    </div>
                    <div>
                        <label>Release Year</label>
                        <input
                            type="number"
                            name="releaseYear"
                            value={formData.releaseYear}
                            onChange={handleInputChange}
                            required
                        />
                    </div>
                    <div>
                        <label>Seasons</label>
                        <input
                            type="number"
                            name="seasons"
                            value={formData.seasons}
                            onChange={handleInputChange}
                            required
                        />
                    </div>
                    <div>
                        <label>Image URL</label>
                        <input
                            type="text"
                            name="imageUrl"
                            value={formData.imageUrl}
                            onChange={handleInputChange}
                            required
                        />
                    </div>
                    {error && <p className="error">{error}</p>}
                    <button className="submit-modal" type="submit">Update</button>
                </form>
                <button className="close-modal" onClick={onClose}>
                    CLOSE
                </button>
            </div>
        </div>
    );
};

export default UpdateShowModal;
