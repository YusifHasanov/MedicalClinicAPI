import { Button, FormControl, Grid, InputLabel, MenuItem, Select, TextField } from '@mui/material';
import { DatePicker, LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterMoment } from '@mui/x-date-pickers/AdapterMoment';
import moment from 'moment';
import { FC, useState } from 'react';
import medicalToast from '../../components/Toast';
import { useGetDoctorsQuery } from '../../redux/slices/doctorSlice';
import { useCreateTherapyMutation } from '../../redux/slices/therapySlice';

interface Props {
    id: number
}


const AddTherapy: FC<Props> = ({ id }) => {

    const [createTherapy, therapyMutationResult] = useCreateTherapyMutation();
    const doctorResult = useGetDoctorsQuery(undefined);

    const [formData, setFormData] = useState<CreateTherapy>({
        therapyDate: new Date(),
        paymentAmount: 1,
        patientId: id,
        isCame: 0,
        workToBeDone: "",
        workDone: "",
        doctorId: 0

    })

    const handleChange = (event: any) => {
        const { name, value } = event.target;

        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));

    };
    const handleSubmit = (event: any) => {
        event.preventDefault();

        createTherapy(formData)
            .unwrap()
            .then((response) => {
                medicalToast.success("Müayinə əlavə edildi")
            })
            .catch((error) => {
                medicalToast.error("Müayinə əlavə edilərkən xəta baş verdi")
            });

    }

    return (
        <div>
            <form onSubmit={handleSubmit}>
                <Grid container spacing={2}>
                    <Grid item xs={12} sm={6}>
                        <LocalizationProvider dateAdapter={AdapterMoment}>
                            <DatePicker
                            className='w_100 '
                                label="Müayinə tarixi"
                                format="YYYY-MM-DD HH:mm:ss"
                                views={['year', 'month', 'day']}
                                onChange={(date) => {
                                    setFormData(prev => ({
                                        ...prev,
                                        therapyDate: date?.toDate() || new Date()
                                    }))
                                }}
                                defaultValue={moment(moment(formData.therapyDate).format('YYYY-MM-DDTHH:mm:ss.SSS'))}
                                slotProps={{ textField: { size: 'medium', }, }} />
                        </LocalizationProvider>
                    </Grid>

                    <Grid item xs={12} sm={6}>
                        <TextField
                            fullWidth
                            label="Görüləcək işlər"
                            name="workToBeDone"
                            type='text'
                            value={formData.workToBeDone}
                            onChange={handleChange}
                            InputLabelProps={{
                                shrink: true,
                            }}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <TextField
                            fullWidth
                            label="Görülən işlər"
                            name="workDone"
                            type='text'
                            value={formData.workDone}
                            onChange={handleChange}
                            InputLabelProps={{
                                shrink: true,
                            }}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <TextField

                            fullWidth
                            label="Ödəniş"
                            name="paymentAmount"
                            type='number'
                            required={formData.paymentAmount >= 0}
                            error={formData.paymentAmount <= 0}
                            helperText={formData.paymentAmount <= 0 ? "Məbləğ 0-dan böyük olmalıdır" : ""}
                            value={formData.paymentAmount}
                            onChange={handleChange}
                            InputLabelProps={{
                                shrink: true,
                            }}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <FormControl fullWidth>
                            <InputLabel id="therapy-doctor-label">Hekim</InputLabel>
                            <Select
                                fullWidth
                                labelId="therapy-doctor-label"
                                id="doctor"
                                value={formData.doctorId}
                                label="Həkim"
                                name={"doctorId"}
                                required={formData.doctorId != 0}
                                error={formData.doctorId == 0}
                                onChange={handleChange} >
                                {
                                    doctorResult.data?.map((doctor) => (
                                        <MenuItem key={doctor.id} value={doctor.id}>{doctor.name + " " + doctor.surname}</MenuItem>
                                    ))
                                }
                            </Select>
                        </FormControl>
                    </Grid>
                </Grid>
                <Button   style={{ marginTop: "12px",marginInline:"auto" }} type="submit" variant="contained" color="primary">
                        Submit
                    </Button>
            </form>
        </div>
    )
}

export default AddTherapy