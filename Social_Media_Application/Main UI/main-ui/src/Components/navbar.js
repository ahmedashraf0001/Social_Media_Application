import React, { useState, useRef, useEffect } from 'react';
import { 
  Home, 
  Search, 
  PlusSquare, 
  Heart, 
  MessageCircle, 
  User, 
  Settings, 
  LogOut,
  Bell,
  Users,
  Bookmark,
  Send,
  Minus,
  X,
  Phone,
  Video,
  MoreHorizontal,
  Circle
} from 'lucide-react';
import './styles/navbar.css'; // Import the CSS file
import { useDispatch, useSelector } from 'react-redux';
import { getNotis, MainURL } from '../URLS';
import { Link, useNavigate, useViewTransitionState } from 'react-router-dom';
import { getContact } from '../URLS';
import { authKey } from '../rtk/User-Slice';
import { formatPostDate } from '../utils/date-helper';
import { useSignalR, useSignalREvent } from '../services/SignalR';
import SearchComponent from './search';
import { getAccessToken, handleLogout } from '../utils/auth';
import { constructNow } from 'date-fns';
const Navbar = () => {
  const user = useSelector(state => state.user);
  const { sendMessage: signalRSendMessage, editMessage: signalREditMessage, deleteMessage: signalRDeleteMessage , isConnected, onlineUsers, markMessageAsRead: signalRMarkMessageAsRead } = useSignalR();
  const [isProfileOpen, setIsProfileOpen] = useState(false);
  const [isNotificationOpen, setIsNotificationOpen] = useState(false);
  const [isMessagingOpen, setIsMessagingOpen] = useState(false);
  const [openChats, setOpenChats] = useState([]);
  const [searchQuery, setSearchQuery] = useState('');
  const [notifications, setNotifications] = useState([]);
  const [notificationCount, setNotificationCount] = useState(0);
  const [messageCount, setMessageCount] = useState(0);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const profileRef = useRef(null);
  const notificationRef = useRef(null);
  const messagingRef = useRef(null);

  // Close dropdowns when clicking outside
  const [Conversations, setConversations] = useState([]);

  useEffect(() => {
    const handleClickOutside = async (event) => {
      if (profileRef.current && !profileRef.current.contains(event.target)) {
        setIsProfileOpen(false);
      }
      if (messagingRef.current && !messagingRef.current.contains(event.target)) {
        setIsMessagingOpen(false);
      }
    };

    document.addEventListener('mousedown', handleClickOutside);
    return () => document.removeEventListener('mousedown', handleClickOutside);
  }, []);

  const onLogoutClick = async () => {
    // Optional: Success callback
    const onSuccess = () => {
      console.log('Logout successful!');
      // You can show a success message or perform other actions
    };

    // Optional: Error callback
    const onError = (error) => {
      console.error('Logout failed:', error);
      // You can show an error message to the user
      alert('Logout failed. Please try again.');
    };

    // Call the handleLogout function
    await handleLogout(dispatch, onSuccess, onError, navigate);
  };

  // Listen for new messages from SignalR
  useSignalREvent('newMessage', async (message) => {
    // Update open chats with new message
    setOpenChats(prev => prev.map(chat => {
      if (chat.id === message.conversationId) {
        const newMessage = {
          id: message.id,
          text: message.content,
          sender: message.senderId === user.id ? 'You' : chat.conversationName,
          timestamp: formatPostDate(message.sentAt),
          IsEdited: message.IsEdited,
          IsDeleted: message.IsDeleted,
          isMe: message.senderId === user.id,
          isRead: message.isRead
        };
        return {
          ...chat,
          messages: [...chat.messages, newMessage]
        };
      }
      return chat;
    }));    // Update message count for unread messages
    if (message.senderId !== user.id && !openChats.find(e => e.id == message.conversationId)) {
      setMessageCount(prev => prev + 1);
    }
    if(openChats.find(e => e.id == message.conversationId)){
      await signalRMarkMessageAsRead(message.id);
    }
    // Update conversations list
    setConversations(prev => prev.map(conv => {
      if (conv.id === message.conversationId) {
        return {
          ...conv,
          lastMessageContent: message.content,
          lastMessageAt: message.sentAt,
          lastMessageRead: message.senderId === user.id ? true : false
        };
      }
      return conv;
    }));
  });
  // Listen for message marked as read from SignalR
  useSignalREvent('messageMarked', (message) => {
    console.log("Message Marked:", message);
    
    // Update open chats to mark message as read
    setOpenChats(prev => prev.map(chat => ({
      ...chat,
      messages: chat.messages.map(msg => 
        msg.id === message.id 
          ? { ...msg, isRead: true }
          : msg
      )
    })));

    console.log("open chat")

    // Update conversations list if this was the last message
    setConversations(prev => prev.map(conv => {
      if (conv.id === message.conversationId) {
        return {
          ...conv,
          lastMessageRead: true
        };
      }
      return conv;
    }));

    // Decrease message count if this message was previously unread
    if (message.senderId !== user.id) {
      setMessageCount(prev => Math.max(0, prev - 1));
    }
  });
  useSignalREvent('messageEdited', (message) => {
    console.log("Message edited:", message);
    
    // Update open chats to reflect the edited message
    setOpenChats(prev => prev.map(chat => ({
      ...chat,
      messages: chat.messages.map(msg => 
        msg.id === message.id 
          ? { 
              ...msg, 
              text: message.content, 
              edited: true,
              // Update timestamp if provided
              ...(message.editedAt && { editedAt: message.editedAt })
            }
          : msg
      )
    })));

    // Update conversations list if this was the last message
    setConversations(prev => prev.map(conv => {
      if (conv.id === message.conversationId) {
        // Check if the edited message was the last message in the conversation
        const chat = openChats.find(c => c.id === message.conversationId);
        if (chat) {
          const lastMessage = chat.messages[chat.messages.length - 1];
          if (lastMessage && lastMessage.id === message.id) {
            return {
              ...conv,
              lastMessageContent: message.content
            };
          }
        }
      }
      return conv;
    }));
  });
  // Listen for message deleted from SignalR
  useSignalREvent('messageDeleted', (data) => {
    console.log("Message deleted:", data);
    
    // Update open chats to mark message as deleted
    setOpenChats(prev => prev.map(chat => ({
      ...chat,
      messages: chat.messages.map(msg => 
        msg.id === data.messageId 
          ? { 
              ...msg, 
              text: "*This message was deleted*", 
              isDeleted: true,
              // Remove edit/delete capabilities for deleted messages
              canEdit: false,
              canDelete: false
            }
          : msg
      )
    })));

    // Update conversations list if this was the last message
    setConversations(prev => prev.map(conv => {
      if (conv.id === data.conversationId && conv.lastMessageContent) {
        // Check if the deleted message was the last message in the conversation
        const chat = openChats.find(c => c.id === data.conversationId);
        if (chat) {
          const lastMessage = chat.messages[chat.messages.length - 1];
          if (lastMessage && lastMessage.id === data.messageId) {
            return {
              ...conv,
              lastMessageContent: "*This message was deleted*"
            };
          }
        }
      }
      return conv;
    }));
  });
  useSignalREvent('notificationRecieved', (notificationData) => {
    console.log("noti received", notificationData)
    setNotificationCount(prev => prev + 1);
    setNotifications(prev => [notificationData, ...prev]);
  });

  const loadConvos = async () => {
    try {
      const res = await fetch(`${getContact}/1/1000`, {
        headers: { Authorization: "Bearer " + getAccessToken()  },
      });
      if (!res.ok) throw new Error(`HTTP error ${res.status}`);
      const data = await res.json();
      setConversations(data);
      setMessageCount(
        data.reduce((total, e) => total + e.unreadMessages, 0)
      );
    } catch (err) {
      console.error("Failed to fetch Conversations:", err.message);
    } finally {
      setLoading(false);
    }
  };
  const loadNotis = async () => {
    try {
      const res = await fetch(getNotis, {
        headers: { Authorization: "Bearer " + getAccessToken()},
      });
      if (!res.ok) throw new Error(`HTTP error ${res.status}`);
      const data = await res.json();
      setNotifications(data);
      setNotificationCount(
        data.filter(n => n.isRead === false).length
      );
    } catch (err) {
      console.error("Failed to fetch Notifications:", err.message);
    } finally {
      setLoading(false);
    }
  };
  useEffect(() => {
    loadConvos();
    loadNotis();
  }, []);
  // Function to fetch messages for a specific conversation
  const fetchConversationMessages = async (otherUserId) => {
    try {
      const res = await fetch(`${MainURL}/api/Messaging/conversations/Between/${otherUserId}/1/1000`, {
        headers: { Authorization:  "Bearer " + getAccessToken()  },
      });
      if (!res.ok) throw new Error(`HTTP error ${res.status}`);
      const data = await res.json();
      return data.messages || [];
    } catch (err) {
      console.error("Failed to fetch conversation messages:", err.message);
      return [];
    }
  };

  const markConversationMessagesAsRead = async (conversationId, messages) => {
    try {
      // Mark all unread messages in this conversation as read
      const unreadMessages = messages.filter(msg => !msg.isRead && !msg.isMe);
      
      if (unreadMessages.length === 0) {
        return true; // No unread messages to mark
      }
  
      // Update counter optimistically
      setMessageCount(prev => Math.max(0, prev - unreadMessages.length));
      
      let failedCount = 0;
      
      // Mark each message as read
      for (const message of unreadMessages) {
        try {
          await signalRMarkMessageAsRead(message.id);
        } catch (error) {
          console.error(`Failed to mark message ${message.id} as read:`, error);
          failedCount++;
        }
      }
      
      // If some messages failed to mark as read, adjust the counter back
      if (failedCount > 0) {
        setMessageCount(prev => prev + failedCount);
        console.warn(`Failed to mark ${failedCount} messages as read`);
      }
      
      return failedCount === 0;
    } catch (err) {
      console.error("Failed to mark messages as read:", err.message);
      return false;
    }
  };
  const markNotificationsAsRead = async (unreadNotifications) => {
    try {
      // Validate input
      if (!unreadNotifications || unreadNotifications.length === 0) {
        console.log('No notifications to mark as read');
        return true;
      }
  
      console.log(`Marking ${unreadNotifications.length} notifications as read...`);
      
      // Mark all unread notifications as read with better error handling
      const markPromises = unreadNotifications.map(notification => 
        fetch(`${MainURL}/api/Notification/MarkAsRead/${notification.id}`, {
          method: 'PUT',
          headers: { 
            Authorization: "Bearer " + getAccessToken(),
            'Content-Type': 'application/json'
          },
        }).then(response => {
          if (!response.ok) {
            throw new Error(`Failed to mark notification ${notification.id} as read: ${response.status}`);
          }
          return { success: true, notificationId: notification.id };
        }).catch(error => {
          console.error(`Error marking notification ${notification.id}:`, error);
          return { success: false, notificationId: notification.id, error };
        })
      );
  
      const results = await Promise.allSettled(markPromises);
      
      // Count successful operations
      const successfulResults = results
        .filter(result => result.status === 'fulfilled' && result.value.success)
        .map(result => result.value.notificationId);
      
      const failedResults = results
        .filter(result => result.status === 'rejected' || (result.status === 'fulfilled' && !result.value.success));
  
      if (failedResults.length > 0) {
        console.error(`Failed to mark ${failedResults.length} notifications as read`);
      }
  
      // Update local state only for successfully marked notifications
      if (successfulResults.length > 0) {
        setNotifications(prev => prev.map(notification => 
          successfulResults.includes(notification.id)
            ? { ...notification, isRead: true }
            : notification
        ));
        
        console.log(`Successfully marked ${successfulResults.length} notifications as read`);
      }
  
      return successfulResults.length === unreadNotifications.length;
    } catch (error) {
      console.error('Error marking notifications as read:', error);
      return false;
    }
  };
  const openChat = async (conversation) => {
    const existingChat = openChats.find(chat => chat.id === conversation.id);
    if (!existingChat) {
      // Fetch messages for this conversation
      const messages = await fetchConversationMessages(conversation.otherUserId);
      console.log("messages : ", messages)
      // Transform messages to match the expected format and reverse order (latest first)
      const formattedMessages = messages
        .map(msg => ({
          id: msg.id,
          text: msg.isDeleted ? "*This message was deleted*" : msg.content,
          sender: msg.senderId === user.id ? 'You' : conversation.conversationName,
          timestamp: formatPostDate(msg.sentAt),
          sentAt: msg.sentAt,
          isMe: msg.senderId === user.id,
          isRead: msg.isRead,
          isDeleted: msg.isDeleted || false,
          isEdited: msg.isEdited || false,
          edited: msg.isEdited || false, // For backward compatibility
          // Add these properties for proper message handling
          canEdit: msg.senderId === user.id && !msg.isDeleted,
          canDelete: msg.senderId === user.id && !msg.isDeleted
        }))
        .reverse(); // Reverse to show latest messages at the bottom
  
      setOpenChats(prev => [...prev, { 
        ...conversation, 
        minimized: false,
        messages: formattedMessages,
        loading: false
      }]);
  
      // Mark messages as read if this conversation was unread
      if (conversation.lastMessageRead === false) {
        const success = await markConversationMessagesAsRead(conversation.id, formattedMessages);
        if (success) {
          // Update the conversation in the list to mark as read
          setConversations(prev => prev.map(conv => 
            conv.id === conversation.id 
              ? { ...conv, lastMessageRead: true }
              : conv
          ));
          
          // The message count will be decremented by the SignalR event listener
          // when each message is marked as read
        }
      }
    }
    setIsMessagingOpen(false);
  };

  const closeChat = (chatId) => {
    setOpenChats(prev => prev.filter(chat => chat.id !== chatId));
  };

  const minimizeChat = (chatId) => {
    setOpenChats(prev => prev.map(chat => 
      chat.id === chatId ? { ...chat, minimized: !chat.minimized } : chat
    ));
  };

  const sendMessage = async (chatId, receiverId, message) => {
    if (message.trim()) {
      const chat = openChats.find(c => c.id === chatId);
      if (!chat) return;

      try {
        // Send message via SignalR
        const success = await signalRSendMessage(receiverId.toString(), message);
        
        if (success) {
          console.log('Message sent successfully');
        } else {
          console.error('Failed to send message via SignalR');
        }

      } catch (err) {
        console.error("Failed to send message:", err.message);
      }
    }
  };

  const editMessage = async (messageId, newText) => {
    try {
      const success = await signalREditMessage(messageId, newText);

      if (success) {
        // Update local state
        setOpenChats(prev => prev.map(chat => ({
          ...chat,
          messages: chat.messages.map(msg => 
            msg.id === messageId 
              ? { ...msg, text: newText, edited: true }
              : msg
          )
        })));
      } else {
        console.error('Failed to edit message');
      }
    } catch (error) {
      console.error('Error editing message:', error);
    }
  };

  const deleteMessage = async (messageId) => {
    try {
        const success = await signalRDeleteMessage(messageId);

      if (success) {
        // Don't update local state here - let the SignalR event handle it
        // This ensures consistency across all connected clients
        console.log('Message deletion initiated');
      } else {
        console.error('Failed to delete message');
      }
    } catch (error) {
      console.error('Error deleting message:', error);
    }
  };
  const isNotificationOpenRef = useRef(isNotificationOpen);

// Update the ref whenever the state changes
useEffect(() => {
  isNotificationOpenRef.current = isNotificationOpen;
}, [isNotificationOpen]);
useEffect(() => {
  const handleClickOutside = async (event) => {
    if (profileRef.current && !profileRef.current.contains(event.target)) {
      setIsProfileOpen(false);
    }
    
    if (notificationRef.current && !notificationRef.current.contains(event.target)) {
      if (isNotificationOpenRef.current) {
        // Use a timeout to ensure the async operation has time to complete
        try {
          await handleCloseNotifications();
        } catch (error) {
          console.error('Error closing notifications:', error);
        }
      }
      setIsNotificationOpen(false);
      isNotificationOpenRef.current = false;
    }
    
    if (messagingRef.current && !messagingRef.current.contains(event.target)) {
      setIsMessagingOpen(false);
    }
  };

  document.addEventListener('mousedown', handleClickOutside);
  return () => document.removeEventListener('mousedown', handleClickOutside);
}, [notifications]); 
const handleCloseNotifications = async () => {
  try {
    // Get current unread notifications at the time of closing
    const currentUnreadNotifications = notifications.filter(n => !n.isRead);
    
    if (currentUnreadNotifications.length > 0) {
      console.log(`Attempting to mark ${currentUnreadNotifications.length} notifications as read...`);
      
      // Mark notifications as read
      await markNotificationsAsRead(currentUnreadNotifications);
      
      // Only reset count after successful marking
      setNotificationCount(0);
    }
  } catch (error) {
    console.error('Error in handleCloseNotifications:', error);
  }
};
  return (
    <div>
      <nav className="navbar">
        <div className="navbar-container" style={{display:"flex", justifyContent:"space-between", alignItems:"center"}}>
          {/* Logo */}
          <Link to={"/"} style={{textDecoration:"none"}}>
            <h1 style={{fontSize:"35px"}} className="navbar-logo">Social</h1>
          </Link>

          {/* Search Bar */}
          <SearchComponent/>

          {/* Navigation Icons */}
          <div className="navbar-actions">
          <Link to="/" className="navbar-icon-button" title="Home">
            <Home size={24} />
          </Link>


            {/* Messages */}
            <div style={{ position: 'relative' }} ref={messagingRef}>
              <button 
                className="navbar-icon-button navbar-icon-button-with-badge"
                onClick={() => {
                  loadConvos();
                  setIsMessagingOpen(!isMessagingOpen);
                }}
                title="Messages"
              >
                <MessageCircle size={24} />
                {messageCount > 0 && (
                  <span className="navbar-badge">
                    {messageCount > 99 ? '99+' : messageCount}
                  </span>
                )}
                {!isConnected && (
                  <div className="connection-indicator offline" title="Disconnected">
                    <Circle size={8} />
                  </div>
                )}
              </button>
              
              {isMessagingOpen && (
                <div className="dropdown messages-dropdown">
                  <div className="dropdown-header">
                    <h3 className="dropdown-title">Messages</h3>
                  </div>
                  
                  <div className="dropdown-content">
                    {loading ? (
                      <div className="loading-placeholder">Loading conversations...</div>
                    ) : (
                      Conversations.map(conversation => (
                        <div
                          key={conversation.id}
                          onClick={() => openChat(conversation)}
                          className="conversation-item"
                        >
                          <div className="conversation-avatar-wrapper">
                            <img 
                              src={MainURL + conversation.photoUrl}
                              alt={conversation.conversationName}
                              className="conversation-avatar"
                            />
                            {onlineUsers[conversation.otherUserId] && (
                              <div className="conversation-online-indicator" />
                            )}
                          </div>
                          <div className="conversation-content">
                            <div className="conversation-header">
                              <h4 className={`conversation-name ${conversation.lastMessageRead == false ? 'unread' : ''}`}>
                                {conversation.conversationName}
                              </h4>
                              <span className="conversation-time">{formatPostDate(conversation.lastMessageAt)}</span>
                            </div>
                            <p className={`conversation-message ${conversation.lastMessageRead == false ? 'unread' : ''}`}>
                              {conversation.lastMessageContent}
                            </p>
                          </div>
                          {conversation.lastMessageRead == false && (
                            <div className="conversation-unread-dot" />
                          )}
                        </div>
                      ))
                    )}
                  </div>
                </div>
              )}
            </div>

            <Link to="/create-post" className="navbar-icon-button" title="Create Post">
              <PlusSquare size={24} />
            </Link>


            {/* Notifications */}
            <div style={{ position: 'relative' }} ref={notificationRef}>
            <button 
                className="navbar-icon-button navbar-icon-button-with-badge"
                onClick={async () => {
                  const wasOpen = isNotificationOpen;
                  
                  if (wasOpen) {
                    // If closing, mark notifications as read first
                    await handleCloseNotifications();
                    setIsNotificationOpen(false);
                    isNotificationOpenRef.current = false;
                  } else {
                    // If opening, load notifications
                    setIsNotificationOpen(true);
                    isNotificationOpenRef.current = true;
                    await loadNotis();
                  }
                }}
                title="Notifications"
              >
                <Bell size={24} />
                {notificationCount > 0 && (
                  <span className="navbar-badge">{notificationCount}</span>
                )}
            </button>
              
              {isNotificationOpen && (
                <div className="dropdown notifications-dropdown">
                  <div className="dropdown-header">
                    <h3 className="dropdown-title">Notifications</h3>
                  </div>
                  <div className="dropdown-scrollable">
                    {notifications.map(notification => (
                      <Link
                        to={notification.postId ? `/PostDetails/${notification.postId}` : `/userDetails/${notification.fromUserId}`}
                        key={notification.id}
                        style={{ textDecoration: "none" }}
                      >
                        <div className={`notification-item ${!notification.isRead ? 'unread' : ''}`}>
                          <div className="notification-avatar">
                              <img
                                src={MainURL + notification.userImage}
                                alt="User Avatar"
                                className="avatar-img"
                                style={{ width: 32, height: 32, borderRadius: "50%" }}
                              />
                          </div>
                          <div className="notification-content">
                            <p className="notification-message">
                              <strong>{notification.user}</strong> {notification.message}
                            </p>
                            <span className="notification-time">{notification.time}</span>
                          </div>
                          {!notification.isRead && (
                            <div className="notification-unread-dot" />
                          )}
                        </div>
                      </Link>
                    ))}
                  </div>
                </div>
              )}
            </div>

            {/* Profile Menu */}
            <div style={{ position: 'relative' }} ref={profileRef}>
              <button 
                className="navbar-profile-button"
                onClick={() => setIsProfileOpen(!isProfileOpen)}
              >
                <img 
                  src={MainURL + user.profilePictureUrl}
                  alt="Profile" 
                  className="navbar-profile-image"
                />
              </button>
              
              {isProfileOpen && (
                <div className="dropdown profile-dropdown">
                  <div className="dropdown-header">
                    <div className="profile-info">
                      <img 
                        src={MainURL + user.profilePictureUrl}
                        alt="Profile" 
                        className="profile-avatar"
                      />
                      <div>
                        <h4 className="profile-name">
                          {user.firstName + " " + user.lastName}
                        </h4>
                        <p className="profile-username">@{user.username}</p>
                      </div>
                    </div>
                  </div>
                  
                  <div className="profile-menu">
                  <Link to={`/UserDetails/${user.id}`} className="profile-menu-item">
                    <User size={18} />
                    <span>Profile</span>
                  </Link>
                    <hr className="profile-menu-divider" />
                    <Link onClick={onLogoutClick} to={`/login`} className="profile-menu-item logout">
                      <LogOut size={18} />
                      <span>Logout</span>
                    </Link>
                  </div>
                </div>
              )}
            </div>
          </div>
        </div>
      </nav>

      {/* Chat Windows */}
      <div className="chat-windows-container">
        {openChats.map(chat => (
          <ChatWindow
            key={chat.id}
            chat={chat}
            messages={chat.messages || []}
            onlineUsers={onlineUsers}
            onClose={() => closeChat(chat.id)}
            onMinimize={() => minimizeChat(chat.id)}
            onSendMessage={(message) => sendMessage(chat.id, chat.otherUserId, message)}
            onEditMessage={editMessage}
            onDeleteMessage={deleteMessage}
          />
        ))}
      </div>
    </div>
  );
};

const ChatWindow = ({ chat, messages, onlineUsers, onClose, onMinimize, onSendMessage, onEditMessage, onDeleteMessage }) => {
    const [message, setMessage] = useState('');
    const [editingMessageId, setEditingMessageId] = useState(null);
    const [editingText, setEditingText] = useState('');
    const messagesEndRef = useRef(null);
  
    const scrollToBottom = () => {
      messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
    };
  
    useEffect(() => {
      scrollToBottom();
    }, [messages]);
  
    const handleSend = (e) => {
      e.preventDefault();
      if (message.trim()) {
        onSendMessage(message);
        setMessage('');
      }
    };
  
    const handleEdit = (messageId, currentText) => {
      setEditingMessageId(messageId);
      setEditingText(currentText);
    };
  
    const handleSaveEdit = (messageId) => {
      if (editingText.trim() && editingText !== messages.find(m => m.id === messageId)?.text) {
        onEditMessage(messageId, editingText.trim());
      }
      setEditingMessageId(null);
      setEditingText('');
    };
  
    const handleCancelEdit = () => {
      setEditingMessageId(null);
      setEditingText('');
    };
  
    const handleDelete = (messageId) => {
      if (window.confirm('Are you sure you want to delete this message?')) {
        onDeleteMessage(messageId);
      }
    };
  
    const canEditOrDelete = (msg) => {
      const rawTimestamp = msg.sentAt || msg.originalTimestamp; 
      if (!rawTimestamp) return false;
    
      const utcString = rawTimestamp.replace(' ', 'T').split('.')[0] + 'Z';
      const messageTime = new Date(utcString);
      const oneHourAgo = new Date(Date.now() - 60 * 60 * 1000);
      return msg.isMe &&  !msg.isDeleted && messageTime > oneHourAgo;
    };
    
    
    
    
  
    const getMessageStatusIcon = (msg) => {
      if (!msg.isMe) return null;
      
      if (msg.isRead) {
        return (
          <div className="message-status">
            <div className="message-status-icon" style={{ color: '#10b981' }}>✓✓</div>
            <span className="message-status-text">Read</span>
          </div>
        );
      } else {
        return (
          <div className="message-status">
            <div className="message-status-icon" style={{ color: '#6b7280' }}>✓</div>
            <span className="message-status-text">Sent</span>
          </div>
        );
      }
    };
  console.log(messages)
    return (
      <div className={`chat-window ${chat.minimized ? 'minimized' : 'expanded'}`}>
        {/* Chat Header */}
        <div 
          className={`chat-header ${chat.minimized ? 'minimized' : 'expanded'}`}
          onClick={chat.minimized ? onMinimize : undefined}
        >
          <div className="chat-user-info">
            <div className="chat-avatar-wrapper">
              <img 
                src={MainURL + chat.photoUrl}
                alt={chat.conversationName}
                className="chat-avatar"
              />
              {onlineUsers[chat.otherUserId] && (
                <div className="chat-online-indicator" />
              )}
            </div>
            <div>
              <h4 className="chat-user-name">{chat.conversationName}</h4>
              <p className={`chat-user-status ${onlineUsers[chat.otherUserId] ? 'online' : 'offline'}`}>
                {onlineUsers[chat.otherUserId] ? 'Active now' : 'Offline'}
              </p>
            </div>
          </div>
          
          <div className="chat-controls">
            <button 
              onClick={onMinimize}
              className="chat-control-button"
              title={chat.minimized ? "Expand" : "Minimize"}
            >
              <Minus size={16} />
            </button>
            <button 
              onClick={onClose}
              className="chat-control-button"
              title="Close"
            >
              <X size={16} />
            </button>
          </div>
        </div>
  
        {!chat.minimized && (
          <>
            {/* Messages */}
            <div className="chat-messages">
              {chat.loading ? (
                <div className="chat-loading">Loading messages...</div>
              ) : messages.length === 0 ? (
                <div className="chat-empty">No messages yet. Start the conversation!</div>
              ) : (
                messages.map(msg => (
                  <div key={msg.id} className={`message-wrapper ${msg.isMe ? 'sent' : 'received'}`}>
                    <div className="message-content">
                      <div className={`message-bubble ${msg.isMe ? 'sent' : 'received'} ${editingMessageId === msg.id ? 'editing' : ''} ${msg.isDeleted ? 'deleted' : ''}`}>
                        {editingMessageId === msg.id ? (
                          <div className="message-edit-form">
                            <textarea
                              value={editingText}
                              onChange={(e) => setEditingText(e.target.value)}
                              className="message-edit-input"
                              autoFocus
                              onKeyDown={(e) => {
                                if (e.key === 'Enter' && !e.shiftKey) {
                                  e.preventDefault();
                                  handleSaveEdit(msg.id);
                                } else if (e.key === 'Escape') {
                                  handleCancelEdit();
                                }
                              }}
                            />
                            <div className="message-edit-actions">
                              <button 
                                className="message-edit-btn save"
                                onClick={() => handleSaveEdit(msg.id)}
                              >
                                Save
                              </button>
                              <button 
                                className="message-edit-btn cancel"
                                onClick={handleCancelEdit}
                              >
                                Cancel
                              </button>
                            </div>
                          </div>
                        ) : (
                          <div className={`message-text ${msg.isDeleted ? 'deleted' : ''}`}>
                            {msg.text}
                            {msg.edited && !msg.isDeleted && <span style={{ opacity: 0.7, fontSize: '11px' }}> (edited)</span>}
                          </div>
                        )}
                      </div>
                      
                      {/* Message actions for sent messages that haven't been read and aren't deleted */}
                      {canEditOrDelete(msg) && editingMessageId !== msg.id && (
                        <div className="message-actions">
                          <button 
                            className="message-action-btn edit"
                            onClick={() => handleEdit(msg.id, msg.text)}
                            title="Edit message"
                          >
                            Edit
                          </button>
                          <button 
                            className="message-action-btn delete"
                            onClick={() => handleDelete(msg.id)}
                            title="Delete message"
                          >
                            Delete
                          </button>
                        </div>
                      )}
                    </div>
                    
                    {/* Timestamp below the message */}
                    <div className="message-time">{msg.timestamp}</div>
                    
                    {/* Message status for sent messages (don't show for deleted messages) */}
                    {!msg.isDeleted && getMessageStatusIcon(msg)}
                  </div>
                ))
              )}
              <div ref={messagesEndRef} />
            </div>
  
            {/* Message Input */}
            <form onSubmit={handleSend} className="chat-input-form">
              <input
                type="text"
                value={message}
                onChange={(e) => setMessage(e.target.value)}
                placeholder="Type a message..."
                className="chat-input"
              />
              <button type="submit" className="chat-send-button">
                <Send size={16} />
              </button>
            </form>
          </>
        )}
      </div>
    );
};

export default Navbar;