import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import { Accordion, AccordionDetails, AccordionSummary, Typography } from '@mui/material';
import moment from 'moment';
import { FC, useState } from 'react';
import { useGetTherapysByPatientIdQuery } from '../../redux/slices/therapySlice';
import PatientTherapy from './PatientTherapy';

interface Props {
    id: number
}

const PatientTherapies: FC<Props> = ({ id }) => {

    const therapyResult = useGetTherapysByPatientIdQuery(id);


    return (
        <div>
            {therapyResult.isLoading && <div>Yüklenir...</div>}
            {therapyResult.isError && <div>Xəta baş verdi</div>}
            {therapyResult.isSuccess && therapyResult.data && therapyResult.data.length === 0 &&
                <div>Müayinə tapılmadı</div>
            }
            {therapyResult.isSuccess && therapyResult.data && therapyResult.data.length > 0 &&
                therapyResult.data?.map((therapy, index) => (
                    <Accordion style={{ marginBottom: "10px" }}>
                        <AccordionSummary
                            expandIcon={<KeyboardArrowDownIcon />}
                            aria-controls={`accordion_${therapy.id}_content`}
                            id={`accordion_${therapy.id}_header`}
                        >
                            <Typography>Muayine {moment(therapy.therapyDate).format('MMMM D, YYYY HH:mm').toString()}
                            </Typography>
                        </AccordionSummary>
                        <AccordionDetails>
                            <PatientTherapy therapy={therapy} />
                        </AccordionDetails>
                    </Accordion>
                ))}
        </div>
    )
}

export default PatientTherapies