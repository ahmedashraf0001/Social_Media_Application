import "../pages/styles/feed.css";
import { useEffect, useState } from "react";
import { getContact } from "../URLS";
import { useSignalR } from "../services/SignalR";
import { MainURL } from "../URLS";
import { Link } from "react-router-dom";
import { getAccessToken } from "../utils/auth";
import FailedToLoad from "../utils/loadError";
function ContactsSidebar() {
  const { onlineUsers } = useSignalR();

  const [friends, setFriends] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);

  // Fetch contacts on mount
  useEffect(() => {
    const loadContact = async () => {
      try {
        const res = await fetch(`${getContact}/1/12`, {
          headers: { Authorization: "Bearer " + getAccessToken() },
        });
        if (!res.ok) throw new Error(`HTTP error ${res.status}`);
        const data = await res.json();
        setFriends(data);
      } catch (err) {
        console.error("Failed to fetch Contact:", err.message);
        if (err.message.includes("404")) {
          setError(404);
        }
      } finally {
        setLoading(false);
      }
    };
    loadContact();
  }, []);

  if (error == true) {
    return (
      <div className="main-content">
        <FailedToLoad />
      </div>
    );
  }

  const onlineFriends = friends
    .filter((friend) => onlineUsers[friend.otherUserId?.toString()])
    .sort((a, b) => a.conversationName.localeCompare(b.conversationName));

  return (
    <div className="contacts-sidebar">
      <div className="contacts-header">
        <h3>Contacts</h3>
      </div>

      {/* Loading State */}
      {loading ? (
        <div className="loading">Loading contacts...</div>
      ) : (
        <>
          {/* Online Friends */}
          <div className="online-friends">
            <h4>Online • {onlineFriends.length}</h4>
            {onlineFriends.length === 0 ? (
              <p className="empty-message"style={{color:"gray",textAlign:"center"}}>No friends are online.</p>
            ) : (
              <ul className="friends-list">
                {onlineFriends.map((friend) => (
                  <li key={friend.otherUserId} className="friend-item online">
                    <Link
                      to={`/userdetails/${friend.otherUserId}`}
                      className="flex items-center space-x-2 no-underline text-inherit"
                      style={{ textDecoration: "None" }}
                    >
                      <div className="friend-avatar relative">
                        <img
                          src={MainURL + friend.photoUrl}
                          alt={friend.conversationName}
                          className="w-10 h-10 rounded-full object-cover"
                        />
                        <span className="online-indicator absolute bottom-0 right-0 w-3 h-3 bg-green-500 rounded-full border-2 border-white"></span>
                      </div>
                      <span className="friend-name text-sm font-medium">
                        {friend.conversationName}
                      </span>
                    </Link>
                  </li>
                ))}
              </ul>
            )}
          </div>

          {/* All Friends */}
          <div className="all-friends">
            <h4>All Friends</h4>
            {friends.length === 0 ? (
              <p className="empty-message" style={{color:"gray",textAlign:"center"}}>No contacts found.</p>
            ) : (
              <ul className="friends-list">
                {friends.map((friend) => (
                  <li
                    key={friend.id}
                    className={`friend-item ${
                      onlineUsers[friend.id] ? "online" : "offline"
                    }`}
                  >
                    <Link
                      to={`/userdetails/${friend.otherUserId}`}
                      className="flex items-center space-x-2 no-underline text-inherit"
                      style={{ textDecoration: "None" }}
                    >
                      <div className="friend-avatar relative">
                        <img
                          src={MainURL + friend.photoUrl}
                          alt={friend.conversationName}
                          className="w-10 h-10 rounded-full object-cover"
                        />
                        {onlineUsers[friend.id] && (
                          <span className="online-indicator absolute bottom-0 right-0 w-3 h-3 bg-green-500 rounded-full border-2 border-white"></span>
                        )}
                      </div>
                      <span className="friend-name text-sm font-medium">
                        {friend.conversationName}
                      </span>
                    </Link>
                  </li>
                ))}
              </ul>
            )}
          </div>
        </>
      )}
    </div>
  );
}

export default ContactsSidebar;
