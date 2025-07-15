import { useEffect, useState, useRef } from "react";
import { formatPostDate } from '../utils/date-helper';
import { PostURL } from "../URLS";
import { CommentsURL, MainURL } from "../URLS";
import { authKey } from "../rtk/User-Slice";
import { useDispatch, useSelector } from "react-redux";
import "./styles/PostDetailsContainer.css";
import { Link } from 'react-router-dom';

import { getAccessToken } from '../utils/auth';

function PostDetailsContainer(props) {
  console.log(props.post)
  const user = useSelector(state => state.user);
  const [post, setPost] = useState(props.post);
  const [comments, setComments] = useState([]);
  
  const [isLiked, setIsLiked] = useState(post.isLikedByCurrentUser);
  const [likeCount, setLikeCount] = useState(props.post.likeCount);
  const [commentCount, setCommentCount] = useState(props.post.commentCount);
  const [newComment, setNewComment] = useState('');
  const [showMenu, setShowMenu] = useState(false);
  
  const menuRef = useRef(null);
  const commentsEndRef = useRef(null);

  useEffect(() => {
    const handleClickOutside = (event) => {
      if (menuRef.current && !menuRef.current.contains(event.target)) {
        setShowMenu(false);
      }
    };

    if (showMenu) {
      document.addEventListener('mousedown', handleClickOutside);
    }

    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, [showMenu]);

  useEffect(()=>
    {
      const fetchData = async () => {
        await fetchComments();
      };
    
      fetchData();   
    }
    , []);

  useEffect(() => {
    commentsEndRef.current?.scrollIntoView({ behavior: 'smooth' });
  }, [comments]);

  const fetchComments = async () => {
    try {
      const res = await fetch(CommentsURL+ "/" + post.id, {
        headers: {
          Authorization: "Bearer " + getAccessToken(),
        }
      });
  
      if (!res.ok) throw new Error(`HTTP error ${res.status}`);
      const data = await res.json();
      setComments(data);
    } catch (err) {
      console.error("Failed to fetch comments:", err.message);
    }
  }

  const handleLike = () => {
    setIsLiked(prev => !prev);
    setLikeCount(prev => (isLiked ? prev - 1 : prev + 1));
  };

  const handleAddComment = async () => {
    if (!newComment.trim()) return;
  
    const comment = {
      content: newComment,
      postId: post.id,
    };
  
    try {
      const res = await fetch(MainURL + '/api/Comment', {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization:  "Bearer " + getAccessToken() ,
        },
        body: JSON.stringify(comment),
      });
      
      if (!res.ok) throw new Error(`HTTP error ${res.status}`);
  
      const savedComment = await res.json();
  
      await fetchComments();
      
      setNewComment('');
      
      // Update comment count
      setCommentCount(prev => prev + 1);
      
      // Also update the post object to keep everything in sync
      setPost(prev => ({
        ...prev,
        commentCount: prev.commentCount + 1
      }));
      
    } catch (err) {
      console.error("Failed to post comment:", err.message);
    }
  };     
  const handleKeyPress = (e) => {
    if (e.key === 'Enter' && !e.shiftKey) {
      e.preventDefault();
      handleAddComment();
    }
  };

  const handleCopyLink = () => {
    const postUrl = `${window.location.origin}/PostDetails/${post.id}`;
    navigator.clipboard.writeText(postUrl);
    setShowMenu(false);
  };

  return (
    <div className="post-details-container">
      <div className="post-details-wrapper">
        {/* Left Side - Media */}
        <div className="post-media-section">
          {post.mediaUrl && post.mediaUrl !== "N/A" && (
            <img
              src={MainURL + post.mediaUrl}
              alt="Post media"
              className="post-media-image"
            />
          )}
          
          {/* Close button */}
          <button
            onClick={() => window.history.back()}
            className="close-button"
          >
            ×
          </button>
        </div>

        {/* Right Side - Info & Comments */}
        <div className="post-info-section">
          {/* Post Header */}
          <div className="post-header">
            <Link to={`/userdetails/${post.userId}`} className="flex items-center space-x-3"
            style={{textDecoration:"None"}}>
              <img
                src={MainURL + post.authorImage}
                alt={post.authorUsername}
                className="post-author-avatar"
              />
              <div className="post-author-info">
                <h3 className="post-author-name">
                  {post.authorUsername}
                </h3>
                <p className="post-date">
                  {formatPostDate(post.createdAt)}
                </p>
              </div>
            </Link>
            {/* Menu */}
            <div className="menu-container" ref={menuRef}>
              <button
                onClick={() => setShowMenu(!showMenu)}
                className="menu-button"
              >
                ⋯
              </button>
              {showMenu && (
                <div className="menu-dropdown">
                  <button
                    onClick={handleCopyLink}
                    className="menu-item"
                  >
                    Copy Link
                  </button>
                </div>
              )}
            </div>
          </div>

          {/* Post Content */}
          <div className="post-content">
            <p className="post-content-text">
              {post.content}
            </p>
          </div>

          {/* Post Actions */}
          <div className="post-actions">
            <div className="post-actions-container">
              <button
                onClick={handleLike}
                className={`like-button ${isLiked ? 'liked' : 'not-liked'}`}
              >
                <span className="like-icon">
                  {isLiked ? '❤️' : '♡'}
                </span>
                {likeCount}
              </button>
              
              <div className="comment-count">
                <span className="comment-icon">💬</span>
                {commentCount}
              </div>
            </div>
          </div>

          {/* Comments Section */}
          <div className="comments-section-main">
            <div className="comments-list">
              {comments.map((comment) => (
                <div key={comment.id} className="comment-item">
                  <img
                    src={MainURL + comment.userImageUrl}
                    alt={comment.userName}
                    className="comment-avatar"
                  />
                  <div className="comment-content">
                    <p className="comment-text">
                      <span className="comment-author">
                        {comment.userName}
                      </span>{' '}
                      {comment.content}
                    </p>
                    <p className="comment-date">
                      {formatPostDate(comment.createdAt)}
                    </p>
                  </div>
                </div>
              ))}
              <div ref={commentsEndRef} />
            </div>


          </div>
          {/* Add Comment */}
          <div className="add-comment-section">
              <img
                src={MainURL + user.profilePictureUrl}
                alt="You"
                className="add-comment-avatar"
              />
              <div className="add-comment-input-container">
                <textarea
                  placeholder="Write a comment..."
                  value={newComment}
                  onChange={(e) => setNewComment(e.target.value)}
                  onKeyPress={handleKeyPress}
                  className="comment-textarea"
                  rows={1}
                />
                <button
                  onClick={handleAddComment}
                  disabled={!newComment.trim()}
                  className={`send-comment-button ${newComment.trim() ? 'enabled' : 'disabled'}`}
                >
                  ➤
                </button>
              </div>
            </div>
        </div>
      </div>
    </div>
  );
}

export default PostDetailsContainer;