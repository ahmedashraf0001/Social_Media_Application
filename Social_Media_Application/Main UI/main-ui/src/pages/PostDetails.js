import { useEffect, useState } from "react";
import { Navigate, useParams, useNavigate  } from "react-router-dom";
import Post from "../Components/Post";
import { PostURL } from "../URLS";
import { authKey } from "../rtk/User-Slice";
import PostDetailsContainer from "./postDetailsContainer";
import  Navbar from "../Components/navbar";
import { getAccessToken } from '../utils/auth';

function PostDetails() {
    const [post, setPost] = useState(null);
    const { Id } = useParams();
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
        }
      };
  
      if (Id) loadPost();
    }, [Id]);
  
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
 
// there is a bug with the liked by current user where if the user make a post
// it will be considered as liked by default without the counter going up resulting in -1 likes