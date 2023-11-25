import LinearProgress from '@mui/material/LinearProgress';
import { DataGrid, GridToolbar } from '@mui/x-data-grid';
import moment from 'moment';
import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { PAYMENT_ROW } from '../../components/Constants';
import Toast from '../../components/Toast';
import { selectInterval } from '../../redux/slices/intervalSlice';
import { useGetPaymentsByDateIntervalQuery } from '../../redux/slices/paymentSlice';
import DateInterval from '../Common/DateInterval';
import { Chip } from '@mui/material';
import QuickSearchToolbar from '../Common/QucikSearchToolbar';


const Patients = () => {
    const interval = useSelector(selectInterval);
    const dispatch = useDispatch();
    const paymentResult =  useGetPaymentsByDateIntervalQuery({ fromDate: interval.fromDate, toDate: interval.toDate });

    useEffect(() => {
        if (paymentResult.error ) {
            Toast.error("Ödəniş məlumatları yüklənərkən xəta baş verdi!");
        }
    }, [paymentResult.error, paymentResult.isError])

    const columns: any[] = [
        { field: PAYMENT_ROW.id, headerName: 'ID', width: 100 },

        { field: PAYMENT_ROW.patientFullName, headerName: 'Xəstənin adı soyadı', width: 200 },
        { field: PAYMENT_ROW.doctorFullName, headerName: 'Həkimin adı soyadı', width: 200 },
        {
            field: PAYMENT_ROW.amount, headerName: "Ödəniş məbləği", width: 150, align: "center", type: "number",
            renderCell: (params: any) => (
                <Chip label={`${params.value} AZN`} color="info" />
            ),
        },
        { field: PAYMENT_ROW.paymentDate, headerName: "Ödəniş tarixi", width: 200, type: "date", valueFormatter: (value: any) => moment(value.value).format('YYYY/MM/DD HH:mm') }


    ];


    const rows = paymentResult.data?.map((payment) => ({
        id: payment.id,
        patientFullName: payment.patientName + " " + payment.patientSurname,
        doctorFullName: payment.doctorName + " " + payment.doctorSurname,
        amount: payment.paymentAmount,
        paymentDate: payment.paymentDate,
    })) ?? [] as any[];



    return (
        <div className='grid_page_container'>
            <DateInterval />
            <DataGrid
                slotProps={{
                    toolbar: {
                        showQuickFilter: true,
                    }
                }}
                checkboxSelection
                disableRowSelectionOnClick
                slots={{ toolbar: QuickSearchToolbar, loadingOverlay: LinearProgress }}
                loading={paymentResult.isLoading} rows={rows} columns={columns} />
        </div>
    );
};

export default Patients;
