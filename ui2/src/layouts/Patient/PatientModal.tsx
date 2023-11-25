import React from 'react';
import MedicalModal from '../Common/MedicalModal';
import ReusableTabs from '../Common/Tabs';
import AddTherapy from '../Therapy/AddTherapy';
import PatientDetail from './PatientDetail';
import PatientImages from './PatientImages';
import PatientTherapies from '../Therapy/PatientTherapies';


interface PatientModalProps {
    id: number;
    open: boolean;
    setOpen: React.Dispatch<React.SetStateAction<boolean>>;

}

const PatientModal: React.FC<PatientModalProps> = ({ id, open, setOpen, }) => {
   
 
    const tabs = [
        {
            label: "Əsas məlumatlar",
            content: <PatientDetail id={id} />
        },
        {
            label: "Şəkillər",
            content: <PatientImages id={id} />

        },
        {
            label: "Müayinələr",
            content: <PatientTherapies id={id} />
        },
        {
            label: "Yeni müayinə",
            content: <AddTherapy id={id} />
        }
    ]

    return (
        <MedicalModal title="Xəstənin detalları" open={open} setOpen={setOpen}>
            <ReusableTabs tabs={tabs} />
        </MedicalModal>
    )
}

export default PatientModal;