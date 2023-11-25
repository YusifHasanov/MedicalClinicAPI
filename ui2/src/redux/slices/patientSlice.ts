import { createSlice } from "@reduxjs/toolkit"
import { apiSlice } from "./apiSlice"
import { RootState } from "./../store";

const patientSlice = apiSlice.injectEndpoints({
    endpoints: (builder) => ({
        getPatients: builder.query<PatientResponse[], undefined>({
            query: () => `Patient`,
            providesTags: ['Patient'],
        }),
        getPatientById: builder.query<PatientResponse, number>({
            query: (id) => `Patient/${id}`,
            providesTags: ['Patient'],
        }),
        updatePatient: builder.mutation<Patient, { data: UpdatePatient, id: number }>({
            query: ({ data, id }) => ({
                url: `Patient/${id}`,
                method: 'PUT',
                body: data,
            }),
            invalidatesTags: ['Patient'],
        }),
        deletePatient: builder.mutation<Patient, number>({
            query: (id) => ({
                url: `Patient/${id}`,
                method: 'DELETE',
            }),
            invalidatesTags: ['Patient'],
        }),
        createPatient: builder.mutation<Patient, CreatePatient>({
            query: (data) => ({
                url: `Patient`,
                method: 'POST',
                body: data,
            }),
            invalidatesTags: ['Patient'], 
        }),
        getPatientsByDateInterval: builder.query<PatientResponse[], DateIntervalRequest | null | undefined>({
            query: (date) => ({
                url: `Patient/byDateInterval`,
                method: 'POST',
                body: date,
            }),
            providesTags: ['Patient','Therapy'],
            
        }),
    }),
})


export const {
    useGetPatientsQuery,
    useGetPatientByIdQuery,
    useUpdatePatientMutation,
    useDeletePatientMutation,
    useCreatePatientMutation,
    useGetPatientsByDateIntervalQuery,
} = patientSlice;


