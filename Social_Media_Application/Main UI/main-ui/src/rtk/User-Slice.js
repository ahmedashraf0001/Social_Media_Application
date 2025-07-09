import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { CurrentUserURL } from "../URLS";
import { getAccessToken } from "../utils/auth";

export const fetchUser = createAsyncThunk('UserSlice/fetchUser', async () => {
    const res = await fetch(CurrentUserURL, {
        headers: {
            Authorization: "Bearer " + getAccessToken()
          }
    });
  
    if (!res.ok) {
      throw new Error(`HTTP error ${res.status}`);
    }
  
    const data = await res.json();
    console.log(data)
    return data;
  });


export const UserSlice = createSlice({
    name: "UserSlice",
    initialState: {},
    extraReducers: (builder) => {
      builder.addCase(fetchUser.fulfilled, (state, action) => {
        return action.payload;
      });
    }
  });

export default UserSlice.reducer;