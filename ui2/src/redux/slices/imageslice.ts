import { createSlice } from "@reduxjs/toolkit"
import { apiSlice } from "./apiSlice"
import { RootState } from "./../store";

const imageSlice = apiSlice.injectEndpoints({
    endpoints: (builder) => ({
        getImages: builder.query<ImageType[], undefined>({
            query: () => `Image`,
            providesTags: ['ImageType'],
        }),
        getImagesById: builder.query<ImageType, number>({
            query: (id) => `Image/${id}`,
            providesTags: ['ImageType'],
        }),
        updateImage: builder.mutation<ImageType, { data: UpdateImage, id: number }>({
            query: ({ data, id }) => ({
                url: `Image/${id}`,
                method: 'PUT',
                body: data,
            }),
            invalidatesTags: ['ImageType'],
        }),
        deleteImage: builder.mutation<ImageType, number>({
            query: (id) => ({
                url: `Image/${id}`,
                method: 'DELETE',
            }),
            invalidatesTags: ['ImageType','Patient'],
        }),
        createImage: builder.mutation<ImageType, CreateImage>({
            query: (data) => ({
                url: `Image`,
                method: 'POST',
                body: data,
            }),
            invalidatesTags: ['ImageType','Patient'],

        }),
    }),
})


export const {
    useGetImagesQuery,
    useGetImagesByIdQuery,
    useUpdateImageMutation,
    useDeleteImageMutation,
    useCreateImageMutation,
} = imageSlice;


