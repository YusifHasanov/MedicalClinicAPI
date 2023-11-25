import { createSlice } from "@reduxjs/toolkit"
import { apiSlice } from "./apiSlice"
import { RootState } from "./../store";

const doctorSlice = apiSlice.injectEndpoints({
    endpoints: (builder) => ({
        getDoctors: builder.query<Doctor[], undefined>({
            query: () => `Doctor`,
            providesTags: ['Doctor'],
        }),
        getDoctorsById: builder.query<Doctor, number>({
            query: (id) => `User/${id}`,
            providesTags: ['Doctor'],
        }),
        updateDoctor: builder.mutation<Doctor, { data: UpdateDoctor, id: number }>({
            query: ({ data, id }) => ({
                url: `Doctor/${id}`,
                method: 'PUT',
                body: data,
            }),
            invalidatesTags: ['Doctor'],
        }),
        deleteDoctor: builder.mutation<Doctor, number>({
            query: (id) => ({
                url: `Doctor/${id}`,
                method: 'DELETE',
            }),
            invalidatesTags: ['Doctor'],
        }),
        createDoctor: builder.mutation<Doctor, CreateDoctor>({
            query: (data) => ({
                url: `Doctor`,
                method: 'POST',
                body: data,
            }),
            invalidatesTags: ['Doctor'],

        }),
    }),
})


export const {
    useGetDoctorsQuery,
    useGetDoctorsByIdQuery,
    useUpdateDoctorMutation,
    useDeleteDoctorMutation,
    useCreateDoctorMutation,
} = doctorSlice;


