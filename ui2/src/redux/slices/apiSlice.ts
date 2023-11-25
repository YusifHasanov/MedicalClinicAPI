import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import Constant from '../../components/Constants';

const username = 'your_username';
const password = 'your_password';


export const apiSlice = createApi({
  tagTypes: ['User', 'Doctor', 'Patient', 'Payment', 'ImageType', 'Therapy','Notification'],
  reducerPath: 'api',
  baseQuery: fetchBaseQuery({
    baseUrl: Constant.API_URL,
    prepareHeaders: (headers) => {
      headers.append('Authorization', Constant.AUTH_HEADER);
      headers.append('Content-Type', 'application/json');
      headers.append("Token", localStorage.getItem("accessToken") ?? "");
      headers.append('Access-Control-Allow-Origin', '*');
      headers.append('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE');
      headers.append('Access-Control-Allow-Headers', 'X-Requested-With,content-type');

      return headers;
    },
  }),
  endpoints: (builder) => ({}),
});
