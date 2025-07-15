import { Image, X, MapPin, ArrowLeft } from "lucide-react";
import React, { useState, useRef, useEffect } from "react";
import "../Components/styles/CreatePost.css";
import { useSelector } from "react-redux";
import { PostURL, MainURL } from "../URLS";
import { useNavigate } from "react-router-dom";
import { useLocation } from "react-router-dom";
import { getAccessToken } from "../utils/auth";
import { Link } from "react-router-dom";

const CreatePostPage = () => {
  const loc = useLocation();
  const pst = loc.state?.currentPost;
  const [CurrentPost, setCurrentPost] = useState(pst);
  const navigate = useNavigate();
  const user = useSelector((state) => state.user);
  const [postText, setPostText] = useState("");
  const [selectedImage, setSelectedImage] = useState(null);
  const [imagePreview, setImagePreview] = useState(null);
  const [location, setLocation] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);
  const fileInputRef = useRef(null);

  const MAX_CHARACTERS = 2000;

  const isEditMode = CurrentPost && CurrentPost.id;

  const hasContent =
    postText.trim() ||
    selectedImage ||
    (isEditMode && imagePreview && !selectedImage);
  const isOverLimit = postText.length > MAX_CHARACTERS;
  const canSubmit = hasContent && !isOverLimit;
  const currentLength = postText.length;
  const remainingCharacters = MAX_CHARACTERS - currentLength;

  useEffect(() => {
    if (isEditMode) {
      setPostText(CurrentPost.content || "");
      setLocation(CurrentPost.location || "");

      if (CurrentPost.mediaUrl != "N/A") {
        setImagePreview(MainURL + CurrentPost.mediaUrl);
      }
    }
  }, [CurrentPost, isEditMode]);

  const handleImageSelect = (event) => {
    const file = event.target.files[0];
    if (file) {
      setSelectedImage(file);
      const reader = new FileReader();
      reader.onload = (e) => {
        setImagePreview(e.target.result);
      };
      reader.readAsDataURL(file);
    }
  };

  const handleBack = () => {
    navigate(-1);
  };

  const removeImage = () => {
    setSelectedImage(null);
    setImagePreview(null);
    if (fileInputRef.current) {
      fileInputRef.current.value = "";
    }
  };

  const handleTextChange = (e) => {
    const text = e.target.value;
    if (text.length <= MAX_CHARACTERS) {
      setPostText(text);
    }
  };

  const handleSubmit = async () => {
    if (!postText.trim() && !selectedImage && !imagePreview) return;

    if (postText.length > MAX_CHARACTERS) return;

    setIsSubmitting(true);

    const formData = new FormData();

    if (isEditMode) {
      formData.append("Id", CurrentPost.id);
      formData.append("Content", postText.trim());
      if (selectedImage) {
        formData.append("Media", selectedImage);
      }

      try {
        const res = await fetch(`${PostURL}/Update`, {
          method: "PUT",
          headers: {
            Authorization: "Bearer " + getAccessToken(),
          },
          body: formData,
        });

        if (!res.ok) throw new Error(`HTTP error ${res.status}`);

        setTimeout(() => {
          console.log("Post updated:", {
            text: postText,
            image: selectedImage,
            location,
          });

          alert("Post updated successfully!");
          navigate(`/PostDetails/${CurrentPost}`);
        }, 1000);
      } catch (err) {
        console.error("Failed to update post:", err.message);
        alert("Failed to update post. Please try again.");
      } finally {
        setIsSubmitting(false);
      }
    } else {
      formData.append("Content", postText.trim());
      if (selectedImage) {
        formData.append("Media", selectedImage);
      }

      try {
        const res = await fetch(`${PostURL}/Create`, {
          method: "POST",
          headers: {
            Authorization: "Bearer " + getAccessToken(),
          },
          body: formData,
        });

        if (!res.ok) throw new Error(`HTTP error ${res.status}`);

        const savedPost = await res.json();

        setTimeout(() => {
          console.log("Post created:", {
            text: postText,
            image: selectedImage,
            location,
          });

          setPostText("");
          setSelectedImage(null);
          setImagePreview(null);
          setLocation("");
          alert("Post created successfully!");
          navigate(`/PostDetails/${savedPost.id}`);
        }, 1000);
      } catch (err) {
        console.error("Failed to create post:", err.message);
        alert("Failed to create post. Please try again.");
      } finally {
        setIsSubmitting(false);
      }
    }
  };

  return (
    <div className="create-post-page">
      <div className="create-post-container">
        {/* Header */}
        <div className="create-post-header">
          <button className="back-button" onClick={handleBack}>
            <ArrowLeft size={24} />
          </button>
          <h1>{isEditMode ? "Edit Post" : "Create Post"}</h1>
          <button
            className={`share-button ${!canSubmit ? "disabled" : ""}`}
            onClick={handleSubmit}
            disabled={!canSubmit || isSubmitting}
          >
            {isSubmitting
              ? isEditMode
                ? "Updating..."
                : "Posting..."
              : isEditMode
              ? "Update"
              : "Post"}
          </button>
        </div>

        {/* User Info */}
        <div className="post-author">
          {user.profilePictureUrl ? (
            <img
              src={MainURL + user.profilePictureUrl}
              alt="Profile"
              className="author-avatar"
            />
          ) : (
            <div className="avatar-placeholder-search">
              <span>👤</span>
            </div>
          )}

          <div className="author-info">
            <h3>
              <Link
                to={`/userdetails/${user.id}`}
                className="no-underline text-inherit"
                style={{textDecoration:"none", color:"black"}}
              >
                {user.firstName + " " + user.lastName}
              </Link>
            </h3>
          </div>
        </div>

        {/* Post Content */}
        <div className="post-content-area">
          <textarea
            placeholder="What's on your mind?"
            value={postText}
            onChange={handleTextChange}
            className="post-textarea"
            rows={6}
          />

          {/* Image Preview */}
          {imagePreview && (
            <div className="image-preview">
              <img src={imagePreview} alt="Preview" className="preview-image" />
              <button className="remove-image" onClick={removeImage}>
                <X size={20} />
              </button>
              {/* Show indicator if this is a new image being uploaded */}
              {selectedImage && (
                <div className="image-status">
                  <span className="new-image-indicator">
                    New image selected
                  </span>
                </div>
              )}
            </div>
          )}
        </div>

        {/* Character Counter */}
        <div className="character-counter">
          <span
            className={`counter-text ${isOverLimit ? "over-limit" : ""}`}
            style={{ paddingRight: "20px" }}
          >
            {currentLength.toLocaleString()} / {MAX_CHARACTERS.toLocaleString()}{" "}
          </span>
          {isOverLimit && (
            <span className="over-limit-warning">
              Character limit exceeded by{" "}
              {Math.abs(remainingCharacters).toLocaleString()}
            </span>
          )}
        </div>

        {/* Post Options */}
        <div className="post-options">
          <div className="options-label">
            <span>Add to your post</span>
          </div>
          <div className="options-buttons">
            <button
              className="option-button"
              onClick={() => fileInputRef.current?.click()}
            >
              <Image size={24} />
              <span>{imagePreview ? "Change Photo" : "Photo"}</span>
            </button>
          </div>
        </div>
        {/* Hidden File Input */}
        <input
          type="file"
          ref={fileInputRef}
          onChange={handleImageSelect}
          accept="image/*"
          style={{ display: "none" }}
        />
      </div>
    </div>
  );
};

export default CreatePostPage;
