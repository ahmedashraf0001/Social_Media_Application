import {
    Send,
    Minus,
    X,
  } from "lucide-react";
import React, { useState, useRef, useEffect } from "react";
import { MainURL } from "../URLS";
import "./styles/messagepopup.css"

const ChatWindow = ({
    chat,
    messages,
    onlineUsers,
    onClose,
    onMinimize,
    onSendMessage,
    onEditMessage,
    onDeleteMessage,
  }) => {
    const [message, setMessage] = useState("");
    const [editingMessageId, setEditingMessageId] = useState(null);
    const [editingText, setEditingText] = useState("");
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
        setMessage("");
      }
    };
  
    const handleEdit = (messageId, currentText) => {
      setEditingMessageId(messageId);
      setEditingText(currentText);
    };
  
    const handleSaveEdit = (messageId) => {
      if (
        editingText.trim() &&
        editingText !== messages.find((m) => m.id === messageId)?.text
      ) {
        onEditMessage(messageId, editingText.trim());
      }
      setEditingMessageId(null);
      setEditingText("");
    };
  
    const handleCancelEdit = () => {
      setEditingMessageId(null);
      setEditingText("");
    };
  
    const handleDelete = (messageId) => {
        onDeleteMessage(messageId);  
    };
  
    const canEditOrDelete = (msg) => {
      const rawTimestamp = msg.sentAt || msg.originalTimestamp;
      if (!rawTimestamp) return false;
  
      const utcString = rawTimestamp.replace(" ", "T").split(".")[0] + "Z";
      const messageTime = new Date(utcString);
      const oneHourAgo = new Date(Date.now() - 60 * 60 * 1000);
      return msg.isMe && !msg.isDeleted && messageTime > oneHourAgo;
    };
  
    const getMessageStatusIcon = (msg) => {
      if (!msg.isMe) return null;
  
      if (msg.isRead) {
        return (
          <div className="message-status">
            <div className="message-status-icon" style={{ color: "#10b981" }}>
              ✓✓
            </div>
            <span className="message-status-text">Read</span>
          </div>
        );
      } else {
        return (
          <div className="message-status">
            <div className="message-status-icon" style={{ color: "#6b7280" }}>
              ✓
            </div>
            <span className="message-status-text">Sent</span>
          </div>
        );
      }
    };
    
    return (
      <div className={`chat-window ${chat.minimized ? "minimized" : "expanded"}`}>
        {/* Chat Header */}
        <div
          className={`chat-header ${chat.minimized ? "minimized" : "expanded"}`}
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
              <p
                className={`chat-user-status ${
                  onlineUsers[chat.otherUserId] ? "online" : "offline"
                }`}
              >
                {onlineUsers[chat.otherUserId] ? "Active now" : "Offline"}
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
                <div className="chat-empty">
                  No messages yet. Start the conversation!
                </div>
              ) : (
                messages.map((msg) => (
                  <div
                    key={msg.id}
                    className={`message-wrapper ${
                      msg.isMe ? "sent" : "received"
                    }`}
                  >
                    <div className="message-content">
                      <div
                        className={`message-bubble ${
                          msg.isMe ? "sent" : "received"
                        } ${editingMessageId === msg.id ? "editing" : ""} ${
                          msg.isDeleted ? "deleted" : ""
                        }`}
                      >
                        {editingMessageId === msg.id ? (
                          <div className="message-edit-form">
                            <textarea
                              value={editingText}
                              onChange={(e) => setEditingText(e.target.value)}
                              className="message-edit-input"
                              autoFocus
                              onKeyDown={(e) => {
                                if (e.key === "Enter" && !e.shiftKey) {
                                  e.preventDefault();
                                  handleSaveEdit(msg.id);
                                } else if (e.key === "Escape") {
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
                          <div
                            className={`message-text ${
                              msg.isDeleted ? "deleted" : ""
                            }`}
                          >
                            {msg.text}
                            {msg.edited && !msg.isDeleted && (
                              <span style={{ opacity: 0.7, fontSize: "11px" }}>
                                {" "}
                                (edited)
                              </span>
                            )}
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
export default ChatWindow;
  