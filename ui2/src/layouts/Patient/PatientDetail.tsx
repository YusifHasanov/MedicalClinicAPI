import { Button, Container, FormControl, Grid, InputLabel, MenuItem, Select, TextField } from '@mui/material';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterMoment } from '@mui/x-date-pickers/AdapterMoment';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import moment from 'moment';
import { FC, useEffect, useState } from 'react';
import myToast from './../../components/Toast';
import { useGetPatientByIdQuery, useUpdatePatientMutation } from './../../redux/slices/patientSlice';


interface Props {
    id: number
}
const PatientDetail: FC<Props> = ({ id }) => {

    const patientResult = useGetPatientByIdQuery(id);
    const [updatePatientM, updatePatientResult] = useUpdatePatientMutation();


    const [formData, setFormData] = useState({
        id: 0,
        name: "",
        surname: "",
        diagnosis: "",
        arrivalDate: new Date(),
        address: "",
        generalStateOfHealth: "",
        drugAllergy: "",
        reactionToAnesthesia: "",
        delayedSurgeries: "",
        gender: 0 as Gender,
        pregnancyStatus: 0 as PregnancyStatus,
        injuryProblem: "",
        birthDate: new Date(),
        bleeding: "",
        images: []
    });

    useEffect(() => {
        const { data } = patientResult;
        setFormData({
            id: data?.id,
            name: data?.name,
            surname: data?.surname,
            diagnosis: data?.diagnosis,
            arrivalDate: data?.arrivalDate,
            address: data?.address,
            generalStateOfHealth: data?.generalStateOfHealth,
            drugAllergy: data?.drugAllergy,
            reactionToAnesthesia: data?.reactionToAnesthesia,
            delayedSurgeries: data?.delayedSurgeries,
            gender: data?.gender,
            pregnancyStatus: data?.pregnancyStatus,
            injuryProblem: data?.injuryProblem,
            birthDate: data?.birthDate,
            bleeding: data?.bleeding,
            images: data?.images
        } as any);
    }, [patientResult.isSuccess]);

    const handleSubmit = (event: any) => {
        event.preventDefault();

        const {
            id,
            name,
            surname,
            diagnosis,
            arrivalDate,
            address,
            generalStateOfHealth,
            drugAllergy,
            reactionToAnesthesia,
            delayedSurgeries,
            gender,
            pregnancyStatus,
            injuryProblem,
            birthDate,
            bleeding
        } = formData;

        const updatePatient: UpdatePatient = {
            id, name, surname, diagnosis, arrivalDate, address, generalStateOfHealth,
            drugAllergy, reactionToAnesthesia, delayedSurgeries, gender, pregnancyStatus,
            injuryProblem, birthDate, bleeding
        };

        if (window.confirm("Eminsne")) {
            updatePatientM({ data: updatePatient, id }).unwrap()
                .then(() => myToast.success("Uğurla yeniləndi"))
                .catch((error) => myToast.error("Xəta baş verdi"))
        }

    };


    const handleChange = (event: any) => {
        const { name, value } = event.target;
        console.log(name, value);

        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));

    };


    return (
        <Container fixed
            maxWidth={false}
        >
            <form onSubmit={handleSubmit}>
                <Grid container spacing={2}>
                    <Grid item xs={12} sm={6}> 
                        <TextField
                            fullWidth
                            label="Ad"
                            InputLabelProps={{
                                shrink: true,
                            }}
                            name="name"
                            value={formData.name}
                            onChange={handleChange}
                            required={formData?.name?.length < 3}
                            error={formData?.name?.length < 3}
                            helperText={formData?.name?.length < 3 ? "Ad minimum 3 hərf olmalıdız" : ""}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <TextField
                            fullWidth
                            InputLabelProps={{
                                shrink: true,
                            }}
                            label="Soyad"
                            name="surname"
                            value={formData.surname}
                            onChange={handleChange}
                            required={formData?.surname?.length < 3}
                            error={formData?.surname?.length < 3}
                            helperText={formData?.surname?.length < 3 ? "Soyad minimum 3 hərf olmalıdı" : ""}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <TextField
                            fullWidth
                            label="Diaqnoz"
                            name="diagnosis"
                            type='text'
                            value={formData.diagnosis}
                            onChange={handleChange}
                            InputLabelProps={{
                                shrink: true,
                            }}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <LocalizationProvider dateAdapter={AdapterMoment}>
                            <DatePicker
                                className='date_picker'
                                label="Bitiş tarixi"
                                views={['year', 'month', 'day']}
                                format="YYYY-MM-DD HH:mm:ss"
                                onChange={(date) => {
                                    setFormData(prev => ({
                                        ...prev,
                                        arrivalDate: date?.toDate() ?? new Date()
                                    }))
                                }}
                                value={moment(moment(formData.arrivalDate).format('YYYY-MM-DDTHH:mm:ss.SSS'))}
                                slotProps={{ textField: { size: 'small' } }} />
                        </LocalizationProvider>

                    </Grid>

                    <Grid item xs={12} sm={6}>
                        <TextField
                            fullWidth
                            label="Ünvan"
                            name="address"
                            InputLabelProps={{
                                shrink: true,
                            }}
                            value={formData.address}
                            onChange={handleChange}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <TextField

                            fullWidth
                            label="Ümumi sağlamlıq"
                            name="generalStateOfHealth"
                            InputLabelProps={{
                                shrink: true,
                            }}
                            value={formData.generalStateOfHealth}
                            onChange={handleChange}
                        />

                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <TextField
                            fullWidth
                            label="Dərman alerjisi"
                            name="drugAllergy"
                            InputLabelProps={{
                                shrink: true,
                            }}
                            value={formData.drugAllergy}
                            onChange={handleChange}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>

                        <TextField
                            fullWidth
                            label="Narkoz reaksiyası"
                            name="reactionToAnesthesia"
                            InputLabelProps={{
                                shrink: true,
                            }}
                            value={formData.reactionToAnesthesia}
                            onChange={handleChange}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <TextField

                            fullWidth
                            label="Gecikmiş əməliyyatlar"
                            name="delayedSurgeries"
                            InputLabelProps={{
                                shrink: true,
                            }}
                            value={formData.delayedSurgeries}
                            onChange={handleChange}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <TextField
                            fullWidth
                            label="anesteziya reaksiyası"
                            name="reactionToAnesthesia"
                            InputLabelProps={{
                                shrink: true,
                            }}
                            value={formData.reactionToAnesthesia}
                            onChange={handleChange}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <TextField
                            fullWidth
                            label="Yaralanma problemi"
                            name="injuryProblem"
                            InputLabelProps={{
                                shrink: true,
                            }}
                            value={formData.injuryProblem}
                            onChange={handleChange}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <TextField
                            fullWidth
                            label="Qan tezliqi"
                            name="bleeding"
                            InputLabelProps={{
                                shrink: true,
                            }}
                            value={formData.bleeding}
                            onChange={handleChange}
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <FormControl fullWidth>
                            <InputLabel id="gender-label">Cinsi</InputLabel>
                            <Select
                                fullWidth
                                labelId="gender-label"
                                id="cinsi"
                                value={formData.gender}
                                label="Cinsi"
                                name={"gender"}
                                onChange={handleChange}
                            >
                                <MenuItem value={0}>Kişi</MenuItem>
                                <MenuItem value={1}>Qadin</MenuItem>
                            </Select>
                        </FormControl>
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <FormControl fullWidth>
                            <InputLabel id="hamilə-label">Hamiləlik </InputLabel>
                            <Select
                                fullWidth
                                labelId="hamilə-label"
                                id="hamilə"
                                value={formData.gender == 0 ? 1 : formData.pregnancyStatus}
                                label="Hamiləlik"
                                name={"pregnancyStatus"}
                                onChange={handleChange}
                            >
                                <MenuItem value={0}>Bəli</MenuItem>
                                <MenuItem value={1}>Xeyir</MenuItem>
                            </Select>
                        </FormControl>
                    </Grid>
                </Grid>
                <Button style={{ marginTop: "12px" }} type="submit" variant="contained" color="primary">
                    Submit
                </Button>
            </form>
        </Container>
    )
}

export default PatientDetail