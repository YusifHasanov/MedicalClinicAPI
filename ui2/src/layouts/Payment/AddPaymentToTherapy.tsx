

import { Button, Grid, TextField, Typography } from '@mui/material';
import { DatePicker, LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterMoment } from '@mui/x-date-pickers/AdapterMoment';
import moment from 'moment';
import React, { FC, useState } from 'react';
import myToast from '../../components/Toast';
import { useCreatePaymentMutation } from '../../redux/slices/paymentSlice';

interface AddPaymentToTherapyProps {
  id: number;

  open: boolean;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
}
 

const AddPaymentToTherapy: FC<AddPaymentToTherapyProps> = ({ id, open, setOpen }) => {

  const [createPayment, paymentMutationResult] = useCreatePaymentMutation();
  

  const [formData, setFormData] = useState<CreatePayment>({
    paymentAmount: 1,
    therapyId: id,
    paymentDate: new Date(),
  });


  const handleSubmit = (event: any) => {
    event.preventDefault();
    console.log(formData);

    if (window.confirm("Ödəniş əlavə edilsin?")) {
      createPayment(formData)
        .unwrap()
        .then((originalPromiseResult) => {
          console.log(originalPromiseResult);
          myToast.success("Ödəniş əlavə edildi!");
          setOpen(false);
        })
        .catch((error) => {
          console.log(error);
          myToast.error("Ödəniş əlavə edilərkən xəta baş verdi!");

        });
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
    <>
      <form onSubmit={handleSubmit}>
        <Typography variant="h6" gutterBottom component="div">
          Ödəniş əlavə et
        </Typography>
        <Grid sx={{ backgroundColor: "text.main" }} container spacing={2}>
          <Grid item xs={12} sm={4}> {/* On small screens (xs), the item will take the full width, and on small+ screens (sm), it will take half the width */}
            <LocalizationProvider dateAdapter={AdapterMoment}>
              <DatePicker
                className='date_picker w_100'
                label="Ödəniş tarixi"
                format="YYYY-MM-DD HH:mm:ss"
                views={['year', 'month', 'day']}
                onChange={(date) => {
                  setFormData(prev => ({
                    ...prev,
                    paymentDate: date?.toDate() || new Date()
                  }))
                }
                }
                defaultValue={moment(moment(formData.paymentDate).format('YYYY-MM-DDTHH:mm:ss.SSS'))}
              />

            </LocalizationProvider>
          </Grid>
          <Grid item xs={12} sm={4}>
            <TextField
              fullWidth
              label="Məbləğ"
              name="paymentAmount"
              required={formData?.paymentAmount >= 0}
              error={formData?.paymentAmount <= 0}
              helperText={formData?.paymentAmount <= 0 ? "Məbləğ 0-dan böyük olmalıdır" : ""}
              InputLabelProps={{
                shrink: true,
              }}
              value={formData.paymentAmount}
              onChange={handleChange}
            />
          </Grid>
          <Grid item xs={12} sm={4} style={{
            display: "flex",
            alignItems: "center",
            paddingBlock: "10px"
          }}>
            <Button
              type="submit"
              fullWidth
              variant="contained"
              color="primary"
              size="large"
            >
              Ödəniş et
            </Button>
          </Grid>
        </Grid>
      </form>
    </>
  )
}

export default AddPaymentToTherapy

