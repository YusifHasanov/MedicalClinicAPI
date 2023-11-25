import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterMoment } from '@mui/x-date-pickers/AdapterMoment';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import moment from 'moment';
import { FC } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { selectInterval, setDates } from '../../redux/slices/intervalSlice';


interface IntervalProps {
 
  children?: React.ReactNode | React.ReactNode[];
}
const DateInterval: FC<IntervalProps> = ({  children }) => {


  const interval = useSelector(selectInterval);
  const dispatch = useDispatch();
   
  return (
    <div className='date_interval_container' >
      <LocalizationProvider dateAdapter={AdapterMoment}>
        <DatePicker
          className='date_picker'

          // viewRenderers={{
          //     hours: renderTimeViewClock,
          //     minutes: renderTimeViewClock,
          //     seconds: renderTimeViewClock,
          // }}
          views={['year', 'month', 'day']}

          format="YYYY-MM-DD HH:mm:ss"
          value={moment(moment(interval.fromDate).format('YYYY-MM-DDTHH:mm:ss.SSS'))}
          onChange={(date) => { dispatch(setDates({ fromDate: date?.toDate(), toDate: interval.toDate })) }}
          label="Başlanğıc tarixi" slotProps={{ textField: { size: 'small' } }} />
        <>
          &nbsp;&nbsp;
        </>
        <DatePicker
          className='date_picker'
          label="Bitiş tarixi"
          views={['year', 'month', 'day']}
          format="YYYY-MM-DD HH:mm:ss"
          onChange={(date) => { dispatch(setDates({ fromDate: interval.fromDate, toDate: date?.toDate() })) }}
          value={moment(moment(interval.toDate).format('YYYY-MM-DDTHH:mm:ss.SSS'))}
          slotProps={{ textField: { size: 'small' } }} />
        {/* <Button
                onClick={refetch}
                style={{ marginLeft: "10px" }}
                variant='contained'>Sorğula</Button> */}
      </LocalizationProvider>
      {
        children  && children
      }
       
    </div>



  )
}

export default DateInterval