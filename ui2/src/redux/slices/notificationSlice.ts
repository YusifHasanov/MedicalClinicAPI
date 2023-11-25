import { createSlice } from "@reduxjs/toolkit"
import { apiSlice } from "./apiSlice"
import { RootState } from "./../store";

const imageSlice = apiSlice.injectEndpoints({
    endpoints: (builder) => ({

        getNotifications: builder.query<NotificationResponse[], undefined>({
            query: () => `Notification`,
            providesTags: ['Notification'],
        }),
        getNotificationsById: builder.query<NotificationResponse[], number>({
            query: (id) => `Notification/${id}`,
            providesTags: ['Notification'],
        }),
        updateNotification: builder.mutation<Notification, { data: UpdateNotification, id: number }>({
            query: ({ data, id }) => ({
                url: `Notification/${id}`,
                method: 'PUT',
                body: data,
            }),
            invalidatesTags: ['Notification'],
        }),
        deleteNotification: builder.mutation<Notification, number>({
            query: (id) => ({
                url: `Notification/${id}`,
                method: 'DELETE',
            }),
            invalidatesTags: ['Notification'],
        }),
        createNotification: builder.mutation<Notification, CreateNotification>({
            query: (data) => ({
                url: `Notification`,
                method: 'POST',
                body: data,
            }),
            invalidatesTags: ['Notification',],
        }),
    }),
})


export const {
    useGetNotificationsQuery,
    useGetNotificationsByIdQuery,
    useUpdateNotificationMutation,
    useDeleteNotificationMutation,
    useCreateNotificationMutation,
} = imageSlice;


