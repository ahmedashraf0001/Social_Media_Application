import {
  Home,
  PlusSquare,
  MessageCircle,
  User,
  LogOut,
  Bell,
  Circle,
} from "lucide-react";
import React, { useState, useRef, useEffect } from "react";
import "./styles/navbar.css";
import { useDispatch, useSelector } from "react-redux";
import { getNotis, MainURL } from "../URLS";
import { Link, useNavigate } from "react-router-dom";
import { getContact } from "../URLS";
import { formatPostDate } from "../utils/date-helper";
import { useSignalR, useSignalREvent } from "../services/SignalR";
import SearchComponent from "./search";
import { getAccessToken, handleLogout } from "../utils/auth";
import ChatWindow from "./messagingPopup";

const Navbar = () => {
  const user = useSelector((state) => state.user);

  const {
    sendMessage: signalRSendMessage,
    editMessage: signalREditMessage,
    deleteMessage: signalRDeleteMessage,
    isConnected,
    onlineUsers,
    markMessageAsRead: signalRMarkMessageAsRead,
  } = useSignalR();

  const [isProfileOpen, setIsProfileOpen] = useState(false);
  const [isNotificationOpen, setIsNotificationOpen] = useState(false);
  const [isMessagingOpen, setIsMessagingOpen] = useState(false);
  const [openChats, setOpenChats] = useState([]);
  const [searchQuery, setSearchQuery] = useState("");
  const [notifications, setNotifications] = useState([]);
  const [notificationCount, setNotificationCount] = useState(0);
  const [messageCount, setMessageCount] = useState(0);
  const [loading, setLoading] = useState(true);
  const [Conversations, setConversations] = useState([]);

  const navigate = useNavigate();
  const dispatch = useDispatch();

  const isNotificationOpenRef = useRef(isNotificationOpen);
  const profileRef = useRef(null);
  const notificationRef = useRef(null);
  const messagingRef = useRef(null);

  useEffect(() => {
    const handleClickOutside = async (event) => {
      if (profileRef.current && !profileRef.current.contains(event.target)) {
        setIsProfileOpen(false);
      }
      if (
        messagingRef.current &&
        !messagingRef.current.contains(event.target)
      ) {
        setIsMessagingOpen(false);
      }
    };
    loadConvos();
    loadNotis();
    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);
  useEffect(() => {
    isNotificationOpenRef.current = isNotificationOpen;
  }, [isNotificationOpen]);
  useEffect(() => {
    const handleClickOutside = async (event) => {
      if (profileRef.current && !profileRef.current.contains(event.target)) {
        setIsProfileOpen(false);
      }

      if (
        notificationRef.current &&
        !notificationRef.current.contains(event.target)
      ) {
        if (isNotificationOpenRef.current) {
          try {
            await handleCloseNotifications();
          } catch (error) {
            console.error("Error closing notifications:", error);
          }
        }
        setIsNotificationOpen(false);
        isNotificationOpenRef.current = false;
      }

      if (
        messagingRef.current &&
        !messagingRef.current.contains(event.target)
      ) {
        setIsMessagingOpen(false);
      }
    };

    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, [notifications]);

  const onLogoutClick = async () => {
    const onSuccess = (sucess) => 
    {
      console.log("Logged in successfully!")
    };
    const onError = (error) => {
      console.error("Logout failed:", error);
      alert("Logout failed. Please try again.");
    };

    await handleLogout(dispatch, onSuccess, onError, navigate);
  };

  useSignalREvent("newMessage", async (message) => {
    setOpenChats((prev) =>
      prev.map((chat) => {
        if (chat.id === message.conversationId) {
          const newMessage = {
            id: message.id,
            text: message.content,
            sender:
              message.senderId === user.id ? "You" : chat.conversationName,
            timestamp: formatPostDate(message.sentAt),
            IsEdited: message.IsEdited,
            IsDeleted: message.IsDeleted,
            isMe: message.senderId === user.id,
            isRead: message.isRead,
          };
          return {
            ...chat,
            messages: [...chat.messages, newMessage],
          };
        }
        return chat;
      })
    );
    if (
      message.senderId !== user.id &&
      !openChats.find((e) => e.id == message.conversationId)
    ) {
      setMessageCount((prev) => prev + 1);
    }
    if (openChats.find((e) => e.id == message.conversationId)) {
      await signalRMarkMessageAsRead(message.id);
    }
    setConversations((prev) =>
      prev.map((conv) => {
        if (conv.id === message.conversationId) {
          return {
            ...conv,
            lastMessageContent: message.content,
            lastMessageAt: message.sentAt,
            lastMessageRead: message.senderId === user.id ? true : false,
          };
        }
        return conv;
      })
    );
  });
  useSignalREvent("messageMarked", (message) => {
    setOpenChats((prev) =>
      prev.map((chat) => ({
        ...chat,
        messages: chat.messages.map((msg) =>
          msg.id === message.id ? { ...msg, isRead: true } : msg
        ),
      }))
    );

    setConversations((prev) =>
      prev.map((conv) => {
        if (conv.id === message.conversationId) {
          return {
            ...conv,
            lastMessageRead: true,
          };
        }
        return conv;
      })
    );

    if (message.senderId !== user.id) {
      setMessageCount((prev) => Math.max(0, prev - 1));
    }
  });
  useSignalREvent("messageEdited", (message) => {
    setOpenChats((prev) =>
      prev.map((chat) => ({
        ...chat,
        messages: chat.messages.map((msg) =>
          msg.id === message.id
            ? {
                ...msg,
                text: message.content,
                edited: true,
                ...(message.editedAt && { editedAt: message.editedAt }),
              }
            : msg
        ),
      }))
    );

    setConversations((prev) =>
      prev.map((conv) => {
        if (conv.id === message.conversationId) {
          const chat = openChats.find((c) => c.id === message.conversationId);
          if (chat) {
            const lastMessage = chat.messages[chat.messages.length - 1];
            if (lastMessage && lastMessage.id === message.id) {
              return {
                ...conv,
                lastMessageContent: message.content,
              };
            }
          }
        }
        return conv;
      })
    );
  });
  useSignalREvent("messageDeleted", (data) => {
    setOpenChats((prev) =>
      prev.map((chat) => ({
        ...chat,
        messages: chat.messages.map((msg) =>
          msg.id === data.messageId
            ? {
                ...msg,
                text: "*This message was deleted*",
                isDeleted: true,
                canEdit: false,
                canDelete: false,
              }
            : msg
        ),
      }))
    );

    setConversations((prev) =>
      prev.map((conv) => {
        if (conv.id === data.conversationId && conv.lastMessageContent) {
          const chat = openChats.find((c) => c.id === data.conversationId);
          if (chat) {
            const lastMessage = chat.messages[chat.messages.length - 1];
            if (lastMessage && lastMessage.id === data.messageId) {
              return {
                ...conv,
                lastMessageContent: "*This message was deleted*",
              };
            }
          }
        }
        return conv;
      })
    );
  });
  useSignalREvent("notificationRecieved", (notificationData) => {
    setNotificationCount((prev) => prev + 1);
    setNotifications((prev) => [notificationData, ...prev]);
  });

  const loadConvos = async () => {
    try {
      const res = await fetch(`${getContact}/1/1000`, {
        headers: { Authorization: "Bearer " + getAccessToken() },
      });
      if (!res.ok) throw new Error(`HTTP error ${res.status}`);
      const data = await res.json();
      setConversations(data);
      setMessageCount(data.reduce((total, e) => total + e.unreadMessages, 0));
    } catch (err) {
      console.error("Failed to fetch Conversations:", err.message);
    } finally {
      setLoading(false);
    }
  };
  const loadNotis = async () => {
    try {
      const res = await fetch(getNotis, {
        headers: { Authorization: "Bearer " + getAccessToken() },
      });
      if (!res.ok) throw new Error(`HTTP error ${res.status}`);
      const data = await res.json();
      setNotifications(data);
      setNotificationCount(data.filter((n) => n.isRead === false).length);
    } catch (err) {
      console.error("Failed to fetch Notifications:", err.message);
    } finally {
      setLoading(false);
    }
  };
  const fetchConversationMessages = async (otherUserId) => {
    try {
      const res = await fetch(
        `${MainURL}/api/Messaging/conversations/Between/${otherUserId}/1/1000`,
        {
          headers: { Authorization: "Bearer " + getAccessToken() },
        }
      );
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
      const unreadMessages = messages.filter((msg) => !msg.isRead && !msg.isMe);

      if (unreadMessages.length === 0) {
        return true;
      }

      setMessageCount((prev) => Math.max(0, prev - unreadMessages.length));

      let failedCount = 0;

      for (const message of unreadMessages) {
        try {
          await signalRMarkMessageAsRead(message.id);
        } catch (error) {
          console.error(`Failed to mark message ${message.id} as read:`, error);
          failedCount++;
        }
      }

      if (failedCount > 0) {
        setMessageCount((prev) => prev + failedCount);
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
      if (!unreadNotifications || unreadNotifications.length === 0) {
        return true;
      }
      const markPromises = unreadNotifications.map((notification) =>
        fetch(`${MainURL}/api/Notification/MarkAsRead/${notification.id}`, {
          method: "PUT",
          headers: {
            Authorization: "Bearer " + getAccessToken(),
            "Content-Type": "application/json",
          },
        })
          .then((response) => {
            if (!response.ok) {
              throw new Error(
                `Failed to mark notification ${notification.id} as read: ${response.status}`
              );
            }
            return { success: true, notificationId: notification.id };
          })
          .catch((error) => {
            console.error(
              `Error marking notification ${notification.id}:`,
              error
            );
            return { success: false, notificationId: notification.id, error };
          })
      );

      const results = await Promise.allSettled(markPromises);

      const successfulResults = results
        .filter(
          (result) => result.status === "fulfilled" && result.value.success
        )
        .map((result) => result.value.notificationId);

      const failedResults = results.filter(
        (result) =>
          result.status === "rejected" ||
          (result.status === "fulfilled" && !result.value.success)
      );

      if (failedResults.length > 0) {
        console.error(
          `Failed to mark ${failedResults.length} notifications as read`
        );
      }

      if (successfulResults.length > 0) {
        setNotifications((prev) =>
          prev.map((notification) =>
            successfulResults.includes(notification.id)
              ? { ...notification, isRead: true }
              : notification
          )
        );
      }

      return successfulResults.length === unreadNotifications.length;
    } catch (error) {
      console.error("Error marking notifications as read:", error);
      return false;
    }
  };

  const openChat = async (conversation) => {
    const existingChat = openChats.find((chat) => chat.id === conversation.id);
    if (!existingChat) {
      const messages = await fetchConversationMessages(
        conversation.otherUserId
      );
      const formattedMessages = messages
        .map((msg) => ({
          id: msg.id,
          text: msg.isDeleted ? "*This message was deleted*" : msg.content,
          sender:
            msg.senderId === user.id ? "You" : conversation.conversationName,
          timestamp: formatPostDate(msg.sentAt),
          sentAt: msg.sentAt,
          isMe: msg.senderId === user.id,
          isRead: msg.isRead,
          isDeleted: msg.isDeleted || false,
          isEdited: msg.isEdited || false,
          edited: msg.isEdited || false,
          canEdit: msg.senderId === user.id && !msg.isDeleted,
          canDelete: msg.senderId === user.id && !msg.isDeleted,
        }))
        .reverse();

      setOpenChats((prev) => [
        ...prev,
        {
          ...conversation,
          minimized: false,
          messages: formattedMessages,
          loading: false,
        },
      ]);

      if (conversation.lastMessageRead === false) {
        const success = await markConversationMessagesAsRead(
          conversation.id,
          formattedMessages
        );
        if (success) {
          setConversations((prev) =>
            prev.map((conv) =>
              conv.id === conversation.id
                ? { ...conv, lastMessageRead: true }
                : conv
            )
          );
        }
      }
    }
    setIsMessagingOpen(false);
  };
  const closeChat = (chatId) => {
    setOpenChats((prev) => prev.filter((chat) => chat.id !== chatId));
  };
  const minimizeChat = (chatId) => {
    setOpenChats((prev) =>
      prev.map((chat) =>
        chat.id === chatId ? { ...chat, minimized: !chat.minimized } : chat
      )
    );
  };

  const sendMessage = async (chatId, receiverId, message) => {
    if (message.trim()) {
      const chat = openChats.find((c) => c.id === chatId);
      if (!chat) return;

      try {
        const success = await signalRSendMessage(
          receiverId.toString(),
          message
        );

        if (success) {
          console.log("Message sent successfully");
        } else {
          console.error("Failed to send message via SignalR");
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
        setOpenChats((prev) =>
          prev.map((chat) => ({
            ...chat,
            messages: chat.messages.map((msg) =>
              msg.id === messageId
                ? { ...msg, text: newText, edited: true }
                : msg
            ),
          }))
        );
      } else {
        console.error("Failed to edit message");
      }
    } catch (error) {
      console.error("Error editing message:", error);
    }
  };
  const deleteMessage = async (messageId) => {
    try {
      const success = await signalRDeleteMessage(messageId);
      if (success) {
        console.log("Message deletion initiated");
      } else {
        console.error("Failed to delete message");
      }
    } catch (error) {
      console.error("Error deleting message:", error);
    }
  };

  const handleCloseNotifications = async () => {
    try {
      const currentUnreadNotifications = notifications.filter((n) => !n.isRead);

      if (currentUnreadNotifications.length > 0) {
        console.log(
          `Attempting to mark ${currentUnreadNotifications.length} notifications as read...`
        );
        await markNotificationsAsRead(currentUnreadNotifications);
        setNotificationCount(0);
      }
    } catch (error) {
      console.error("Error in handleCloseNotifications:", error);
    }
  };
  return (
    <div>
      <nav className="navbar">
        <div
          className="navbar-container"
          style={{
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
          }}
        >
          {/* Logo */}
          <Link to={"/"} style={{ textDecoration: "none" }}>
            <h1 style={{ fontSize: "35px" }} className="navbar-logo">
              Social
            </h1>
          </Link>

          {/* Search Bar */}
          <SearchComponent />

          {/* Navigation Icons */}
          <div className="navbar-actions">
            <Link to="/" className="navbar-icon-button" title="Home">
              <Home size={24} />
            </Link>

            {/* Messages */}
            <div style={{ position: "relative" }} ref={messagingRef}>
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
                    {messageCount > 99 ? "99+" : messageCount}
                  </span>
                )}
                {!isConnected && (
                  <div
                    className="connection-indicator offline"
                    title="Disconnected"
                  >
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
                      <div className="loading-placeholder">
                        Loading conversations...
                      </div>
                    ) : (
                      Conversations.map((conversation) => (
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
                              <h4
                                className={`conversation-name ${
                                  conversation.lastMessageRead == false
                                    ? "unread"
                                    : ""
                                }`}
                              >
                                {conversation.conversationName}
                              </h4>
                              <span className="conversation-time">
                                {formatPostDate(conversation.lastMessageAt)}
                              </span>
                            </div>
                            <p
                              className={`conversation-message ${
                                conversation.lastMessageRead == false
                                  ? "unread"
                                  : ""
                              }`}
                            >
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

            <Link
              to="/create-post"
              className="navbar-icon-button"
              title="Create Post"
            >
              <PlusSquare size={24} />
            </Link>

            {/* Notifications */}
            <div style={{ position: "relative" }} ref={notificationRef}>
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
                    {notifications.map((notification) => (
                      <Link
                        to={
                          notification.postId
                            ? `/PostDetails/${notification.postId}`
                            : `/userDetails/${notification.fromUserId}`
                        }
                        key={notification.id}
                        style={{ textDecoration: "none" }}
                      >
                        <div
                          className={`notification-item ${
                            !notification.isRead ? "unread" : ""
                          }`}
                        >
                          <div className="notification-avatar">
                            <img
                              src={MainURL + notification.userImage}
                              alt="User Avatar"
                              className="avatar-img"
                              style={{
                                width: 32,
                                height: 32,
                                borderRadius: "50%",
                              }}
                            />
                          </div>
                          <div className="notification-content">
                            <p className="notification-message">
                              <strong>{notification.user}</strong>{" "}
                              {notification.message}
                            </p>
                            <span className="notification-time">
                              {notification.time}
                            </span>
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
            <div style={{ position: "relative" }} ref={profileRef}>
              <button
                className="navbar-profile-button"
                onClick={() => setIsProfileOpen(!isProfileOpen)}
              >
                    {user.profilePictureUrl ? (
                      <img
                        src={MainURL + user.profilePictureUrl}
                        alt="Profile"
                        className="profile-avatar"
                     />
                    ) : (
                      <div className="avatar-placeholder-search">
                        <span>👤</span>
                      </div>
                    )}
              </button>

              {isProfileOpen && (
                <div className="dropdown profile-dropdown">
                  <div className="dropdown-header">
                    <div className="profile-info">
                    {user.profilePictureUrl ? (
                      <img
                        src={MainURL + user.profilePictureUrl}
                        alt="Profile"
                        className="profile-avatar"
                     />
                    ) : (
                      <div className="avatar-placeholder-search">
                        <span>👤</span>
                      </div>
                    )}
                      <div>
                        <h4 className="profile-name">
                          {user.firstName + " " + user.lastName}
                        </h4>
                        <p className="profile-username">@{user.username}</p>
                      </div>
                    </div>
                  </div>

                  <div className="profile-menu">
                    <Link
                      to={`/UserDetails/${user.id}`}
                      className="profile-menu-item"
                    >
                      <User size={18} />
                      <span>Profile</span>
                    </Link>
                    <hr className="profile-menu-divider" />
                    <Link
                      onClick={onLogoutClick}
                      to={`/login`}
                      className="profile-menu-item logout"
                    >
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
        {openChats.map((chat) => (
          <ChatWindow
            key={chat.id}
            chat={chat}
            messages={chat.messages || []}
            onlineUsers={onlineUsers}
            onClose={() => closeChat(chat.id)}
            onMinimize={() => minimizeChat(chat.id)}
            onSendMessage={(message) =>
              sendMessage(chat.id, chat.otherUserId, message)
            }
            onEditMessage={editMessage}
            onDeleteMessage={deleteMessage}
          />
        ))}
      </div>
    </div>
  );
};

export default Navbar;
