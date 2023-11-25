import HighlightOffIcon from '@mui/icons-material/HighlightOff';
import { Accordion, AccordionSummary, Button, Container, FormControl, Grid, IconButton, InputLabel, List, ListItem, ListItemButton, ListItemText, MenuItem, Select, TextField, Typography } from '@mui/material';
import { DatePicker, LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterMoment } from '@mui/x-date-pickers/AdapterMoment';
import moment from 'moment';
import { useCallback, useEffect, useState } from 'react';
import { useDropzone } from 'react-dropzone';
import Converter from '../../components/Converter';
import medicalToast from '../../components/Toast';
import { useGetDoctorsQuery } from '../../redux/slices/doctorSlice';
import { useCreatePatientMutation } from '../../redux/slices/patientSlice';
import MyModal from '../Common/MedicalModal';

import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import { AccordionDetails } from '@mui/material';
const AddPatient = () => {
    const { data: doctors } = useGetDoctorsQuery(undefined);
    const [addPatient, { isError, error, isSuccess }] = useCreatePatientMutation();
    const [open, setOpen] = useState(false);
    const [phoneNumber, setPhoneNumber] = useState("");
    const [images, setImages] = useState<File[]>([]);
    const doctorResult = useGetDoctorsQuery(undefined);
    const [therapyFormData, setTherapyFormData] = useState<CreateTherapy>({
        therapyDate: new Date(),
        paymentAmount: 1,
        patientId: 0,
        isCame: 0,
        workToBeDone: "",
        workDone: "",
        doctorId: 0

    })
    const [formData, setFormData] = useState<CreatePatient>({
        name: "",
        surname: "",
        diagnosis: "",
        address: "",
        generalStateOfHealth: "",
        drugAllergy: "",
        reactionToAnesthesia: "",
        delayedSurgeries: "",
        gender: 0,
        pregnancyStatus: 0,
        injuryProblem: "",
        arrivalDate: new Date(),
        birthDate: new Date(),
        bleeding: "",
        phoneNumbers: [],
        images: [],
        therapies: [],
    });

    useEffect(() => {
        if (formData.gender == 0) {
            setFormData(prev => ({
                ...prev,
                pregnancyStatus: 1,
            }))
        }
    }, [formData.gender])

    const handleAddPhoneNumber = () => {
        setFormData(prev => ({
            ...prev,
            phoneNumbers: prev.phoneNumbers.concat(parseInt(phoneNumber)),
        }))
        setPhoneNumber("");
    }

    const removeImage = (index: number) => {
        setImages(prev => prev.filter((_, i) => i !== index));
    };

    const addImages = useCallback((acceptedFiles: File[]) => {
        setImages((prev) => [...prev, ...acceptedFiles]);
    }, []);

    const { getRootProps: getImagesRootProps, getInputProps: getImagesInputProps } = useDropzone({
        onDrop: addImages,
        accept: '*' as any,
    });


    const handleSubmit = async (event: any) => {
        event.preventDefault();

        const convertImages = async (): Promise<CreatePatient> => {
            const base64Images = await Promise.all(images.map(file => Converter.convertToBase64(file)));
            const updatedFormData = {
                ...formData,
                images: base64Images,
                therapies: [therapyFormData]
            } as CreatePatient;

            return updatedFormData;
        };

        var newPatient: CreatePatient = await convertImages();

        if (window.confirm("Xeste elave edilsin?")) {
            addPatient(newPatient)
                .unwrap()
                .then(() => {
                    medicalToast.success("Xəstə əlavə edildi!")
                })
                .catch((error) => {
                    medicalToast.error("Xəstə əlavə edilərkən xəta baş verdi!")
                });
            setOpen(false);
        }

    }
    const handleChange = (event: any) => {
        const { name, value } = event.target;

        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));

    };

    const handleTherapyChange = (event: any) => {
        const { name, value } = event.target;

        setTherapyFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    }

    const handleRemovePhoneNumber = (index: any) => {
        setFormData((prevData) => ({
            ...prevData,
            phoneNumbers: prevData.phoneNumbers.filter((_, i) => i !== index)
        }));
    };

    return (
        <>
            <Button
                style={{ marginBottom: "10px", marginLeft: "10px" }}
                variant='contained' color="primary"
                onClick={() => setOpen(prev => !prev)}
            >
                Xəstə əlavə et
            </Button>
            <MyModal
                title="Xəstənin detalları"
                open={open}
                setOpen={setOpen}
            >
                <Container fixed maxWidth={"xl"}>
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
                            <Grid item xs={12}>
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
                                        label="Doğum Tarixi"
                                        format="YYYY-MM-DD HH:mm:ss"
                                        views={['year', 'month', 'day']}
                                        onChange={(date) => {
                                            setFormData(prev => ({
                                                ...prev,
                                                birthDate: date?.toDate() || new Date()
                                            }))
                                        }}
                                        defaultValue={moment(moment(formData.birthDate).format('YYYY-MM-DDTHH:mm:ss.SSS'))}
                                        slotProps={{ textField: { size: 'medium', }, }} />
                                </LocalizationProvider>
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <TextField
                                    fullWidth
                                    label="Ünvan"
                                    name="address"
                                    type='text'
                                    value={formData.address}
                                    onChange={handleChange}
                                    InputLabelProps={{
                                        shrink: true,
                                    }}
                                />
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <TextField
                                    fullWidth
                                    label="Ümumi Sağlamlıq"
                                    name="generalStateOfHealth"
                                    type='text'
                                    value={formData.generalStateOfHealth}
                                    onChange={handleChange}
                                    InputLabelProps={{
                                        shrink: true,
                                    }}
                                />
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <TextField
                                    fullWidth
                                    label="Dərman Allerjiyası"
                                    name="drugAllergy"
                                    type='text'
                                    value={formData.drugAllergy}
                                    onChange={handleChange}
                                    InputLabelProps={{
                                        shrink: true,
                                    }}
                                />
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <TextField
                                    fullWidth
                                    label="Narkoz Reaksiyası"
                                    name="reactionToAnesthesia"
                                    type='text'
                                    value={formData.reactionToAnesthesia}
                                    onChange={handleChange}
                                    InputLabelProps={{
                                        shrink: true,
                                    }}
                                />
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <TextField
                                    fullWidth
                                    label="Gecikmiş Əməliyyatlar"
                                    name="delayedSurgeries"
                                    type='text'
                                    value={formData.delayedSurgeries}
                                    onChange={handleChange}
                                    InputLabelProps={{
                                        shrink: true,
                                    }}
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
                                        value={formData.pregnancyStatus}
                                        label="Hamiləlik"
                                        name={"pregnancyStatus"}
                                        onChange={handleChange}
                                    >
                                        <MenuItem value={0}>Bəli</MenuItem>
                                        <MenuItem value={1}>Xeyir</MenuItem>
                                    </Select>
                                </FormControl>
                            </Grid>
                            <Grid container style={{ marginLeft: "0px", marginTop: "6px" }} spacing={2}>
                                <Grid style={{
                                    display: "flex",
                                    alignItems: "flex-start",
                                    justifyContent: "space-between"
                                }} item xs={12} sm={6} >
                                    <TextField
                                        label="Phone Number"
                                        variant="outlined"

                                        style={{ marginBottom: "6px" }}
                                        type='tel'
                                        value={phoneNumber}
                                        inputProps={{
                                            maxLength: 10,

                                            pattern: "^(055|070|051|077|060|010)[0-9]{7}$",
                                            title: "Please enter a valid phone number (e.g., xxx-xxx-xxxx)",
                                        }}
                                        onChange={(e) => { setPhoneNumber(e.target.value.trim()) }} />

                                    <Button
                                        variant="contained"
                                        color="primary"

                                        onClick={handleAddPhoneNumber}
                                    >
                                        Add Phone Number
                                    </Button>
                                </Grid>
                                <Grid item xs={12} sm={6}>
                                    <List>
                                        {formData?.phoneNumbers?.map((number, index) => (
                                            <ListItem key={index}>
                                                <ListItemButton>
                                                    <ListItemText primary={number} />
                                                    <IconButton edge="end" color='error' aria-label="delete" onClick={() => handleRemovePhoneNumber(index)}>
                                                        <HighlightOffIcon />
                                                    </IconButton>
                                                </ListItemButton>
                                            </ListItem>
                                        ))}
                                    </List>
                                </Grid>
                            </Grid>

                            <Grid item xs={12} >
                                <Typography variant="h6" gutterBottom>
                                    Xəstə üçün yeni şəkil əlavə et
                                </Typography>

                                <div>
                                    <div style={{ border: '2px dashed #ccc', padding: '20px', textAlign: 'center' }}>
                                        <Typography variant="subtitle1">Drag and drop images here</Typography>
                                    </div>
                                    {images.length > 0 ? (
                                        <div>
                                            <Typography variant="subtitle1">Selected Images:</Typography>
                                            {images.map((image, index) => (
                                                <div key={index}>
                                                    <img src={URL.createObjectURL(image)} alt={`Selected ${index}`} style={{ maxWidth: '100%', maxHeight: "500px", marginLeft: "auto", marginRight: "auto", marginTop: '10px' }} />
                                                    <Button variant="outlined" color="secondary" onClick={() => removeImage(index)} style={{ marginTop: '5px' }}>
                                                        Remove
                                                    </Button>
                                                </div>
                                            ))}
                                        </div>
                                    ) : (
                                        <Typography>No images selected</Typography>
                                    )}
                                </div>
                                <div {...getImagesRootProps()} style={{ marginTop: '10px' }}>
                                    <input {...getImagesInputProps()} />
                                    <Button style={{ marginBottom: "10px" }} variant="outlined" color="primary">
                                        Add Image
                                    </Button>
                                </div>
                            </Grid>
                            <Accordion className="w_100 ">
                                <AccordionSummary
                                    className="w_100 "
                                    expandIcon={<KeyboardArrowDownIcon />}
                                    aria-controls={`accordion_${therapyFormData.therapyDate}_content`}
                                    id={`accordion_${therapyFormData.therapyDate}_header`}
                                >
                                    <Typography>Xestenin Muayinesi
                                    </Typography>
                                </AccordionSummary>
                                <AccordionDetails  >
                                    <Grid container spacing={2} className="w_100 " >
                                        <Grid item xs={12} sm={6}>
                                            <LocalizationProvider dateAdapter={AdapterMoment}>
                                                <DatePicker
                                                    label="Müayinə tarixi"
                                                    format="YYYY-MM-DD HH:mm:ss"
                                                    views={['year', 'month', 'day']}
                                                    onChange={(date) => {
                                                        setTherapyFormData(prev => ({
                                                            ...prev,
                                                            therapyDate: date?.toDate() || new Date()
                                                        }))
                                                    }}
                                                    defaultValue={moment(moment(therapyFormData.therapyDate).format('YYYY-MM-DDTHH:mm:ss.SSS'))}
                                                    slotProps={{ textField: { size: 'medium', }, }} />
                                            </LocalizationProvider>
                                        </Grid>

                                        <Grid item xs={12} sm={6}>
                                            <TextField
                                                fullWidth
                                                label="Görüləcək işlər"
                                                name="workToBeDone"
                                                type='text'
                                                value={therapyFormData.workToBeDone}
                                                onChange={handleTherapyChange}
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
                                                value={therapyFormData.workDone}
                                                onChange={handleTherapyChange}
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
                                                required={therapyFormData.paymentAmount >= 0}
                                                error={therapyFormData.paymentAmount <= 0}
                                                helperText={therapyFormData.paymentAmount <= 0 ? "Məbləğ 0-dan böyük olmalıdır" : ""}
                                                value={therapyFormData.paymentAmount}
                                                onChange={handleTherapyChange}
                                                InputLabelProps={{
                                                    shrink: true,
                                                }}
                                            />
                                        </Grid>
                                        <Grid item xs={12} sm={6}>
                                            <FormControl fullWidth>
                                                <InputLabel id="therapy-doctor-label">Doctor</InputLabel>
                                                <Select
                                                    fullWidth
                                                    labelId="therapy-doctor-label"
                                                    id="doctor"
                                                    value={therapyFormData.doctorId}
                                                    label="Həkim"
                                                    name={"doctorId"}
                                                    required={therapyFormData.doctorId != 0}
                                                    error={therapyFormData.doctorId == 0}

                                                    onChange={handleTherapyChange}
                                                >
                                                    {
                                                        doctorResult.data?.map((doctor) => (
                                                            <MenuItem key={doctor.id} value={doctor.id}>{doctor.name + " " + doctor.surname}</MenuItem>
                                                        ))
                                                    }
                                                </Select>
                                            </FormControl>
                                        </Grid>

                                    </Grid>
                                </AccordionDetails>
                            </Accordion>

                        </Grid>
                        <Button style={{ marginTop: "12px" }} type="submit" variant="contained" color="primary">
                            Submit
                        </Button>

                    </form>
                </Container>
            </MyModal>
        </>
    )
}

export default AddPatient