import React from 'react';
import { Link } from 'react-router-dom';
import './styles/SocialPostInterface.css';
import { MainURL } from '../URLS';

export default function SocialPostInterface(props) {
  const user  = props.user;
  
  return (
    <div className="post-container-interface">
      {/* Header with profile and input */}
      <div className="post-header">
        {user.profilePictureUrl ? (
          <img 
          src={MainURL + user.profilePictureUrl}
          alt="Profile" 
          className="profile-image"
        />
        ) : (
          <div className="avatar-placeholder-search">
            <span>👤</span>
          </div>
        )}
        <div className="input-container">
          <Link to="/create-post">
            <input
              type="text"
              placeholder="Start a post"
              className="post-input"
              readOnly
            />
          </Link>
        </div>
      </div>

      {/* Action links */}
      <div className="action-links">
        <Link to="/create-post" className="action-link video-link">
          <svg className="action-icon" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M15 10l4.553-2.276A1 1 0 0121 8.618v6.764a1 1 0 01-1.447.894L15 14M5 18h8a2 2 0 002-2V8a2 2 0 00-2-2H5a2 2 0 00-2 2v8a2 2 0 002 2z" />
          </svg>
          <span className="action-text">Video</span>
        </Link>
        
        <Link to="/create-post" className="action-link photo-link">
          <svg className="action-icon" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 16l4.586-4.586a2 2 0 012.828 0L16 16m-2-2l1.586-1.586a2 2 0 012.828 0L20 14m-6-6h.01M6 20h12a2 2 0 002-2V6a2 2 0 00-2-2H6a2 2 0 00-2 2v12a2 2 0 002 2z" />
          </svg>
          <span className="action-text">Photo</span>
        </Link>
        
        <Link to="/create-post" className="action-link article-link">
          <svg className="action-icon" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
          </svg>
          <span className="action-text">Write article</span>
        </Link>
      </div>
    </div>
  );
}