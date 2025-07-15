// utils/auth.js

/**
 * Handles user logout functionality (client-side only)
 * @param {Function} dispatch - Redux dispatch function
 * @param {Function} onSuccess - Optional callback function to execute on successful logout
 * @param {Function} onError - Optional callback function to execute on logout error
 * @param {Function} navigate - Optional navigation function (e.g., from useNavigate hook)
 */
export const handleLogout = async (dispatch, onSuccess, onError, navigate) => {
  try {
    // Import and dispatch logout action
    const { logout } = await import('../rtk/auth-Slice');
    dispatch(logout());
    
    // Clear any other user-related data from localStorage
    localStorage.removeItem('userData');
    localStorage.removeItem('userProfile');
    
    // Clear session storage as well if you use it
    sessionStorage.clear();
    
    // Execute success callback if provided
    if (onSuccess && typeof onSuccess === 'function') {
      onSuccess();
    }
    
    // Navigate to login page or home page if navigate function is provided
    if (navigate && typeof navigate === 'function') {
      navigate('/login');
    }
    
    console.log('User logged out successfully');
    
  } catch (error) {
    console.error('Logout error:', error);
    
    // Execute error callback if provided
    if (onError && typeof onError === 'function') {
      onError(error);
    }
    
    // Even if there's an error, try to clear local storage and Redux state
    try {
      const { logout } = await import('../rtk/auth-Slice');
      dispatch(logout());
    } catch (storageError) {
      console.error('Error clearing storage:', storageError);
    }
  }
};

/**
* Handles user login functionality
* @param {Function} dispatch - Redux dispatch function
* @param {string} token - Access token from login response
* @param {string} expirationTime - Token expiration time
* @param {Function} onSuccess - Optional callback function to execute on successful login
* @param {Function} navigate - Optional navigation function
*/
export const handleLogin = async (dispatch, token, expirationTime, onSuccess, navigate) => {
try {
  // Import and dispatch login action
  const { login } = await import('../rtk/auth-Slice');
  dispatch(login({ 
    token, 
    expirationTime 
  }));
  
  // Execute success callback if provided
  if (onSuccess && typeof onSuccess === 'function') {
    onSuccess();
  }
  
  // Navigate to home page if navigate function is provided
  if (navigate && typeof navigate === 'function') {
    navigate('/');
  }
  
  console.log('User logged in successfully');
  
} catch (error) {
  console.error('Login error:', error);
  throw error;
}
};

/**
* Checks if user is currently logged in
* @returns {boolean} - True if user is logged in, false otherwise
*/
export const isUserLoggedIn = () => {
try {
  const accessToken = localStorage.getItem('accessToken');
  const tokenExpiration = localStorage.getItem('tokenExpiration');
  
  if (!accessToken || !tokenExpiration) {
    return false;
  }
  
  // Check if token is expired
  const expirationDate = new Date(tokenExpiration);
  const currentDate = new Date();
  
  if (currentDate >= expirationDate) {
    // Token is expired, remove it
    localStorage.removeItem('accessToken');
    localStorage.removeItem('tokenExpiration');
    return false;
  }
  
  return true;
} catch (error) {
  console.error('Error checking login status:', error);
  return false;
}
};

/**
* Gets the current user's access token
* @returns {string|null} - Access token or null if not logged in
*/
export const getAccessToken = () => {
try {
  console.log(isUserLoggedIn())
  if (isUserLoggedIn()) {
    return localStorage.getItem('accessToken');
  }
  return null;
} catch (error) {
  console.error('Error getting access token:', error);
  return null;
}
};

/**
* Auto-logout when token expires
* @param {Function} dispatch - Redux dispatch function
* @param {Function} onAutoLogout - Callback function to execute on auto logout
*/
export const setupAutoLogout = (dispatch, onAutoLogout) => {
const checkTokenExpiration = async () => {
  if (!isUserLoggedIn()) {
    try {
      // Import and dispatch logout action
      const { logout } = await import('../rtk/auth-Slice');
      dispatch(logout());
      
      if (onAutoLogout && typeof onAutoLogout === 'function') {
        onAutoLogout();
      }
    } catch (error) {
      console.error('Error during auto-logout:', error);
    }
  }
};

// Check every minute
const interval = setInterval(checkTokenExpiration, 60000);

// Return cleanup function
return () => clearInterval(interval);
};