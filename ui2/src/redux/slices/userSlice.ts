import { createSlice } from "@reduxjs/toolkit"
import { apiSlice } from "./apiSlice"
import { RootState } from "./../store";

const userSlice = apiSlice.injectEndpoints({
    endpoints: (builder) => ({
        getUsers: builder.query<UserResponse[], undefined>({
            query: () => `User`,
            providesTags: ['User'],
        }),
        getUsersById: builder.query<User, number>({
            query: (id) => `User/${id}`,
            providesTags: ['User'],
        }),
        updateUser: builder.mutation<User, { data: UpdateUser, id: number }>({
            query: ({ data, id }) => ({
                url: `User/${id}`,
                method: 'PUT',
                body: data,
            }),
            invalidatesTags: ['User'],
        }),
        deleteUser: builder.mutation<User, number>({
            query: (id) => ({
                url: `User/${id}`,
                method: 'DELETE',
            }),
            invalidatesTags: ['User'],
        }),
        createUser: builder.mutation<User, CreateUser>({
            query: (data) => ({
                url: `User`,
                method: 'POST',
                body: data,
            }),
            invalidatesTags: ['User'],
        }),
        loginUser: builder.mutation<AuthResponse, LoginUser>({
            query: (data) => ({
                url: `User/login`,
                method: 'POST',
                body: data, 
            }) ,
            invalidatesTags: ['User'],
        }),
        refreshAccessToken: builder.mutation<AuthResponse, RefreshTokenDto>({
            query: (data) => ({
                url: `User/refresh`,
                method: 'POST',
                body: data,
            }),
            invalidatesTags: ['User'],
        }),
    }),
})


export const {
    useGetUsersQuery,
    useGetUsersByIdQuery,
    useUpdateUserMutation,
    useDeleteUserMutation,
    useCreateUserMutation,
    useLoginUserMutation,
    useRefreshAccessTokenMutation,
} = userSlice;


