import HighlightOffIcon from '@mui/icons-material/HighlightOff';
import { Button, Grid, IconButton, Typography } from '@mui/material';
import { FC, useCallback } from 'react';
import { useDropzone } from 'react-dropzone';
import Converter from '../../components/Converter';
import medicalToast from '../../components/Toast';
import { useCreateImageMutation, useDeleteImageMutation } from '../../redux/slices/imageslice';
import { useGetPatientByIdQuery } from '../../redux/slices/patientSlice';

interface Props {
    id: number
}

const PatientImages: FC<Props> = ({ id }) => {

    const patientResult = useGetPatientByIdQuery(id);
    const [deleteImage, deleteImageResult] = useDeleteImageMutation();
    const [createImage, createImageResult] = useCreateImageMutation();



    const addImages = useCallback(async (acceptedFiles: File[]) => {

        let imageData = await Promise.resolve(Converter.convertToBase64(acceptedFiles[0])) ?? "";

        var image: CreateImage = {
            imageData: imageData,
            patientId: id,
            imageDate: new Date()
        }
        createImage(image)
            .unwrap()
            .then(() => {
                medicalToast.success("Eklendi")
            })
            .catch(() => {
                medicalToast.error("Eklenemedi")
            })
    }, []);

    const { getRootProps: getImagesRootProps, getInputProps: getImagesInputProps } = useDropzone({
        onDrop: addImages,
        accept: '*' as any,
    });

    const handleRemoveImage = (imageId: number) => {
        if (window.confirm("Emin misiniz?")) {
            deleteImage(imageId)
                .unwrap()
                .then(() => {
                    medicalToast.success("Silindi")
                })
                .catch((error) => {
                    medicalToast.error("Silinemedi")
                })
        }
    }


    return (
        <Grid xs={12} item>
               <div {...getImagesRootProps()} style={{ marginTop: '10px' }}>
                <input {...getImagesInputProps()} />
                <Button variant="outlined" color="primary">
                    Add Image
                </Button>
            </div>
            {patientResult.data?.images && patientResult.data?.images?.length > 0 ? (
                <div className='image_grid'>
                    {patientResult.data?.images.map((image, index) => (

                        <div style={{ display: "20px", position: "relative" }} key={index}>
                            <img src={`${image.imageData.toLocaleString()}`} alt={`Selected ${index}`} style={{ maxWidth: '100%', marginTop: '10px' }} />

                            <IconButton color='error' style={{ position: 'absolute', top: "5px", right: "0" }} onClick={() => handleRemoveImage(image.id)}>
                                <HighlightOffIcon fontSize='large' />
                            </IconButton>
                        </div>
                    ))}
                </div>
            ) : (
                <Typography>No images selected</Typography>
            )}
         
        </Grid>
    )
}

export default PatientImages