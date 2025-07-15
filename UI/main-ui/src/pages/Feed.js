import { useEffect, useState } from "react";
import Post from "../Components/Post";
import { FeedURL } from "../URLS";
import FailedToLoad from "../utils/loadError";
import './styles/feed.css'
import ContactsSidebar from "../Components/Contact";
import SocialPostInterface from "../Components/postInterface";
import DiscoverPeople from "../Components/DiscoverPeople";
import { useSelector } from "react-redux";
import { getAccessToken } from '../utils/auth';

function Feed() {
  const user = useSelector(state => state.user);
  const [posts, setPosts] = useState([]);



  useEffect(() => {
    const loadPosts = async () => {
      try {
        const res = await fetch(FeedURL, {
          headers: {
            Authorization: "Bearer " + getAccessToken(),
          },
        });

        if (!res.ok) throw new Error(res.status);
        const data = await res.json();
        setPosts(data);
      } catch (err) {
        console.error("Failed to fetch posts:", err.message);
        if(err.message.includes("400")){
          setError(400)
        }
        else{
          setError(true);
        }
      } finally {
        setLoading(false);
      }
    };
    loadPosts();
  }, []);

  const [error, setError] = useState(false);
  const [loading, setLoading] = useState(true);
  
  if (loading) return <div>Loading posts...</div>;

  if (error == true) {
    return (
        <div className="main-content">
          <FailedToLoad/>
        </div>
    );
  }
  if (error == 400) {
    return (
      <div className="feed-layout">
      <div className="main-content-feed">
        <div className="Post-Container">
          <SocialPostInterface user={user}/>
          <DiscoverPeople/>
        </div>
      </div>
        <ContactsSidebar />  
    </div>
    );
  }
  
  return (
    <div className="feed-layout">
      <div className="main-content-feed">
        <div className="Post-Container">
          <SocialPostInterface user={user}/>
          {posts.map((post, index) => (
            <Post key={index} post={post} />
          ))}
        </div>
      </div>
        <ContactsSidebar />  
    </div>
  );
}

export default Feed;