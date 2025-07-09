import React, { useState, useEffect } from 'react';
import './styles/profilePage.css';
import Post from '../Components/Post';
import { useSelector } from 'react-redux';
import { MainURL, PostURL, CurrentUserURL } from "../URLS";
import { authKey } from "../rtk/User-Slice";
import { Link } from 'react-router-dom';
import { formatPostDate } from '../utils/date-helper';
import { useParams } from "react-router-dom";
import { current } from '@reduxjs/toolkit';
import SocialPostInterface from '../Components/postInterface';
import { getAccessToken } from '../utils/auth';
import { delay } from 'framer-motion';

const ProfilePage = () => {
  const currentuser = useSelector(state => state.user);
 
  const { userId } = useParams(); 
  const [posts, setPosts] = useState([]);
  const [user, setUser] = useState({})
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [editForm, setEditForm] = useState({
    firstName: '',
    lastName: '',
    bio: '',
    location: '',
    profilePicture: null,
    secondaryProfilePicture: null
  });
  const [updating, setUpdating] = useState(false);
  const [isFollowing, setIsFollowing] = useState(false);
  const [isAddedToMessages, setisAddedToMessages] = useState(false);
  const [followLoading, setFollowLoading] = useState(false);
  const [AddToMessagesLoading, setAddToMessagesLoading] = useState(false);

  // Followers modal state
  const [showFollowersModal, setShowFollowersModal] = useState(false);
  const [followers, setFollowers] = useState([]);
  const [followersLoading, setFollowersLoading] = useState(false);
  const [followersError, setFollowersError] = useState(false);

  // Following modal state
  const [showFollowingModal, setShowFollowingModal] = useState(false);
  const [following, setFollowing] = useState([]);
  const [followingLoading, setFollowingLoading] = useState(false);
  const [followingError, setFollowingError] = useState(false);

  // Check if current user is viewing their own profile
  const isOwnProfile = currentuser.id == userId;

  console.log("Fetching from:", CurrentUserURL + "/" + userId);

  useEffect(() => {
    console.log("userId",userId);
    const fetchUser = async () => {
      try{
        const res = await fetch(CurrentUserURL + "/" + userId, {
          headers: {
              Authorization: "Bearer " + getAccessToken()
            }
      });
    
      if (!res.ok) {
        throw new Error(`HTTP error ${res.status}`);
      }
    
      const data = await res.json();
      console.log(data);

      setUser(data);
      setPosts(data.posts);
      // Check if current user is following this user
      setIsFollowing(data.isFollowedByCurrentUser || false);
      }
      catch (err) {
        console.error("Failed to fetch posts:", err.message);
        setError(true);
      } finally {
        setLoading(false);
      }
    }
    
    fetchUser();
  }, [userId]);

  useEffect(() => {
    // Initialize form with user data when editing starts
    if (isEditing) {
      setEditForm({
        firstName: user.firstName || '',
        lastName: user.lastName || '',
        bio: user.bio || '',
        location: user.location || '',
        profilePicture: null,
        secondaryProfilePicture: null
      });
    }
  }, [isEditing, user]);

  // Fetch followers function
  const fetchFollowers = async () => {
    setFollowersLoading(true);
    setFollowersError(false);
    try {
      const response = await fetch(`${MainURL}/api/User/${userId}/followers?pageNumber=1&pageSize=12`, {
        headers: {
          Authorization: "Bearer " + getAccessToken()
        }
      });

      if (!response.ok) {
        throw new Error(`HTTP error ${response.status}`);
      }

      const data = await response.json();
      setFollowers(data);
    } catch (err) {
      console.error("Failed to fetch followers:", err.message);
      setFollowersError(true);
    } finally {
      setFollowersLoading(false);
    }
  };

  // Fetch following function
  const fetchFollowing = async () => {
    setFollowingLoading(true);
    setFollowingError(false);
    try {
      const response = await fetch(`${MainURL}/api/User/${userId}/following?pageNumber=1&pageSize=12`, {
        headers: {
          Authorization: "Bearer " + getAccessToken()
        }
      });

      if (!response.ok) {
        throw new Error(`HTTP error ${response.status}`);
      }

      const data = await response.json();
      setFollowing(data);
    } catch (err) {
      console.error("Failed to fetch following:", err.message);
      setFollowingError(true);
    } finally {
      setFollowingLoading(false);
    }
  };

  // Handle showing followers modal
  const handleShowFollowers = () => {
    setShowFollowersModal(true);
    fetchFollowers();
  };

  // Handle closing followers modal
  const handleCloseFollowersModal = () => {
    setShowFollowersModal(false);
    setFollowers([]);
    setFollowersError(false);
  };

  // Handle showing following modal
  const handleShowFollowing = () => {
    setShowFollowingModal(true);
    fetchFollowing();
  };

  // Handle closing following modal
  const handleCloseFollowingModal = () => {
    setShowFollowingModal(false);
    setFollowing([]);
    setFollowingError(false);
  };

  const handleEditProfile = () => {
    if (isOwnProfile) {
      setIsEditing(true);
    }
  };

  const handleCancelEdit = () => {
    setIsEditing(false);
    setEditForm({
      firstName: '',
      lastName: '',
      bio: '',
      location: '',
      profilePicture: null,
      secondaryProfilePicture: null
    });
  };

  const handleFormChange = (e) => {
    const { name, value } = e.target;
    setEditForm(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleFileChange = (e, type) => {
    const file = e.target.files[0];
    if (file) {
      setEditForm(prev => ({
        ...prev,
        [type]: file
      }));
    }
  };
  const handlePostDeleted = (deletedPostId) => {
    console.log("handlePostDeleted is called")
    setPosts(posts => posts.filter(post => post.id !== deletedPostId));
  };
  // Handle follow/unfollow action
  const handleFollowToggle = async () => {
    if (isOwnProfile || followLoading) return;
    
    setFollowLoading(true);
    try {
      const endpoint = isFollowing ? 'unfollow' : 'follow';
      const response = await fetch(`${MainURL}/api/User/follow/${userId}`, {
        method: 'POST',
        headers: {
          'Authorization': "Bearer " + getAccessToken(),
          'Content-Type': 'application/json'
        }
      });

      if (!response.ok) {
        throw new Error(`Failed to ${endpoint} user`);
      }

      // Toggle the follow state
      setIsFollowing(!isFollowing);
      
      // Update follower count
      setUser(prev => ({
        ...prev,
        followersCount: isFollowing ? prev.followersCount - 1 : prev.followersCount + 1
      }));
      
    } catch (err) {
      console.error('Error following/unfollowing user:', err);
      alert(`Failed to ${isFollowing ? 'unfollow' : 'follow'} user. Please try again.`);
    } finally {
      setFollowLoading(false);
    }
  };
  const handleAddToMessageToggle = async () => {
    if (isOwnProfile || AddToMessagesLoading) return;
  
    setAddToMessagesLoading(true);
    try {
      const messageData = {
        receiverId: userId,
        content: "👋"
      };
  
      const response = await fetch(`${MainURL}/api/Messaging/messages/Send`, {
        method: 'POST',
        headers: {
          'Authorization': "Bearer " + getAccessToken(),
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(messageData)
      });


      if (!response.ok) {
        throw new Error('Failed to message user');
      }

      

      setisAddedToMessages(!isAddedToMessages);
    } catch (err) {
      console.error('Error Messaging user:', err);
      alert(`Failed to ${isFollowing ? 'unfollow' : 'follow'} user. Please try again.`);
    } finally {
      await delay(5000);
      setAddToMessagesLoading(false); // ✅ You probably meant this instead of setFollowLoading
    }
  };
  
  // Handle direct image upload
  const handleImageUpload = async (file, type) => {
    if (!file || !isOwnProfile) return;
    
    setUpdating(true);
    try {
      const formData = new FormData();
      formData.append('firstName', user.firstName || '');
      formData.append('lastName', user.lastName || '');
      formData.append('bio', user.bio || '');
      formData.append('location', user.location || '');
      
      if (type === 'profilePicture') {
        formData.append('profilePicture', file);
      } else if (type === 'secondaryProfilePicture') {
        formData.append('secondaryProfilePicture', file);
      }

      const response = await fetch(MainURL + "/api/User/profile", {
        method: 'PUT',
        headers: {
          Authorization: "Bearer " + getAccessToken(),
        },
        body: formData
      });

      if (!response.ok) {
        throw new Error('Failed to update profile');
      }

      // Refresh the page or update the Redux store
      window.location.reload();
      
    } catch (err) {
      console.error('Error updating profile:', err);
      alert('Failed to update profile. Please try again.');
    } finally {
      setUpdating(false);
    }
  };

  const handleDirectImageChange = (e, type) => {
    const file = e.target.files[0];
    if (file && isOwnProfile) {
      handleImageUpload(file, type);
    }
  };

  const handleSaveProfile = async () => {
    if (!isOwnProfile) return;
    
    setUpdating(true);
    try {
      const formData = new FormData();
      formData.append('firstName', editForm.firstName);
      formData.append('lastName', editForm.lastName);
      formData.append('bio', editForm.bio);
      formData.append('location', editForm.location);
      
      if (editForm.profilePicture) {
        formData.append('profilePicture', editForm.profilePicture);
      }
      if (editForm.secondaryProfilePicture) {
        formData.append('secondaryProfilePicture', editForm.secondaryProfilePicture);
      }
      const response = await fetch(MainURL + "/api/User/profile", {
        method: 'PUT',
        headers: {
          Authorization: "Bearer " + getAccessToken(),
        },
        body: formData
      });

      if (!response.ok) {
        throw new Error('Failed to update profile');
      }

      // Refresh the page or update the Redux store
      window.location.reload();
      
    } catch (err) {
      console.error('Error updating profile:', err);
      alert('Failed to update profile. Please try again.');
    } finally {
      setUpdating(false);
    }
  };

  const triggerFileInput = (inputId) => {
    if (isOwnProfile) {
      document.getElementById(inputId).click();
    }
  };

  return (
    <div className="min-h-screen bg-gray-100">
      {/* Hidden file inputs for direct image upload - only render for own profile */}
      {isOwnProfile && (
        <>
          <input
            type="file"
            id="coverPhotoInput"
            accept="image/*"
            onChange={(e) => handleDirectImageChange(e, 'secondaryProfilePicture')}
            style={{ display: 'none' }}
          />
          <input
            type="file"
            id="profilePictureInput"
            accept="image/*"
            onChange={(e) => handleDirectImageChange(e, 'profilePicture')}
            style={{ display: 'none' }}
          />
        </>
      )}

      {/* Cover Photo & Profile Info */}
      <div className="profile-section">
        <div className="max-w-4xl mx-auto">
          {/* Cover Photo */}
          <div className="cover-photo-container">
            <img 
              src={MainURL + user.secondaryPictureUrl} 
              alt="Cover"
              className="w-full h-full object-cover"
            />
            {/* Only show edit button for own profile */}
            {isOwnProfile && (
              <button 
                onClick={() => triggerFileInput('coverPhotoInput')}
                disabled={updating}
                className="absolute bottom-4 right-4 bg-white hover:bg-gray-100 px-4 py-2 rounded-md shadow-md text-sm font-medium disabled:opacity-50"
              >
                <span className="icon mr-2">📷</span>
                {updating ? 'Uploading...' : 'Edit cover photo'}
              </button>
            )}
          </div>
          
          {/* Profile Info */}
          <div className="px-6 pb-4">
            <div className="flex flex-col sm:flex-row sm:items-end sm:justify-between -mt-16 sm:-mt-8" style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}
            >
              <div className="flex flex-col sm:flex-row sm:items-end"  style={{paddingTop:"30px", display:"flex", justifyContent:"center", alignItems:"center"}}>
                <div className="relative mb-4 sm:mb-0 mx-auto sm:mx-0" style={{paddingTop:"20px"}}>
                  <img 
                    src={MainURL + user.profilePictureUrl} 
                    alt={user.firstName && user.lastName ? `${user.firstName} ${user.lastName}` : 'User'}
                    className="w-40 h-40 rounded-full border-4 border-white shadow-lg"
                  />
                  {/* Only show edit button for own profile */}
                  {isOwnProfile && (
                    <button 
                      onClick={() => triggerFileInput('profilePictureInput')}
                      disabled={updating}
                      className="absolute bottom-1 right-1 sm:bottom-2 sm:right-2 bg-gray-100 hover:bg-gray-200 p-2 rounded-full shadow-md disabled:opacity-50"
                    >
                      <span className="icon text-gray-600">
                        {updating ? '⏳' : '📷'}
                      </span>
                    </button>
                  )}
                </div>
                <div className="sm:ml-6 sm:pb-2 text-center sm:text-left" style={{paddingLeft: "15px"}}>
                <div style={{display:"flex", flexDirection:"column"}}>
                  <h1 className="font-bold text-gray-900 break-words leading-tight" style={{maxHeight:"5px"}}>
                    {user.firstName && user.lastName ? `${user.firstName} ${user.lastName}` : 'User Name'}
                  </h1>
                  <p className="text-gray-500 text-base" style={{minHeight:"30px"}}>
                    @{user.username || 'username'}
                  </p>
                </div>

                  <div className="flex space-x-4 text-sm sm:text-base">
                    <button 
                      onClick={handleShowFollowers}
                      className="text-gray-600 hover:text-blue-600 hover:underline cursor-pointer font-semibold text-base"
                      style={{textDecoration:"none" }}
                    >👥{user.followersCount ? user.followersCount.toLocaleString() : 0} Followers
                    </button>
                    <button 
                      onClick={handleShowFollowing}
                      className="text-gray-600 hover:text-blue-600 hover:underline cursor-pointer font-semibold text-base"
                      style={{textDecoration:"none"}}
                    >
                      🫂{user.followingCount ? user.followingCount.toLocaleString() : 0} Following
                    </button>
                  </div>
                </div>
              </div>
              
              {/* Follow Button - Only show for other users' profiles */}
              {!isOwnProfile && (
                <div className="mt-4 sm:mt-0 flex justify-center sm:justify-end" style={{gap:"10px"}}>
                  <button
                    onClick={handleAddToMessageToggle}
                    disabled={AddToMessagesLoading}
                    className={`Addtomessage-button ${isAddedToMessages ? 'following' : ''}`}
                  >
                    <span className="button-text">
                      {AddToMessagesLoading ? 'Loading...' :  `Wave At ${user.firstName}`}
                    </span>
                  </button>
                  <button
                    onClick={handleFollowToggle}
                    disabled={followLoading}
                    className={`follow-button ${isFollowing ? 'following' : ''}`}
                  >
                    <span className="button-text">
                      {followLoading ? 'Loading...' : isFollowing ? 'Following' : 'Follow'}
                    </span>
                  </button>
                </div>
              )}
            </div>
          </div>
        </div>
      </div>
      
      {/* Main Content */}
      <div className="max-w-4xl mx-auto px-4 py-6">
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-6"style={{maxWidth:"1000px"}}>
          {/* Left Sidebar */}
          <div className="lg:col-span-1" >
            <div className="intro-card">
              <h3 className="font-semibold text-gray-900">About</h3>
              
              <div>
                <p className="text-gray-700 text-sm mb-3">
                  {user.bio || "No bio available"}
                </p>
                <div className="space-y-2 text-sm text-gray-600">
                  <div className="flex items-center">
                    <span className="icon mr-2">📍</span>
                    Lives in {user.location || "N/A"}
                  </div>
                  <div className="flex items-center">
                    <span className="icon mr-2">📅</span>
                    Joined {formatPostDate(user.joinedIn)}
                  </div>
                </div>
                {/* Only show edit profile button for own profile */}
                {isOwnProfile && (
                  <button 
                    onClick={handleEditProfile}
                    className="w-full mt-4 bg-gray-100 hover:bg-gray-200 py-2 rounded-md text-sm font-medium"
                  >
                    Edit profile
                  </button>
                )}
              </div>
            </div>
          </div>
          
          {/* Main Content - Posts Only */}
          <div className="lg:col-span-2">
            <div className="space-y-4">
             
              {/* <div className="bg-white rounded-lg shadow-sm p-4">
                <div className="flex items-center space-x-3 mb-4">
                  <img 
                    src={MainURL + user.profilePictureUrl} 
                    alt={user.firstName + " " + user.lastName}
                    className="w-10 h-10 rounded-full"
                  />
                  <Link to="/create-post" className="flex-1">
                    <input 
                      type="text" 
                      placeholder="What's on your mind?"
                      className="w-full bg-gray-100 rounded-full px-4 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
                      readOnly
                    />
                  </Link>
                </div>
                <div className="flex items-center justify-between pt-3 border-t border-gray-200">
                  <button className="flex items-center space-x-2 text-gray-600 hover:bg-gray-100 px-3 py-2 rounded-md">
                    <span className="icon text-red-500">🎥</span>
                    <span className="text-sm font-medium">Live video</span>
                  </button>
                  <button className="flex items-center space-x-2 text-gray-600 hover:bg-gray-100 px-3 py-2 rounded-md">
                    <span className="icon text-green-500">📸</span>
                    <span className="text-sm font-medium">Photo/video</span>
                  </button>
                  <Link 
                    to="/create-post"
                    className="bg-blue-500 hover:bg-blue-600 text-white px-4 py-2 rounded-md text-sm font-medium"
                  >
                    Post
                  </Link>
                </div>
              </div> */}
              <SocialPostInterface user={user}/>
              {/* Posts */}
              {(!posts || posts.length === 0) ? (
                <div className="flex flex-col items-center justify-center py-10 bg-white rounded-lg shadow-md text-center" style={{padding: "20px"}}>
                  <h3 className="text-lg font-semibold text-gray-700">No Posts Yet</h3>
                  <p className="text-sm text-gray-500 mt-1">When you post something, it’ll show up here.</p>
                </div>
              ) : (
                [...posts]
                  .sort((a, b) => new Date(b.createdAt) - new Date(a.createdAt))
                  .map((post, index) => (
                    <Post
                     key={index}
                    post={post}
                    onPostDeleted={handlePostDeleted}
                     />
                  ))
              )}


            </div>
          </div>
        </div>
      </div>

      {/* Followers Modal */}
      {showFollowersModal && (
        <div className="modal-overlay">
          <div className="modal-content" style={{ maxWidth: '600px', maxHeight: '900px' }}>
            <div className="p-6">
              <div className="flex items-center justify-between mb-4">
                <h2 className="text-xl font-bold">Followers</h2>
                <button
                  onClick={handleCloseFollowersModal}
                  className="text-gray-400 hover:text-gray-600 text-2xl"
                >
                  ✕
                </button>
              </div>
              
              <div className="followers-list" style={{ maxHeight: '450px', overflowY: 'auto' }}>
                {followersLoading ? (
                  <div className="flex justify-center py-8">
                    <div className="text-gray-500">Loading followers...</div>
                  </div>
                ) : followersError ? (
                  <div className="text-center py-8">
                    <div className="text-red-500 mb-2">Failed to load followers</div>
                    <button 
                      onClick={fetchFollowers}
                      className="text-blue-500 hover:text-blue-600 text-sm"
                    >
                      Try again
                    </button>
                  </div>
                ) : followers.length === 0 ? (
                  <div className="text-center py-8 text-gray-500">
                    No followers yet
                  </div>
                ) : (
                  <div className="space-y-3">
                    {followers.map((follower) => (
                      <div key={follower.id} className="flex items-center space-x-3 p-3 hover:bg-gray-50 rounded-lg">
                        <Link to={`/userdetails/${follower.id}`} onClick={handleCloseFollowersModal}>
                          <img 
                            src={MainURL + follower.profilePictureUrl} 
                            alt={`${follower.firstName} ${follower.lastName}`}
                            className="w-10 h-10 rounded-full object-cover"
                          />
                        </Link>
                        <div className="flex-1 min-w-0">
                          <Link 
                            to={`/userdetails/${follower.id}`} 
                            onClick={handleCloseFollowersModal}
                            className="block text-inherit no-underline hover:text-blue-600"
                            style={{textDecoration: "None"}}
                          >
                            <h3 className="font-semibold text-gray-900 truncate">
                              {follower.firstName && follower.lastName 
                                ? `${follower.firstName} ${follower.lastName}` 
                                : 'User'}
                            </h3>
                          </Link>
                          {follower.bio && (
                            <p className="text-xs text-gray-500 mt-1 truncate">{follower.bio}</p>
                          )}
                        </div>
                      </div>
                    ))}
                  </div>
                )}
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Following Modal */}
      {showFollowingModal && (
        <div className="modal-overlay">
          <div className="modal-content" style={{ maxWidth: '600px', maxHeight: '900px' }}>
            <div style={{padding:"0 0 20px 20px"}}>
              <div className="flex items-center justify-between mb-4">
                <h2 className="text-xl font-bold">Following</h2>
                <button
                  onClick={handleCloseFollowingModal}
                  className="text-gray-400 hover:text-gray-600 text-2xl"
                >
                  ✕
                </button>
              </div>
              
              <div className="following-list" style={{ maxHeight: '450px', overflowY: 'auto' }}>
                {followingLoading ? (
                  <div className="flex justify-center py-8" style={{padding:"30px"}}>
                    <div className="text-gray-500">Loading following...</div>
                  </div>
                ) : followingError ? (
                  <div className="text-center py-8" style={{padding:"30px"}}>
                    <div className="text-red-500 mb-2" >Failed to load following</div>
                    <button 
                      onClick={fetchFollowing}
                      className="text-blue-500 hover:text-blue-600 text-sm"
                    >
                      Try again
                    </button>
                  </div>
                ) : following.length === 0 ? (
                  <div className="text-center py-8 text-gray-500" style={{padding:"30px"}}>
                    Not following anyone yet
                  </div>
                ) : (
                  <div className="space-y-3">
                    {following.map((followedUser) => (
                      <div key={followedUser.id} className="flex items-center space-x-3 p-3 hover:bg-gray-50 rounded-lg">
                        <Link to={`/userdetails/${followedUser.id}`} onClick={handleCloseFollowingModal}>
                          <img 
                            src={MainURL + followedUser.profilePictureUrl} 
                            alt={`${followedUser.firstName} ${followedUser.lastName}`}
                            className="w-10 h-10 rounded-full object-cover"
                          />
                        </Link>
                        <div className="flex-1 min-w-0">
                          <Link 
                            to={`/userdetails/${followedUser.id}`} 
                            onClick={handleCloseFollowingModal}
                            className="block text-inherit no-underline hover:text-blue-600"
                            style={{textDecoration: "None"}}
                          >
                            <h3 className="font-semibold text-gray-900 truncate">
                              {followedUser.firstName && followedUser.lastName 
                                ? `${followedUser.firstName} ${followedUser.lastName}` 
                                : 'User'}
                            </h3>
                          </Link>
                          {followedUser.bio && (
                            <p className="text-xs text-gray-500 mt-1 truncate">{followedUser.bio}</p>
                          )}
                        </div>
                      </div>
                    ))}
                  </div>
                )}
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Edit Profile Modal - Only show for own profile */}
      {isEditing && isOwnProfile && (
        <div className="modal-overlay">
          <div className="modal-content">
            <div className="p-4">
              <div className="flex items-center justify-between mb-4">
                <h2 className="text-xl font-bold">Edit Profile</h2>
                <button
                  onClick={handleCancelEdit}
                  className="text-gray-400 hover:text-gray-600"
                >
                  ✕
                </button>
              </div>
              
              <div className="space-y-4">
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    First Name
                  </label>
                  <input
                    type="text"
                    name="firstName"
                    value={editForm.firstName}
                    onChange={handleFormChange}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                    placeholder="Enter your first name"
                  />
                </div>
                
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Last Name
                  </label>
                  <input
                    type="text"
                    name="lastName"
                    value={editForm.lastName}
                    onChange={handleFormChange}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                    placeholder="Enter your last name"
                  />
                </div>
                
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Bio
                  </label>
                  <textarea
                    name="bio"
                    value={editForm.bio}
                    onChange={handleFormChange}
                    rows={3}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                    placeholder="Tell us about yourself..."
                  />
                </div>
                
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Location
                  </label>
                  <input
                    type="text"
                    name="location"
                    value={editForm.location}
                    onChange={handleFormChange}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                    placeholder="Where do you live?"
                  />
                </div>
                
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Profile Picture
                  </label>
                  <input
                    type="file"
                    accept="image/*"
                    onChange={(e) => handleFileChange(e, 'profilePicture')}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  />
                  {editForm.profilePicture && (
                    <p className="text-sm text-gray-600 mt-1">
                      Selected: {editForm.profilePicture.name}
                    </p>
                  )}
                </div>
                
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Cover Photo
                  </label>
                  <input
                    type="file"
                    accept="image/*"
                    onChange={(e) => handleFileChange(e, 'secondaryProfilePicture')}
                    className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  />
                  {editForm.secondaryProfilePicture && (
                    <p className="text-sm text-gray-600 mt-1">
                      Selected: {editForm.secondaryProfilePicture.name}
                    </p>
                  )}
                </div>
              </div>
              
              <div className="flex items-center justify-end space-x-3 mt-6">
                <button
                  onClick={handleCancelEdit}
                  className="px-4 py-2 text-gray-600 hover:text-gray-800"
                  disabled={updating}
                >
                  Cancel
                </button>
                <button
                  onClick={handleSaveProfile}
                  disabled={updating}
                  className="bg-blue-500 hover:bg-blue-600 text-white px-4 py-2 rounded-md font-medium disabled:opacity-50"
                >
                  {updating ? 'Saving...' : 'Save Changes'}
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default ProfilePage;