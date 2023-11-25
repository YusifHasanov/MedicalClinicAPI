import { selectInterval } from '../../redux/slices/intervalSlice';
import React, { useEffect } from 'react';
import { useSelector } from 'react-redux';
import moment from 'moment';
import { useGetPatientsByDateIntervalQuery } from '../../redux/slices/patientSlice';
import { DataGrid, GridColDef, GridToolbar } from '@mui/x-data-grid';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterMoment } from '@mui/x-date-pickers/AdapterMoment'
import { DatePicker } from '@mui/x-date-pickers/DatePicker'; 
import LinearProgress from '@mui/material/LinearProgress'; 
import { DOCTOR_ROW, USERS_ROW } from '../../components/Constants';
import   Toast from '../../components/Toast';
 
import { selectTheme } from '../../redux/slices/themeSlice';
import { useGetUsersQuery } from '../../redux/slices/userSlice';

const Patients = () => {
  const isDark = useSelector(selectTheme)
  const interval = useSelector(selectInterval);
  const { data: users, isLoading,isError,error,isSuccess } = useGetUsersQuery(undefined);
 
  useEffect(() => {
    if (isError && !isLoading && !isSuccess) { 
      Toast.error("Həkimlərin məlumatları yüklənərkən xəta baş verdi!"); 
    }
  }, [isError])

  const columns: GridColDef[] = [
    { field: USERS_ROW.id, headerName: 'ID', width: 50 },
    // { field: 'createDate', headerName: 'Create Date', width: 150 ,  type: 'dateTime',  valueGetter: ({ value }:any) => value && new Date(value)},
    // { field: 'updateDate', headerName: 'Update Date', width: 150 },
    { field: USERS_ROW.userName, headerName: 'Adi', width: 200 },
    { field:  USERS_ROW.password, headerName: 'Password', width: 200, 
    valueFormatter:(value:any) => {
      //decrypt hashed password
        
      
    }
  },
    {field:USERS_ROW.role, headerName:"Role", width:80,align:"center",type:"number", valueFormatter:(value:any) => {
      return value == 0 ? "Admin" : "User"
    }}
    // { field: DOCTOR_ROW.fullName, headerName: 'Ad Soyad', width: 200 },

  ];


  const rows = users?.map((user:any) => ({
    id: user.id,
    userName: user.userName,
    password: user.password,
    role: user.role

  })) ?? [] as any[];

 

  return (
    <div className='grid_page_container'>
      
      <DataGrid
  
      loading={isLoading} slots={{ toolbar: GridToolbar, loadingOverlay: LinearProgress, }} rows={rows} columns={columns} />
    </div>
  );
};

export default Patients;
