import { useEffect, useState } from "react";
import { useParams, useNavigate  } from "react-router-dom";
import Post from "../Components/Post";
import { PostURL } from "../URLS";
import PostDetailsContainer from "./postDetailsContainer";
import  Navbar from "../Components/navbar";
import { getAccessToken } from '../utils/auth';
import FailedToLoad from "../utils/loadError";

function PostDetails() {
    const [post, setPost] = useState(null);
    const { Id } = useParams();

    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(false);

    const navigate = useNavigate();

    const handlePostDeleted = () => {
       navigate("/")
    };

    useEffect(() => {
      const loadPost = async () => {
        try {
          const res = await fetch(`${PostURL}/Id/${Id}`, {
            headers: { Authorization: "Bearer " + getAccessToken() },
          });
          if (!res.ok) throw new Error(`HTTP error ${res.status}`);
          const data = await res.json();
          setPost(data);
        } catch (err) {
          console.error("Failed to fetch post:", err.message);
          setError(true);
        }
        finally {
          setLoading(false);
        }
      };
  
      if (Id) loadPost();
    }, [Id]);

    if (loading) return <div>Loading Contacts...</div>;

    if (error == true) {
      return (
          <div className="main-content">
            <FailedToLoad/>
          </div>
      );
    }
  
    if (!post) return <div className="Post-Container" style={{ display: "flex" , justifyContent: "center", alignItems:"center", height:"700px"}}>Loading post...</div>;
    return (
      post.mediaUrl === "N/A" ? (
        <>
        <Navbar/>
        <div className="Post-Container" style={{padding:"100px"}}>
          <Post 
          post={post} 
          details={true} 
          onPostDeleted={handlePostDeleted}
          />
        </div>
        </>
      ) : (
        <>
          <PostDetailsContainer post={post} />
        </>
      )
    );
    
  }
  

export default PostDetails;
 