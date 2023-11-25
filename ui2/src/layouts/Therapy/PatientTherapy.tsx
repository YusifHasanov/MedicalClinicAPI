import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import { Accordion, AccordionDetails, AccordionSummary, Button, FormControl, Grid, InputLabel, MenuItem, Select, TextField, Typography } from '@mui/material';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterMoment } from '@mui/x-date-pickers/AdapterMoment';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import moment from 'moment';
import { FC, useState } from 'react';
import medicalToast from '../../components/Toast';
import { useGetDoctorsQuery } from '../../redux/slices/doctorSlice';
import { useDeleteTherapyMutation, useUpdateTherapyMutation } from '../../redux/slices/therapySlice';
import MedicalModal from '../Common/MedicalModal';
import AddPaymentToTherapy from '../Payment/AddPaymentToTherapy';
import PatientTherapyPayment from '../Payment/PatientTherapyPayment';

interface Props {
    therapy: TherapyResponse
}
const PatientTherapy: FC<Props> = ({ therapy }) => {

    const [udpateTherapy, therapyMutationResult] = useUpdateTherapyMutation();
    const [open, setOpen] = useState(false);
    const [deleteTherapy, therapyDeleteResult] = useDeleteTherapyMutation();

    const doctorsResult = useGetDoctorsQuery(undefined);
    const [formData, setFormData] = useState<UpdateTherapy>({
        therapyDate: therapy.therapyDate,
        paymentAmount: therapy.paymentAmount,
        patientId: therapy.patientId,
        isCame: therapy.isCame,
        workToBeDone: therapy.workToBeDone ?? "",
        workDone: therapy.workDone ?? "",
        doctorId: therapy.doctorId,
        id: therapy.id
    })
    const handleChange = (event: any) => {
        const { name, value } = event.target;

        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));

    };

    const handleUpdateTherapy = () => {
        if (window.confirm("Müayinə yenilənsin?")) {
            udpateTherapy({ data: formData, id: therapy.id })
                .unwrap()
                .then((response) => {
                    medicalToast.success("Muayine yeniləndi!");
                })
                .catch((error) => {
                    medicalToast.error("Muayine yenilənərkən xəta baş verdi!");
                });
        }
    }


    const handleDelete = (therapyId: number) => {
        if (window.confirm("Muayine silinsin?")) {
            deleteTherapy(therapyId).unwrap()
                .then((response) => {
                    medicalToast.success("Muayine silindi!");
                })
                .catch((error) => {
                    medicalToast.error("Muayine silinərkən xəta baş verdi!");
                });
        }
    }

    return (
        <>
            <Grid item xs={12}  >
                <Button sx={{ margin: "0 10px 20px 0" }} variant="contained" color='error' onClick={() => { handleDelete(therapy.id) }}>
                    Muayine sil
                </Button>
                <Button variant="contained" sx={{ margin: "0 0 20px 0" }} color="primary" onClick={() => setOpen(true)}>
                    Ödəniş et
                </Button>
                <Button sx={{ margin: "0 10px 20px 10px" }} variant="contained" color="primary" onClick={handleUpdateTherapy} >
                    Yenilə
                </Button>
                <MedicalModal title='Yeni Odenis' setOpen={setOpen} open={open} fullScreen={false} >
                    <AddPaymentToTherapy open={open} setOpen={setOpen} id={therapy.id} />
                </MedicalModal>
            </Grid>
            <Grid container style={{ width: "100%", justifyContent: "space-between" }} spacing={2}>
                <Grid item xs={12} sm={6} md={4}>
                    <TextField
                        fullWidth
                        label="Ümumi ödəniş"
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
                            label="Müayinə tarixi"
                            views={['year', 'month', 'day']}
                            format="YYYY-MM-DD HH:mm:ss"
                            value={moment(moment(formData.therapyDate).format('YYYY-MM-DDTHH:mm:ss.SSS'))}
                            onChange={(date) => {
                                setFormData(prev => ({
                                    ...prev,
                                    therapyDate: date?.toDate() || new Date()
                                }))
                            }} />
                    </LocalizationProvider>
                </Grid>
                <Grid item xs={12} sm={6} md={4}>
                    <FormControl fullWidth>
                        <InputLabel id="gender-label">Doctor</InputLabel>
                        <Select
                            fullWidth
                            labelId="gender-label"
                            id="cinsi"
                            value={formData.doctorId}
                            label="Doctor"
                            name={"doctorId"}
                            onChange={handleChange}
                        >
                            {
                                doctorsResult.data?.map((doctor) => (
                                    <MenuItem value={doctor.id}>{doctor.name + " " + doctor.surname}</MenuItem>
                                ))
                            }
                        </Select>
                    </FormControl>
                </Grid>

                <Grid item xs={12} sm={6}>
                    <TextField
                        fullWidth
                        label="Gorulen isler"
                        name="workDone"
                        InputLabelProps={{
                            shrink: true,
                        }}
                        onChange={handleChange}
                        value={formData?.workDone}
                    />
                </Grid>
                <Grid item xs={12} sm={6}>
                    <TextField
                        fullWidth
                        label="Gorulecek isler"
                        name="workToBeDone"
                        InputLabelProps={{
                            shrink: true,
                        }}
                        onChange={handleChange}
                        value={formData?.workToBeDone}
                    />
                </Grid>
                <Grid item xs={12} sm={6}>
                </Grid>

                <Grid style={{ paddingLeft: "15px" }} xs={12}>

                    {
                        therapy.payments?.length > 0
                            ? (<>
                                {/* <Typography variant="h6" gutterBottom component="div">
                                    Ödənişlər
                                </Typography> */}
                                <Accordion >
                                    <AccordionSummary
                                        expandIcon={<KeyboardArrowDownIcon />}
                                        aria-controls={`accordion_payment_content`}
                                        id={`accordion_payment_header`}
                                    >
                                        <Typography>Odenisler</Typography>
                                    </AccordionSummary>
                                    <AccordionDetails>
                                        {
                                            therapy.payments.map((payment) => (
                                                <PatientTherapyPayment payment={payment} therapy={therapy} />
                                            ))
                                        }
                                    </AccordionDetails>
                                </Accordion>
                            </>)
                            : (
                                <Typography variant="h6" gutterBottom component="div">
                                    Ödəniş yoxdur
                                </Typography>
                            )
                    }
                </Grid>
            </Grid>
        </>
    )
}

export default PatientTherapy