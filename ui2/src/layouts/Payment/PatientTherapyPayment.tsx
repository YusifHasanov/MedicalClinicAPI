import { Button, Grid, TextField } from '@mui/material';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterMoment } from '@mui/x-date-pickers/AdapterMoment';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import moment from 'moment';
import { FC, useState } from 'react';
import medicalToast from '../../components/Toast';
import { useUpdatePaymentMutation } from '../../redux/slices/paymentSlice';
interface Props {
    payment: MyPaymentResponse,
    therapy: TherapyResponse
}


const PatientTherapyPayment: FC<Props> = ({ payment, therapy }) => {

    const [updatePaymentMutation, UpdatePaymentResult] = useUpdatePaymentMutation();
    const [formData, setFormData] = useState<UpdatePayment>({
        patientId: therapy.patientId,
        id: payment.id,
        paymentAmount: payment.paymentAmount,
        paymentDate: payment.paymentDate,
        therapyId: payment.therapyId
    })

    const handleUpdate = () => {
        if (window.confirm("Ödəniş məlumatlarını yeniləmək istədiyinizə əminsiniz?")) {
            console.log(formData)
            updatePaymentMutation({ data: formData, id: payment.id })
                .unwrap()
                .then((response) => {
                    medicalToast.success("Ödəniş məlumatları yeniləndi")
                })
                .catch((error) => {
                    medicalToast.error("Xəta baş verdi")
                })
        }
    }


    const handleChange = (event: any) => {
        const { name, value } = event.target;

        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));

    };

    return (
        <Grid container spacing={2}>
            <Grid item xs={12} sm={6} md={3}>
                <TextField
                    fullWidth
                    label="Ödəniş"
                    InputLabelProps={{
                        shrink: true,
                    }}
                    name="paymentAmount"
                    value={formData.paymentAmount}
                    onChange={handleChange}
                />
            </Grid>
            <Grid item xs={12} sm={6} md={4}>
                <LocalizationProvider dateAdapter={AdapterMoment}>
                    <DatePicker
                        className='date_picker w_100'
                        label="Ödəniş tarixi"
                        views={['year', 'month', 'day']}
                        format="YYYY-MM-DD HH:mm:ss"
                        value={moment(moment(formData.paymentDate).format('YYYY-MM-DDTHH:mm:ss.SSS'))}
                    />

                </LocalizationProvider>
            </Grid>
            <Grid item xs={12} sm={6} md={4}>
                <TextField
                    fullWidth
                    label="Həkim"
                    InputLabelProps={{
                        shrink: true,
                    }}
                    disabled={true}
                    name="doctorFullName"
                    value={therapy?.doctorName + " " + therapy?.doctorSurname}
                />
            </Grid>
            <Grid item xs={12} sm={6} md={1}>
                <Button  sx={{ margin: "10px 10px 20px 0" }}  variant='contained' color='primary'  onClick={handleUpdate} >
                    Save
                </Button>
            </Grid>
        </Grid>
    )
}

export default PatientTherapyPayment