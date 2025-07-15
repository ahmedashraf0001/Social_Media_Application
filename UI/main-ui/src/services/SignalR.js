import React, { createContext, useContext, useEffect, useState, useCallback } from 'react';
import * as signalR from '@microsoft/signalr';
import { useSelector } from "react-redux";
import { getAccessToken } from '../utils/auth';
const SignalRContext = createContext();

export const useSignalR = () => {
    const context = useContext(SignalRContext);
    if (!context) {
        throw new Error('useSignalR must be used within a SignalRProvider');
    }
    return context;
};

export const SignalRProvider = ({ children }) => {
    const [connection, setConnection] = useState(null);
    const [isConnected, setIsConnected] = useState(false);
    const [onlineUsers, setOnlineUsers] = useState({});
    const [currentUser, setCurrentUser] = useState(null);
    const [currentConversation, setCurrentConversation] = useState(null);
    const user = useSelector(state => state.user);
    const initializeConnection = useCallback(async () => {

        setCurrentUser(user);

        try {
            const newConnection = new signalR.HubConnectionBuilder()
                .withUrl("https://localhost:7242/chat", {
                    accessTokenFactory: () => getAccessToken()
                })
                .withAutomaticReconnect()
                .build();

            setupSignalREvents(newConnection);

            await newConnection.start();
            console.log("SignalR Connected");
            setConnection(newConnection);
            setIsConnected(true);
        } catch (err) {
            console.error("SignalR Connection Error: ", err);
            setIsConnected(false);
        }
    }, []);

    // Setup SignalR event handlers
    const setupSignalREvents = useCallback((conn) => {
        // Receive a new message
        conn.on("ReceiveMessage", (message) => {
            console.log("New message received:", message);

            // Play notification sound
            if (message.senderId !== currentUser?.id && document.hidden) {
                const notificationSound = new Audio('pop_1lzEdx1.mp3');
                notificationSound.volume = 0.4;
                notificationSound.play().catch(e => console.warn("Sound playback failed:", e));
            }

            // Emit custom event for components to listen to
            window.dispatchEvent(new CustomEvent('newMessage', { detail: message }));
        });

        // Message edited
        conn.on("MessageEdited", (message) => {
            console.log("Message edited:", message);
            window.dispatchEvent(new CustomEvent('messageEdited', { detail: message }));
        });

        // Message deleted
        conn.on("MessageDeleted", (data) => {
            console.log("Message deleted:", data);
            window.dispatchEvent(new CustomEvent('messageDeleted', { detail: data }));
        });

        conn.on("MessageMarkedAsRead", (message) => {
            console.log("Message Marked:", message);
            window.dispatchEvent(new CustomEvent('messageMarked', { detail: message }));
        });
        // User typing indicator
        conn.on("UserIsTyping", (message) => {
            window.dispatchEvent(new CustomEvent('userTyping', { detail: message }));
        });
        conn.on("ReceiveNotification", (message) => {
            window.dispatchEvent(new CustomEvent('notificationRecieved', { detail: message }));
          });
        // Online/Offline status
        conn.on("NotifyOnline", (userId, conversationId) => {
            console.log(`User ${userId} is online in conversation ${conversationId}`);
            setOnlineUsers(prev => ({ ...prev, [userId]: true }));
            window.dispatchEvent(new CustomEvent('userOnline', { 
                detail: { userId, conversationId } 
            }));
        });

        conn.on("NotifyOffline", (userId, conversationId) => {
            console.log(`User ${userId} is offline in conversation ${conversationId}`);
            setOnlineUsers(prev => ({ ...prev, [userId]: false }));
            window.dispatchEvent(new CustomEvent('userOffline', { 
                detail: { userId, conversationId } 
            }));
        });
        // Connection events
        conn.onreconnecting(error => {
            console.log("Connection lost, trying to reconnect...");
            setIsConnected(false);
            window.dispatchEvent(new CustomEvent('connectionReconnecting', { detail: error }));
        });

        conn.onreconnected(connectionId => {
            console.log("Connection reestablished");
            setIsConnected(true);
            window.dispatchEvent(new CustomEvent('connectionReconnected', { detail: connectionId }));
        });

        conn.onclose(error => {
            console.log("Connection closed");
            setIsConnected(false);
            setConnection(null);
            window.dispatchEvent(new CustomEvent('connectionClosed', { detail: error }));
        });
    }, [currentUser]);

    // Send message function
    const sendMessage = useCallback(async (recId, finalcontent) => {
        if (connection && isConnected) {
            try {
                const messageData = {
                    receiverId: recId,
                    content: finalcontent,
                };
                await connection.invoke("SendMessage", messageData);
                return true;
            } catch (error) {
                console.error("Failed to send message:", error);
                return false;
            }
        }
        return false;
    }, [connection, isConnected]);

    // Edit message function
    const editMessage = useCallback(async (messageId, content) => {
        if (connection && isConnected) {
            try {
                await connection.invoke("EditMessage", messageId, content);
                return true;
            } catch (error) {
                console.error("Failed to edit message:", error);
                return false;
            }
        }
        return false;
    }, [connection, isConnected]);
    const markMessageAsRead = useCallback(async (messageId) => {
        if (connection && isConnected) {
            try {
                await connection.invoke("MarkMessageAsRead", messageId);
                return true;
            } catch (error) {
                console.error("Failed to Mark message:", error);
                return false;
            }
        }
        return false;
    }, [connection, isConnected]);

    // Delete message function
    const deleteMessage = useCallback(async (messageId) => {
        if (connection && isConnected) {
            try {
                await connection.invoke("DeleteMessage", messageId);
                return true;
            } catch (error) {
                console.error("Failed to delete message:", error);
                return false;
            }
        }
        return false;
    }, [connection, isConnected]);

    // Send typing indicator
    const sendTypingIndicator = useCallback(async (conversationId) => {
        if (connection && isConnected) {
            try {
                await connection.invoke("SendTypingIndicator", conversationId);
            } catch (error) {
                console.error("Failed to send typing indicator:", error);
            }
        }
    }, [connection, isConnected]);

    // Set current conversation (client-side only)
    const setCurrentConversationId = useCallback((conversationId) => {
        setCurrentConversation(conversationId ? { id: conversationId } : null);
    }, []);

    // Initialize connection on mount
    useEffect(() => {
        initializeConnection();

        // Cleanup on unmount
        return () => {
            if (connection) {
                connection.stop();
            }
        };
    }, [initializeConnection]);

    // Context value
    const value = {
        connection,
        isConnected,
        onlineUsers,
        currentUser,
        currentConversation,
        sendMessage,
        editMessage,
        markMessageAsRead,
        deleteMessage,
        sendTypingIndicator,
        setCurrentConversationId,
        initializeConnection
    };

    return (
        <SignalRContext.Provider value={value}>
            {children}
        </SignalRContext.Provider>
    );
};

// Custom hook for listening to SignalR events
export const useSignalREvent = (eventName, callback) => {
    useEffect(() => {
        const handleEvent = (event) => {
            callback(event.detail);
        };

        window.addEventListener(eventName, handleEvent);
        return () => window.removeEventListener(eventName, handleEvent);
    }, [eventName, callback]);
};

export default SignalRContext;