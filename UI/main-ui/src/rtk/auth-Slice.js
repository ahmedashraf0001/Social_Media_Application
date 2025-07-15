import { createSlice } from '@reduxjs/toolkit';

const authSlice = createSlice({
  name: 'auth',
  initialState: {
    isAuthenticated: !!localStorage.getItem('accessToken'),
    token: localStorage.getItem('accessToken'),
    tokenExpiration: localStorage.getItem('tokenExpiration')
  },
  reducers: {
    login: (state, action) => {
      const { token, expirationTime } = action.payload;
      
      state.isAuthenticated = true;
      state.token = token;
      state.tokenExpiration = expirationTime;
      
      localStorage.setItem('accessToken', token);
      localStorage.setItem('tokenExpiration', expirationTime);
    },
    logout: (state) => {
      state.isAuthenticated = false;
      state.token = null;
      state.tokenExpiration = null;
      localStorage.removeItem('accessToken');
      localStorage.removeItem('tokenExpiration');
    },
    checkTokenExpiration: (state) => {
      const tokenExpiration = localStorage.getItem('tokenExpiration');
      
      if (!tokenExpiration) {
        state.isAuthenticated = false;
        state.token = null;
        state.tokenExpiration = null;
        return;
      }
      
      const expirationDate = new Date(tokenExpiration);
      const currentDate = new Date();
      
      if (currentDate >= expirationDate) {
        state.isAuthenticated = false;
        state.token = null;
        state.tokenExpiration = null;
        
        localStorage.removeItem('accessToken');
        localStorage.removeItem('tokenExpiration');
      }
    }
  }
});

export const { login, logout, checkTokenExpiration } = authSlice.actions;
export default authSlice.reducer;