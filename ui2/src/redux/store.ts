import { configureStore } from '@reduxjs/toolkit'
import { setupListeners } from '@reduxjs/toolkit/query'
import intervalReducer from "./slices/intervalSlice";
import themeReducer from "./slices/themeSlice";
import authReducer from "./slices/authSlice";

import { apiSlice } from './slices/apiSlice';
 




export const store = configureStore({
  reducer: {
    [apiSlice.reducerPath]: apiSlice.reducer,
    interval :intervalReducer,
    theme:themeReducer,
    auth:authReducer
  },

  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(apiSlice.middleware),
})


setupListeners(store.dispatch)
export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch