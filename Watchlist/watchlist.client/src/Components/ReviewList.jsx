import React, { useState } from 'react';
import '../Modals/InfoModals.css';

const ReviewList = ({ movieId }) => {
    const [reviews, setReviews] = useState([]);
    const [newReview, setNewReview] = useState('');

    useEffect(() => {
        const fetchRevies = async () => {
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
            fetchRevies();
        }
    }, [movieId]);

    const handleReviewSubmit = (e) => {
        e.preventDefault();
        if (newReview.trim()) {
            const newReviewObj = { id: reviews.length + 1, content: newReview };
            setReviews([...reviews, newReviewObj]);
            setNewReview(''); 
        }
    };

    return (
        <div className="review-list-container">
            <h3>Reviews</h3>
            <div className="reviews-scroll">
                {reviews.map(review => (
                    <p key={review.id} className="review-item">{review.content}</p>
                ))}
            </div>
            <form onSubmit={handleReviewSubmit} className="review-form">
                <textarea
                    value={newReview}
                    onChange={(e) => setNewReview(e.target.value)}
                    placeholder="Write your review here..."
                    className="review-input"
                    required
                />
                <button type="submit" className="submit-review-btn">Submit Review</button>
            </form>
        </div>
    );
};

export default ReviewList;
