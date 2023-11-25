import { DataGrid, GridColDef, GridToolbar } from '@mui/x-data-grid';
import { useEffect } from 'react';
import { useSelector } from 'react-redux';
import { selectInterval } from '../../redux/slices/intervalSlice';
 
import LinearProgress from '@mui/material/LinearProgress';
import { DOCTOR_ROW } from '../../components/Constants';
import Toast from '../../components/Toast';
import { useGetDoctorsQuery } from '../../redux/slices/doctorSlice';
import { selectTheme } from '../../redux/slices/themeSlice';
import AddDoctor from './AddDoctor';
import QuickSearchToolbar from '../Common/QucikSearchToolbar';

const Patients = () => {
 
  const doctorResult = useGetDoctorsQuery(undefined );
 
 

  const columns: GridColDef[] = [
    { field: DOCTOR_ROW.id, headerName: 'ID', width: 50 },
    { field: DOCTOR_ROW.name, headerName: 'Adi', width: 200 },
    { field:  DOCTOR_ROW.surname, headerName: 'Soyadi', width: 200 },
  ];


  const rows = doctorResult.data?.map((doctor:any) => ({
    id: doctor.id,
    name: doctor.name,
    surname: doctor.surname, 
  })) ?? [] as any[];

 

  return (
    <div className='grid_page_container'>
       <AddDoctor/>
      <DataGrid
          slotProps={{
            toolbar: {
              showQuickFilter: true,
            }
          }}
          checkboxSelection
          disableRowSelectionOnClick
          slots={{ toolbar: QuickSearchToolbar, loadingOverlay: LinearProgress }}
      loading={doctorResult.isLoading}   rows={rows} columns={columns} />
    </div>
  );
};

export default Patients;
