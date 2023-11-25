import React from 'react'
import MyModal from '../Common/MedicalModal';
import { Button, Grid, TextField } from '@mui/material';
import crudService from './../../components/CrudService';
import { useCreateDoctorMutation } from '../../redux/slices/doctorSlice';
import medicalToast from '../../components/Toast';

const AddDoctor = () => {
    const [createDoctor] = useCreateDoctorMutation();
    const [open, setOpen] = React.useState(false);
    const [formData, setFormData] = React.useState<CreateDoctor>({
        name: "",
        surname: "",
    });

    const handleChange = (event: any) => {
        const { name, value } = event.target;

        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    }

    const handleSubmit = (event: any) => {
        event.preventDefault();

        let doctor: CreateDoctor = {
            name: formData.name.trim(),
            surname: formData.surname.trim(),
        }
        if (window.confirm("Həkim əlavə edilsin?")) {
            createDoctor(doctor)
                .unwrap()
                .then((originalPromiseResult) => {
                    medicalToast.success("Həkim əlavə edildi!");
                })
                .catch((error) => {
                    medicalToast.error("Həkim əlavə edilərkən xəta baş verdi!");
                });
        }



        setOpen(false);
    }

    return (
        <div style={{ marginBottom: "12px" }}>
            <Button variant='contained' color='error' onClick={() => setOpen(true)}>Həkim əlavə et</Button>
            <MyModal open={open} setOpen={setOpen} title='Yeni Həkim Əlavə edin' >
                <form onSubmit={handleSubmit}>
                    <Grid container spacing={2}>
                        <Grid item xs={12} sm={6}> {/* On small screens (xs), the item will take the full width, and on small+ screens (sm), it will take half the width */}
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

                    </Grid>
                    <div style={{ display: 'flex', justifyContent: 'center', marginTop: '20px' }}>
                        <Button type='submit' color='success' variant='contained'>Submit</Button>
                    </div>
                </form>
            </MyModal>
        </div>
    )
}

export default AddDoctor