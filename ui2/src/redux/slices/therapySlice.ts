import { createSlice } from "@reduxjs/toolkit"
import { apiSlice } from "./apiSlice"
import { RootState } from "./../store";

const therapySlice = apiSlice.injectEndpoints({
    endpoints: (builder) => ({
        getTherapys: builder.query<TherapyResponse[], undefined>({
            query: () => `Therapy`,
            providesTags: ['Therapy'],
        }),
        getTherapysById: builder.query<Therapy, number>({
            query: (id) => `Therapy/${id}`,
            providesTags: ['Therapy'],
        }),
        updateTherapy: builder.mutation<Therapy, { data: UpdateTherapy, id: number }>({
            query: ({ data, id }) => ({
                url: `Therapy/${id}`,
                method: 'PUT',
                body: data,
            }),
            invalidatesTags: ['Therapy'],
        }),
        deleteTherapy: builder.mutation<Therapy, number>({
            query: (id) => ({
                url: `Therapy/${id}`,
                method: 'DELETE',
            }),
            invalidatesTags: ['Therapy'],
        }),
        createTherapy: builder.mutation<Therapy, CreateTherapy>({
            query: (data) => ({
                url: `Therapy`,
                method: 'POST',
                body: data,
            }),
            invalidatesTags: ['Therapy'],
        }),
        getTherapysByDateInterval: builder.query<TherapyResponse[], DateIntervalRequest | null | undefined>({
            query: (date) => ({
                url: `Therapy/byDateInterval`,
                method: 'POST',
                body: date,
            }),
            providesTags: ['Therapy'],
        }),
        getTherapysByPatientId: builder.query<TherapyResponse[], number>({
            query: (id) => `Therapy/byPatientId/${id}`,
            providesTags: ['Therapy'],
        }),
    }),
})


export const {
    useGetTherapysQuery,
    useGetTherapysByIdQuery,
    useUpdateTherapyMutation,
    useDeleteTherapyMutation,
    useCreateTherapyMutation,
    useGetTherapysByDateIntervalQuery,
    useGetTherapysByPatientIdQuery,
} = therapySlice;


