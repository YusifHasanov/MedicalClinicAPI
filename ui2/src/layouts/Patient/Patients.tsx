import { Button, Chip } from '@mui/material';
import { GridColDef } from '@mui/x-data-grid';
import moment from 'moment';
import { useState } from 'react';
import { useSelector } from 'react-redux';
import { PATIENT_COLS } from '../../components/Constants';
import { selectInterval } from '../../redux/slices/intervalSlice';
import { useGetPatientsByDateIntervalQuery } from '../../redux/slices/patientSlice';
import { selectTheme } from '../../redux/slices/themeSlice';
import DateInterval from '../Common/DateInterval';
import MedicalGrid from '../Common/MedicalGrid';
import Toast from "./../../components/Toast";
import AddPatient from './AddPatient';
import AddPaymentToPatient from '../Payment/AddPaymentToTherapy';
import PatientModal from './PatientModal';


const Patients = () => {

  const [rowId, setRowId] = useState(0);
  const [openPatientDetail, setOpenPatientDetail] = useState(false);
  const [openPaymentDetail, setOpenPaymentDetail] = useState(false);
  const interval = useSelector(selectInterval);
  const isDark = useSelector(selectTheme);
  const result = useGetPatientsByDateIntervalQuery(
    { fromDate: interval.fromDate, toDate: interval.toDate });

  const fetchAgain = () => {
    result.refetch().unwrap()
      .then((originalPromiseResult) => {
        console.log(originalPromiseResult);
        Toast.success("Xəstə məlumatları yükləndi!");
      })
      .catch((rejectedValueOrSerializedError) => {
        console.log(rejectedValueOrSerializedError);
        Toast.error("Xəstə məlumatları yüklənərkən xəta baş verdi!");
      });
  }

  const columns: GridColDef[] = [
    { field: PATIENT_COLS.id, headerName: 'ID', width: 50 },
    {
      field: PATIENT_COLS.name, headerName: 'Ad', width: 200,
    }, {
      field: PATIENT_COLS.surname, headerName: 'Soyad', width: 200,
    },
    { field: PATIENT_COLS.diagnosis, headerName: 'Diagnoz', width: 200, },

    {
      field: PATIENT_COLS.arrivalDate, headerName: 'Gelis Tarixi', width: 200, type: "date",
      valueFormatter: (value: any) => moment(value.value).format('YYYY/MM/DD HH:mm:ss')
    },
     
    // {
    //   field: PATIENT_COLS.isCame, headerName: 'Gelib gelmediyi', width: 180, type: "boolean",
    //   renderCell: (params: any) => {
    //     const { isCame, arrivalDate } = params.row;
    //     const now = moment();
    //     const today = moment().startOf('day');

    //     if (!isCame && moment(arrivalDate).isBefore(now)) {

    //       return <Chip label="Gəlməyib" color="error" />;

    //     } else if (!isCame && moment(arrivalDate).isSameOrBefore(now) && moment(arrivalDate).isAfter(today)) {

    //       return <Chip label="Gəlib" color="success" />;

    //     } else if (!isCame &&
    //       moment(arrivalDate).isSame(today, 'day') &&
    //       moment(arrivalDate).hours() > moment(now).hours()) {

    //       return <Chip label="Bugün Gələcək" color="info" />;

    //     } else if (!isCame && moment(arrivalDate).isAfter(now)) {

    //       const daysUntilArrival = moment(arrivalDate).diff(today, 'days');

    //       if (daysUntilArrival > 0 && daysUntilArrival < 7) {

    //         return <Chip label={`${daysUntilArrival} gün sonra gelecek`} color="warning" />;

    //       } else {
    //         return <Chip label={`Gələcək`} color="warning" />;
    //       }
    //     } else if (isCame) {
    //       return <Chip label="Gəlib" color="success" />;
    //     }
    //   }
    // },
    {

      field: PATIENT_COLS.details, headerName: 'Xeste', width: 100, align: "center",
      renderCell: (params: any) => (
        <strong>
          <Button
            variant="contained"
            color="primary"
            size="small"
            onClick={() => {
              setRowId(params.row.id);
              setOpenPatientDetail(true);
            }}>
            Detallar
          </Button>
        </strong>
      ),
    },
    // {

    //   field: PATIENT_COLS.paymentDetails, headerName: 'Oednis', width: 100, align: "center",
    //   renderCell: (params: any) => (
    //     <strong>
    //       <Button
    //         variant="contained"
    //         color="info"
    //         size="small"
    //         onClick={() => {
    //           setRowId(params.row.id);
    //           console.log(params.row.id);
    //           setOpenPaymentDetail(true);
    //         }}>
    //         Detallar
    //       </Button>
    //     </strong>
    //   ),
    // }

  ];

  const rows = result?.data?.map((patient) => ({
    id: patient.id,
    name: patient.name,
    surname: patient.surname,
    arrivalDate: patient.arrivalDate,
    diagnosis: patient.diagnosis,
  })) ?? [];


  return (
    <div className='grid_page_container' >
      <DateInterval >
        <Button style={{ margin: "0 0 10px 10px" }} variant='contained' color="info" onClick={fetchAgain}>
          Sorğula
        </Button>
        <AddPatient />
      </DateInterval>
      <PatientModal open={openPatientDetail} setOpen={setOpenPatientDetail} id={rowId} />
 

      <MedicalGrid
        rows={rows}
        columns={columns}
        loading={result.isLoading} />
    </div>
  );
};

export default Patients;
