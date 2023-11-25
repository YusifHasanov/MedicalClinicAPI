import { createSlice } from "@reduxjs/toolkit"
import { apiSlice } from "./apiSlice"
import { RootState } from "./../store";

const paymentSlice = apiSlice.injectEndpoints({
    endpoints: (builder) => ({
        getPayments: builder.query<PaymentResponse[], undefined>({
            query: () => `Payment`,
            providesTags: ['Payment'],
        }),
        getPaymentsById: builder.query<Payment, number>({
            query: (id) => `Payment/${id}`,
            providesTags: ['Payment'],
        }),
        updatePayment: builder.mutation<Payment, { data: UpdatePayment, id: number }>({
            query: ({ data, id }) => ({
                url: `Payment/${id}`,
                method: 'PUT',
                body: data,
            }),
            invalidatesTags: ['Payment'],
        }),
        deletePayment: builder.mutation<Payment, number>({
            query: (id) => ({
                url: `Payment/${id}`,
                method: 'DELETE',
            }),
            invalidatesTags: ['Payment'],
        }),
        createPayment: builder.mutation<Payment, CreatePayment>({
            query: (data) => ({
                url: `Payment`,
                method: 'POST',
                body: data,
            }),
            invalidatesTags: ['Payment',"Therapy","Patient"],
        }),
        getPaymentsByDateInterval: builder.query<MyPaymentResponse[], DateIntervalRequest | null | undefined>({
            query: (date) => ({
                url: `Payment/byDateInterval`,
                method: 'POST',
                body: date,
            }),
            providesTags: ['Payment'],
        }),
        getPaymentsByPatientId: builder.query<MyPaymentResponse[], number>({
            query: (id) => `Payment/byPatientId/${id}`,
            providesTags: ['Payment'],
        }),
    }),
})


export const {
    useGetPaymentsQuery,
    useGetPaymentsByIdQuery,
    useUpdatePaymentMutation,
    useDeletePaymentMutation,
    useCreatePaymentMutation,
    useGetPaymentsByDateIntervalQuery,
    useGetPaymentsByPatientIdQuery,
} = paymentSlice;


