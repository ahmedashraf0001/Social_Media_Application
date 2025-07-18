import { useEffect, useState, useRef } from "react";
import "./styles/post.css";
import { formatPostDate } from "../utils/date-helper";
import { PostURL } from "../URLS";
import { useSelector } from "react-redux";
import { CommentsURL, MainURL } from "../URLS";
import { Link } from "react-router-dom";
import { getAccessToken } from "../utils/auth";

function Post(props) {
  const user = useSelector((state) => state.user);
  const [post, setPost] = useState(props.post);
  const [comments, setComments] = useState([]);
  const [isLiked, setIsLiked] = useState(props.post.isLikedByCurrentUser);
  const [likeCount, setLikeCount] = useState(props.post.likeCount);
  const [commentCount, setCommentCount] = useState(props.post.commentCount);
  const [newComment, setNewComment] = useState("");
  const [showComments, setShowComments] = useState(props.details);
  const [showMenu, setShowMenu] = useState(false);

  const menuRef = useRef(null);

  useEffect(() => {
    const handleClickOutside = (event) => {
      if (menuRef.current && !menuRef.current.contains(event.target)) {
        setShowMenu(false);
      }
    };

    if (showMenu) {
      document.addEventListener("mousedown", handleClickOutside);
    }

    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, [showMenu]);
  useEffect(() => {
    if (props.details) {
      toggleComments();
    }
  }, []);

  const handleLike = async () => {
    try {
      const res = await fetch(`${PostURL}/Like/${post.id}`, {
        method: "PUT",
        headers: {
          Authorization: "Bearer " + getAccessToken(),
          "Content-Type": "application/json",
        },
      });

      if (!res.ok) throw new Error(`HTTP error ${res.status}`);
      const data = await res.json();

      setIsLiked((prev) => !prev);
      setLikeCount((prev) => (isLiked ? prev - 1 : prev + 1));

      setPost((prev) => ({
        ...prev,
        isLikedByCurrentUser: !isLiked,
        likeCount: isLiked ? prev.likeCount - 1 : prev.likeCount + 1,
      }));
    } catch (err) {
      console.error("Failed to toggle like:", err.message);
    }
  };
  const handleDeletePost = async () => {
    const isConfirmed = window.confirm(
      "Are you sure you want to delete this post? This action cannot be undone."
    );

    if (!isConfirmed) {
      return;
    }

    try {
      const res = await fetch(`${PostURL}/Delete/${post.id}`, {
        method: "DELETE",
        headers: {
          Authorization: "Bearer " + getAccessToken(),
          "Content-Type": "application/json",
        },
      });

      if (!res.ok) {
        throw new Error(`HTTP error ${res.status}`);
      }

      setShowMenu(false);

      if (props.onPostDeleted) {
        props.onPostDeleted(post.id);
      }
    } catch (err) {
      console.error("Failed to delete post:", err.message);

      alert("Failed to delete post. Please try again.");
    }
  };
  const handleAddComment = async () => {
    if (!newComment.trim()) return;

    const comment = {
      content: newComment,
      postId: post.id,
    };

    try {
      const res = await fetch(MainURL + "/api/Comment", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + getAccessToken(),
        },
        body: JSON.stringify(comment),
      });

      if (!res.ok) throw new Error(`HTTP error ${res.status}`);

      const savedComment = await res.json();

      await fetchComments();

      setNewComment("");

      setCommentCount((prev) => prev + 1);

      setPost((prev) => ({
        ...prev,
        commentCount: prev.commentCount + 1,
      }));
    } catch (err) {
      console.error("Failed to post comment:", err.message);
    }
  };
  const handleKeyPress = (e) => {
    if (e.key === "Enter" && !e.shiftKey) {
      e.preventDefault();
      handleAddComment();
    }
  };
  const handleCopyLink = async () => {
    try {
      const postUrl = `${window.location.origin}/PostDetails/${post.id}`;

      await navigator.clipboard.writeText(postUrl);

      setShowMenu(false);

      console.log("Link copied to clipboard!");
    } catch (err) {
      console.error("Failed to copy link:", err);

      try {
        const textArea = document.createElement("textarea");
        textArea.value = `${window.location.origin}/post/${post.id}`;
        document.body.appendChild(textArea);
        textArea.select();
        document.execCommand("copy");
        document.body.removeChild(textArea);

        setShowMenu(false);
      } catch (fallbackErr) {
        console.error("Fallback copy failed:", fallbackErr);
      }
    }
  };

  const fetchComments = async () => {
    try {
      const res = await fetch(CommentsURL + "/" + post.id, {
        headers: {
          Authorization: "Bearer " + getAccessToken(),
        },
      });

      if (!res.ok) throw new Error(`HTTP error ${res.status}`);
      const data = await res.json();
      setComments(data);
    } catch (err) {
      console.error("Failed to fetch comments:", err.message);
    }
  };
  const toggleComments = async () => {
    await fetchComments();
    setShowComments((prev) => !prev);
  };
  const toggleMenu = () => {
    setShowMenu(!showMenu);
  };

  return (
    <div className="post-container">
      {/* Post Header */}
      <div className="post-header">
        <Link
          to={`/UserDetails/${post.userId}`}
          style={{ textDecoration: "none", color: "inherit", display: "flex" }}
        >
          <img
            src={MainURL + post.authorImage}
            alt={post.authorUsername}
            className="user-avatar"
          />
          <div className="user-info">
            <h3 className="user-name">{post.authorUsername}</h3>
            <p className="user-meta">{formatPostDate(post.createdAt)}</p>
          </div>
        </Link>
        <div className="menu-container" ref={menuRef}>
          <button onClick={toggleMenu} className="menu-button">
            ⋯
          </button>
          {showMenu && (
            <div className="dropdown-menu">
              {post.userId === user.id && (
                <>
                  <Link
                    to="/Create-post"
                    state={{ currentPost: post }}
                    className="dropdown-item"
                  >
                    Edit Post
                  </Link>
                  <button
                    className="dropdown-item delete-item"
                    onClick={handleDeletePost}
                  >
                    Delete
                  </button>
                </>
              )}
              <button className="dropdown-item" onClick={handleCopyLink}>
                Copy Link
              </button>
            </div>
          )}
        </div>
      </div>
      {/* Post Content */}
      <div className="post-content">
        {post.mediaUrl !== "N/A" ? (
          <Link
            to={`/PostDetails/${post.id}`}
            style={{
              textDecoration: "none",
              color: "inherit",
              display: "flex",
              flexDirection: "column",
            }}
          >
            <p className="post-text">{post.content}</p>
            <img
              src={MainURL + post.mediaUrl}
              className="post-image"
              alt="Post media"
            />
          </Link>
        ) : (
          <Link
            to={`/PostDetails/${post.id}`}
            style={{
              textDecoration: "none",
              color: "inherit",
              display: "flex",
              flexDirection: "column",
            }}
          >
            {" "}
            <p className="post-text">{post.content}</p>
          </Link>
        )}
      </div>
      {/* Post Actions */}
      <div className="post-actions">
        {/* Action Buttons */}
        <div className="action-buttons">
          <div className="action-group">
            <div className="action-item">
              <button
                onClick={handleLike}
                className={`action-button ${isLiked ? "liked" : ""}`}
              >
                {isLiked ? "❤️" : "♡"}
              </button>
              <span className="action-count">{likeCount}</span>
            </div>

            <div className="action-item">
              <button onClick={toggleComments} className="action-button">
                💬
              </button>
              <span className="action-count">{commentCount}</span>
            </div>
          </div>
        </div>
        {/* Comments Section */}
        {showComments && (
          <>
            <div className="comments-section">
              {/* Existing Comments */}
              {comments.map((comment) => (
                <div
                  key={comment.id}
                  className="comment"
                  style={{ display: "flex", alignItems: "center" }}
                >
                  <Link
                    to={`/userdetails/${comment.userId}`}
                    className="flex-shrink-0"
                  >
                    <img
                      src={MainURL + comment.userImageUrl}
                      alt={comment.userName}
                      className="comment-avatar"
                    />
                  </Link>
                  <div className="comment-content">
                    <p className="comment-text">
                      <Link
                        to={`/userdetails/${comment.userId}`}
                        className="font-semibold hover:underline text-gray-800"
                        style={{ textDecoration: "None" }}
                      >
                        {comment.userName}
                      </Link>{" "}
                      {comment.content}
                    </p>
                    <p className="comment-time">
                      {formatPostDate(comment.createdAt)}
                    </p>
                  </div>
                </div>
              ))}

              {/* Add Comment */}
            </div>
            <div
              className="add-comment"
              style={{ display: "flex", alignItems: "center" }}
            >
              <img
                src={MainURL + user.profilePictureUrl}
                alt="You"
                className="comment-avatar"
              />
              <textarea
                placeholder="Add a comment..."
                value={newComment}
                onChange={(e) => setNewComment(e.target.value)}
                onKeyPress={handleKeyPress}
                className="comment-input"
                rows={1}
              />
              <button
                onClick={handleAddComment}
                disabled={!newComment.trim()}
                className={`send-button ${newComment.trim() ? "active" : ""}`}
              >
                ➤
              </button>
            </div>
          </>
        )}
      </div>
    </div>
  );
}

export default Post;
