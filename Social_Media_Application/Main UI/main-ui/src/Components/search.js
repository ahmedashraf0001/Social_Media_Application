import React, { useState, useEffect, useRef } from 'react';
import './styles/SearchComponent.css';
import './styles/navbar.css';
import { Search } from 'lucide-react';
import { MainURL } from '../URLS';
import { useNavigate } from 'react-router-dom';

const SearchComponent = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const [searchResults, setSearchResults] = useState({
    users: [],
    posts: [],
    loading: false,
    error: null
  });
  const [showResults, setShowResults] = useState(false);
  const searchRef = useRef(null);
  const resultsRef = useRef(null);

  useEffect(() => {
    const handleClickOutside = (event) => {
      if (searchRef.current && !searchRef.current.contains(event.target)) {
        setShowResults(false);
      }
    };
    document.addEventListener('mousedown', handleClickOutside);
    return () => document.removeEventListener('mousedown', handleClickOutside);
  }, []);

  const performSearch = async (keyword) => {
    if (!keyword.trim()) {
      setSearchResults({ users: [], posts: [], loading: false, error: null });
      setShowResults(false);
      return;
    }

    setSearchResults(prev => ({ ...prev, loading: true, error: null }));
    setShowResults(true);

    try {
      const [usersResponse, postsResponse] = await Promise.all([
        fetch(`https://localhost:7242/api/User/search?query=${encodeURIComponent(keyword)}&pageNumber=1&pageSize=12`),
        fetch(`https://localhost:7242/api/Post/Search?Keyword=${encodeURIComponent(keyword)}&pageNumber=1&pageSize=12`)
      ]);

      const users = usersResponse.ok ? await usersResponse.json() : [];
      const posts = postsResponse.ok ? await postsResponse.json() : [];

      setSearchResults({ users, posts, loading: false, error: null });
    } catch (error) {
      setSearchResults(prev => ({
        ...prev,
        loading: false,
        error: 'Search failed. Please try again.'
      }));
    }
  };

  const navigate = useNavigate();

  useEffect(() => {
    const delayedSearch = setTimeout(() => {
      performSearch(searchTerm);
    }, 300);
    return () => clearTimeout(delayedSearch);
  }, [searchTerm]);

  const handleInputChange = (e) => {
    setSearchTerm(e.target.value);
  };

  const handleUserClick = (userId) => {
    navigate(`/userdetails/${userId}`);
    setShowResults(false);
  };

  const handlePostClick = (postId) => {
    navigate(`/postdetails/${postId}`);
    setShowResults(false);
  };

  const formatDate = (dateString) => new Date(dateString).toLocaleDateString();

  const getMediaTypeIcon = (mediaType) => {
    switch (mediaType) {
      case 1: return '🖼️';
      case 2: return '🎥';
      default: return '📝';
    }
  };

  return (
    <div className="search-container-search" ref={searchRef}>
      <div className="navbar-search-container-search">
        <div className="navbar-search-wrapper-search">
          <Search className="navbar-search-icon-search" size={20} />
          <input
            type="text"
            placeholder="Search users and posts..."
            value={searchTerm}
            onChange={handleInputChange}
            className="search-input-search"
          />
        </div>
      </div>

      {showResults && (
        <div ref={resultsRef} className="search-results-search">
          {searchResults.loading && (
            <div className="loading-container-search">
              <div className="loading-spinner-search"></div>
              <span>Searching...</span>
            </div>
          )}

          {searchResults.error && (
            <div className="error-container-search">
              {searchResults.error}
            </div>
          )}

          {!searchResults.loading && !searchResults.error && (
            <>
              {searchResults.users.length > 0 && (
                <div className="results-section-search">
                  <div className="section-header-search">
                    <span className="section-icon-search">👤</span>
                    Users
                  </div>
                  {searchResults.users.map((user) => (
                    <div
                      key={user.id}
                      onClick={() => handleUserClick(user.id)}
                      className="result-item-search user-item-search"
                    >
                      <div className="user-avatar-search">
                        {MainURL + user.profilePictureUrl ? (
                          <img
                            src={MainURL + user.profilePictureUrl}
                            alt={user.username}
                            className="avatar-image-search"
                          />
                        ) : (
                          <div className="avatar-placeholder-search">
                            <span>👤</span>
                          </div>
                        )}
                      </div>
                      <div className="user-info-search">
                        <p className="user-name-search">
                          {user.firstName} {user.lastName}
                        </p>
                        <p className="username-search">@{user.username}</p>
                        <div className="user-stats-search">
                          <span>{user.followersCount} followers</span>
                          <span className="separator-search">•</span>
                          <span>Joined {formatDate(user.joinedIn)}</span>
                        </div>
                      </div>
                    </div>
                  ))}
                </div>
              )}

              {searchResults.posts.length > 0 && (
                <div className="results-section-search">
                  <div className="section-header-search">
                    <span className="section-icon-search">📄</span>
                    Posts
                  </div>
                  {searchResults.posts.map((post) => (
                    <div
                      key={post.id}
                      onClick={() => handlePostClick(post.id)}
                      className="result-item-search post-item-search"
                    >
                      <div className="post-content-search">
                        <div className="post-author-search">
                          {post.authorImage ? (
                            <img
                              src={post.authorImage}
                              alt={post.authorUsername}
                              className="author-avatar-search"
                            />
                          ) : (
                            <div className="author-avatar-placeholder-search">
                              <span>👤</span>
                            </div>
                          )}
                        </div>
                        <div className="post-details-search">
                          <div className="post-header-search">
                            <span className="author-username-search">@{post.authorUsername}</span>
                            <span className="post-date-search">{formatDate(post.createdAt)}</span>
                            <span className="media-type-search">{getMediaTypeIcon(post.mediaType)}</span>
                          </div>
                          <p className="post-text-search">{post.content}</p>
                          <div className="post-stats-search">
                            <div className="stat-item-search">
                              <span className="stat-icon-search">❤️</span>
                              <span>{post.likeCount}</span>
                            </div>
                            <div className="stat-item-search">
                              <span className="stat-icon-search">💬</span>
                              <span>{post.commentCount}</span>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  ))}
                </div>
              )}

              {!searchResults.loading &&
                searchResults.users.length === 0 &&
                searchResults.posts.length === 0 &&
                searchTerm.trim() && (
                  <div className="no-results-search">
                    No results found for "{searchTerm}"
                  </div>
                )}
            </>
          )}
        </div>
      )}
    </div>
  );
};

export default SearchComponent;
