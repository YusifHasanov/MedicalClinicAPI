import { createSlice } from "@reduxjs/toolkit";
import { RootState } from "../store";


interface AuthState {
    accessToken: string | null;
    isValidToken: boolean;
    userId: number | null;
    role: Role | null;
}




const authSlice = createSlice({
    name: 'auth',
    initialState: {
        accessToken: localStorage.getItem('accessToken') ?? null,
        isValidToken: false,
        userId: null,
        role: null,
    } as AuthState,
    reducers: {
        // setAccessToken(state, action) {
        //     state.accessToken = action.payload;
        // },
        setIsValidToken(state, action) {
            state.isValidToken = action.payload;
        },
        setRole(state, action) {
            state.role = action.payload;
        },
        setUserId(state, action) {
            state.userId = action.payload;
        },
        setAll(state, action) {
            state.isValidToken = action.payload.isValidToken;
            state.userId = action.payload.userId;
            state.role = action.payload.role;
            state.accessToken = action.payload.accessToken;
        }
    }
});

export const {
    // setAccessToken,
    setIsValidToken,
    setRole,
    setUserId,
    setAll
} = authSlice.actions;
// export const selectAccessToken = (state: RootState) => state.auth.accessToken;
export const selectIsValidToken = (state: RootState) => state.auth.isValidToken;
export const selectRole = (state: RootState) => state.auth.role;
export const selectUserId = (state: RootState) => state.auth.userId;
export const selectAll = (state: RootState) => state.auth;

export default authSlice.reducer;