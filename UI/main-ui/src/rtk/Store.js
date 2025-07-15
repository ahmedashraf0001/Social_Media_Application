import { configureStore } from "@reduxjs/toolkit";
import UserReducer from "./User-Slice"
import AuthReducer from "./auth-Slice"; 

export const store = configureStore({
    reducer:{
        user: UserReducer,
        auth: AuthReducer,
    },
});