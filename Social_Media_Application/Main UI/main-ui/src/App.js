import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Feed from './pages/Feed';
import { fetchUser } from './rtk/User-Slice';
import { checkTokenExpiration } from './rtk/auth-Slice';
import { useDispatch, useSelector } from "react-redux";
import PostDetails from './pages/PostDetails';
import Navbar from './Components/navbar';
import CreatePostPage from './pages/CreatePost';
import { SignalRProvider } from './services/SignalR';
import { useEffect } from "react";
import 'react-toastify/dist/ReactToastify.css';
import { ToastContainer } from 'react-toastify';
import ProfilePage from './pages/profilePage';
import AuthPage from './pages/login-register';

function App() {
  const dispatch = useDispatch();
  const { isAuthenticated, token } = useSelector(state => state.auth);

  useEffect(() => {
    // Check token expiration on app load
    dispatch(checkTokenExpiration());
  }, [dispatch]);

  useEffect(() => {
    // Fetch user data when authenticated
    if (isAuthenticated && token) {
      dispatch(fetchUser());
    }
  }, [dispatch, isAuthenticated, token]);

  return (
    <>  
      <SignalRProvider>
        <div className="App">
          <Routes>
            <Route path='/' element={
              isAuthenticated ? (
                <>
                  <Navbar />
                  <Feed />
                </>
              ) : (
                <AuthPage />
              )
            } />

            <Route path='/PostDetails/:Id' element={
              isAuthenticated ? (
                <PostDetails />
              ) : (
                <AuthPage />
              )
            } />
            
            <Route path='/login' element={<AuthPage />} />           
            
            <Route path='/UserDetails/:userId' element={
              isAuthenticated ? (
                <>
                  <Navbar />
                  <ProfilePage />
                </>
              ) : (
                <AuthPage />
              )
            } />

            <Route path='/create-post' element={
              isAuthenticated ? (
                <>
                  <Navbar />
                  <CreatePostPage />
                </>
              ) : (
                <AuthPage />
              )
            } />
          </Routes>
          <ToastContainer position="top-right" autoClose={3000} />
        </div>
      </SignalRProvider>
    </>
  );
}

export default App;