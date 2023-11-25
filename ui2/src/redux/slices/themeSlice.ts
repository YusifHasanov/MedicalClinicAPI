import { createSlice } from "@reduxjs/toolkit";
import { RootState } from "../store";
import moment from "moment";

interface ThemeState {
    darkMode: boolean
}

const themeSlice = createSlice({
    name: 'theme',
    initialState: {
        darkMode: false
    } as ThemeState,
    reducers: {
        toggleTheme(state) {
            state.darkMode = !state.darkMode;
        }
    }

});

export const { toggleTheme } = themeSlice.actions;
export const selectTheme = (state: RootState) => state.theme.darkMode;

export default themeSlice.reducer;