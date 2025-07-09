import React from 'react';
import './styles/FailedToLoad.css'; // Import the CSS

const FailedToLoad = ({ message = "Failed to load content.", onRetry }) => {
  return (
    <div className="error-box">
      <div className="error-icon">⚠️</div>
      <h2>Error</h2>
      <p>{message}</p>
      {onRetry && <button onClick={onRetry}>Retry</button>}
    </div>
  );
};

export default FailedToLoad;
