import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import MailIcon from '@mui/icons-material/Mail';
import { Accordion, AccordionDetails, AccordionSummary, Badge, Button, Grid, IconButton, TextField, Tooltip, Typography } from '@mui/material';
import React, { useEffect } from 'react';
import { useSelector } from 'react-redux';
import { selectAll } from '../../redux/slices/authSlice';
import { useCreateNotificationMutation, useDeleteNotificationMutation, useGetNotificationsByIdQuery } from '../../redux/slices/notificationSlice';
import PatientTherapyPayment from '../Payment/PatientTherapyPayment';
import MedicalModal from './MedicalModal';
import ReusableTabs from './Tabs';
import medicalToast from '../../components/Toast';
import { DatePicker, LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterMoment } from '@mui/x-date-pickers/AdapterMoment';
import moment from 'moment';


const Notifications = () => {
    const [open, setOpen] = React.useState(false);
    const user = useSelector(selectAll);
    const notificationResult = useGetNotificationsByIdQuery(user.userId as number);
    const tabs = [
        {
            label: "Bildirisler",
            content: <Notification />
        },
        {
            label: "Yeni Bildiris",
            content: <AddNotification />

        }
    ]
    return (
        <>
            <IconButton onClick={() => setOpen(true)} size="large" aria-label="show 4 new mails" color="inherit">
                <Tooltip title="Notifications">
                    <Badge badgeContent={notificationResult.data?.length} color="secondary">
                        <MailIcon color="action" />
                    </Badge >
                </Tooltip >
            </IconButton >
            <MedicalModal title='Notificatons' open={open} setOpen={setOpen}   >
                <ReusableTabs tabs={tabs} />
            </MedicalModal>

        </>
    )
}

export default Notifications



const Notification = () => {
    const user = useSelector(selectAll);
    const notificationResult = useGetNotificationsByIdQuery(user.userId as number);
    const [deleteNotification, notificationDeleteResult] = useDeleteNotificationMutation();


    useEffect(() => {
        notificationResult.refetch();
    }, [user.userId])

    const handleShowNotification = (notificationId: number) => {
        console.log(notificationId);
        if (window.confirm("Bildiris silinsin?")) {
            deleteNotification(notificationId)
                .unwrap()
                .then((response) => {
                    console.log(response);
                    medicalToast.success("Bildiris gorudldu!");
                })
                .catch((error) => {
                    console.log(error);
                    medicalToast.error("Xəta baş verdi!");
                });
        }
    }

    return (
        <>
            {
                notificationResult.isLoading ? <div>Loading...</div> :

                    notificationResult.isError ? < div > Xeta Bas verdi...</div > :

                        notificationResult.data?.length === 0 ? <div>No notifications</div> :

                            notificationResult.data?.map((notification) =>
                                <Accordion key={notification.id} >
                                    <AccordionSummary
                                        expandIcon={<KeyboardArrowDownIcon />}
                                        aria-controls={`accordion_notification_content`}
                                        id={`accordion_notification_header`}
                                    >
                                        <Typography>{notification.title}   {moment(notification.notificationDate).format('YYYY-MM-DD HH:mm:ss')}</Typography>
                                    </AccordionSummary>
                                    <AccordionDetails style={{ width: "100% !important", textAlign: "center" }}>
                                        <Typography style={{ textAlign: "left", padding: "8px" }}>
                                            {notification.content}   
                                        </Typography>
                                        <Button onClick={() => { handleShowNotification(notification.id) }} variant="contained" color="success" >
                                            Goruldu
                                        </Button>
                                    </AccordionDetails>
                                </Accordion>
                            )
            }
        </>
    )
}



const AddNotification = () => {

    const user = useSelector(selectAll);
    const [createNotification, notificationAddResult] = useCreateNotificationMutation();
    const notificationResult = useGetNotificationsByIdQuery(user.userId as number);
    const [formData, setFormData] = React.useState<CreateNotification>({
        title: "",
        content: "",
        userId: user.userId as number,
        notificationDate: new Date()
    });
    useEffect(() => {
        notificationResult.refetch();
    }, [user.userId])
    const handleChange = (event: any) => {
        const { name, value } = event.target;

        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));

    };


    const handleAddNotification = () => {
        console.log(formData);
        if (window.confirm("Bildiris elave edilsin?")) {
            createNotification(formData)
                .unwrap()
                .then((response) => {
                    console.log(response);
                    medicalToast.success("Bildiris elave edildi!");
                })
                .catch((error) => {
                    console.log(error);
                    medicalToast.error("Xəta baş verdi!");
                });
        }
    }


    return (
        <form>
            <Grid container spacing={2}>
                <Grid item xs={12}>
                    <TextField
                        fullWidth
                        autoComplete="title"
                        name="title"
                        required
                        label="Basliq"
                        InputLabelProps={{
                            shrink: true,
                        }}
                        onChange={handleChange}
                        value={formData?.title}
                    />
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        fullWidth
                        autoComplete="content"
                        name="content"
                        required
                        label="Mezmun"
                        InputLabelProps={{
                            shrink: true,
                        }}
                        onChange={handleChange}
                        value={formData?.content}
                    />
                </Grid>
                <Grid item xs={12}>
                    <LocalizationProvider dateAdapter={AdapterMoment}>
                        <DatePicker
                            className='date_picker w_100'
                            label="Müayinə tarixi"
                            views={['year', 'month', 'day']}
                            format="YYYY-MM-DD HH:mm:ss"
                            value={moment(moment(formData.notificationDate).format('YYYY-MM-DDTHH:mm:ss.SSS'))}
                            onChange={(date) => {
                                setFormData(prev => ({
                                    ...prev,
                                    therapyDate: date?.toDate() || new Date()
                                }))
                            }} />
                    </LocalizationProvider>
                </Grid>

                <Grid item xs={12}>
                    <Button onClick={handleAddNotification} variant="contained" color="success" >
                        Elave et
                    </Button>
                </Grid>
            </Grid>



        </form>
    )
}