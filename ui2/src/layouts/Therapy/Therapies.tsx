import { Button } from '@mui/material';
import { GridColDef } from '@mui/x-data-grid';
import moment from 'moment';
import { useState } from 'react';
import { useSelector } from 'react-redux';
import Toast from "../../components/Toast";
import { selectInterval } from '../../redux/slices/intervalSlice';
import { selectTheme } from '../../redux/slices/themeSlice';
import { useGetTherapysByDateIntervalQuery } from '../../redux/slices/therapySlice';
import DateInterval from '../Common/DateInterval';
import MedicalGrid from '../Common/MedicalGrid';
import AddPatient from '../Patient/AddPatient';
import AddPaymentToPatient from '../Payment/AddPaymentToTherapy';
import PatientModal from '../Patient/PatientModal';


const Patients = () => {

  const [rowId, setRowId] = useState(0);
  const [openPatientDetail, setOpenPatientDetail] = useState(false);
  const [openPaymentDetail, setOpenPaymentDetail] = useState(false);
  const interval = useSelector(selectInterval);
  const isDark = useSelector(selectTheme);
  const result = useGetTherapysByDateIntervalQuery(
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
    { field: "id", headerName: 'Id', width: 50 },
    {
      field: "patientName", headerName: 'Xeste Ad', width: 100,
    }, {
      field: "patientSurname", headerName: 'Xeste Soyad', width: 100,
    },
    {
      field: "therapyDate", headerName: 'Gelis Tarixi', width: 200, type: "date",
      valueFormatter: (value: any) => moment(value.value).format('YYYY/MM/DD HH:mm:ss')
    },
    {
      field: "doctorName", headerName: "Hekim",
    },
    {
      field: "doctorSurname", headerName: "Hekim Soyad", width: 100,
    },
    {
      field: "workToBeDone", headerName: "Gorulecek isler", width: 200,
    },
    // {
    //   field: PATIENT_ROWS.isCame, headerName: 'Gelib gelmediyi', width: 180, type: "boolean",
    //   renderCell: (params: any) => {
    //     const { arrivalDate } = params.row;
    //     const now = moment();
    //     const today = moment().startOf('day');

    //     if (moment(arrivalDate).isBefore(now)) {
    //       return <Chip label="Gəlməyib" color="error" />;
    //     } else if (moment(arrivalDate).isSameOrBefore(now) && moment(arrivalDate).isAfter(today)) {
    //       return <Chip label="Gəlib" color="success" />;
    //     } else if (
    //       moment(arrivalDate).isSame(today, 'day') &&
    //       moment(arrivalDate).hours() > moment(now).hours()) {
    //       return <Chip label="Bugün Gələcək" color="info" />;
    //     } else if (moment(arrivalDate).isAfter(now)) {
    //       const daysUntilArrival = moment(arrivalDate).diff(today, 'days');
    //       if (daysUntilArrival > 0 && daysUntilArrival < 7) {
    //         return <Chip label={`${daysUntilArrival} gün sonra gelecek`} color="warning" />;
    //       } else {
    //         return <Chip label={`Gələcək`} color="warning" />;
    //       }
    //     } else {
    //       return <Chip label="Gəlib" color="success" />;
    //     }
    //   }
    // },
    {

      field: "details", headerName: 'Xeste', width: 100, align: "center",
      renderCell: (params: any) => (
        <strong>
          <Button
            variant="contained"
            color="primary"
            size="small"
            onClick={() => {
              setRowId(params.row.patientId);
              setOpenPatientDetail(true);
            }}>
            Detallar
          </Button>
        </strong>
      ),
    },
    // {

    //   field: PATIENT_ROWS.paymentDetails, headerName: 'Oednis', width: 100, align: "center",
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

  const rows = result?.data?.map((therapy) => ({
    id: therapy.id,
    patientName: therapy.patientName,
    patientSurname: therapy.patientSurname,
    doctorName: therapy.doctorName,
    doctorSurname: therapy.doctorSurname,
    therapyDate: therapy.therapyDate,
    patientId: therapy.patientId,
    workToBeDone : therapy.workToBeDone
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
        loading={result.isLoading}
        rows={rows}
        columns={columns} />

    </div>
  );
};

export default Patients;
