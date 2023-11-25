type User = {
    id: number,
    createDate: Date,
    updateDate: Date | null,
    userName: string,
    password: string,
    accessToken: string,
    role: Role
}

type Doctor = {
    id: number,
    createDate: Date,
    updateDate: Date | null,
    name: string,
    surname: string,
    doctorPatients: DoctorPatients[],
}

type Patient = {
    id: number,
    createDate: Date,
    updateDate: Date | null,
    name: string,
    surname: string,
    diagnosis: string,
    arrivalDate: Date,
    totalAmount: number,
    doctorId: number,
    doctor: Doctor,
    adress: string?,
    generalStateOfHealth: string?,
    drugAllergy: string?,
    reactionToAnesthesia: string?,
    delayedSurgeries: string?,
    gender: Gender,
    pregnancyStatus: PregnancyStatus,
    injuryProblem: string?,
    birthDate: Date?,
    bleeding: string?,

    images: ImageType[],
    therapies: Therapy[],
    phoneNumbers: PhoneNumber[],
    doctorPatients: DoctorPatient[]

}
type DoctorPatient = {
    id: number,
    doctorId: number,
    patientId: number,
    doctor: Doctor,
    patient: Patient

}
type Payment = {
    id: number,
    createDate: Date,
    updateDate: Date | null,
    therapy: Therapy,
    paymentDate: Date,
    therapyId: number,
    paymentAmount: number
}

type ImageType = {
    id: number,
    createDate: Date,
    updateDate: Date | null,
    imageData: byte[],
    patientId: number,
    patient: Patient
}

type Therapy = {
    therapyDate: Date,
    paymentAmount: number,
    patientId: number,
    patient: Patient,
    payments: Payment[],
    id: number,
    createDate: Date,
    updateDate: Date | null,
    workToBeDone: string?,
    workDone: string?,
}
type PhoneNumber = {
    id: number,
    createDate: Date,
    updateDate: Date | null,
    number: number,
    patientId: number,
    patient: Patient
}

type Role = 0 | 1;
type Gender = 0 | 1;
type PregnancyStatus = 0 | 1;
type IsCame = 0 | 1;


type CreateUser = {
    userName: string,
    password: string,
    role: Role
}

type CreateDoctor = {
    name: string,
    surname: string
}

type CreatePatient = {
    name: string;
    surname: string;
    address?: string | null;
    diagnosis: string;
    generalStateOfHealth?: string | null;
    drugAllergy?: string | null;
    reactionToAnesthesia?: string | null;
    delayedSurgeries?: string | null;
    gender: Gender;
    pregnancyStatus: PregnancyStatus;
    injuryProblem?: string | null;
    arrivalDate: Date;
    birthDate?: Date | null;
    bleeding?: string | null;

    phoneNumbers: number[];
    images: string[];
    therapies: CreateTherapy[];


}

type CreatePayment = {
    therapyId: number,
    paymentAmount: number,
    paymentDate: Date
}

type CreateNotification ={
    title:string,
    content:string,
    notificationDate:Date,
    userId:number 
}

type CreateImage = {
    imageDate: Date,
    imageData: string
    patientId: number

}
type CreatePhoneNumber = {
    number: number,
    patientId: number
}
type CreateTherapy = {
    therapyDate: Date,
    paymentAmount: number,
    patientId: number,
    isCame: IsCame,
    workToBeDone: string,
    workDone: string,
    doctorId:number, 
}

type UpdateUser = {
    id: number,
    userName: string,
    password: string,
    role: Role
}

type UpdateDoctor = {
    id: number,
    name: string,
    surname: string
}

type UpdatePatient = {
    id: number,
    name: string,
    surname: string,
    diagnosis: string,
    arrivalDate: Date,
    generalStateOfHealth: string,
    drugAllergy: string,
    reactionToAnesthesia: string,
    delayedSurgeries: string,
    gender: Gender,
    pregnancyStatus: PregnancyStatus,
    injuryProblem: string,
    birthDate: Date,
    bleeding: string, 
    
    address: string,

}
type UpdateNotification ={
    id:number,
    title:string,
    content:string,
    notificationDate:Date,
    userId:number 
}
type UpdatePayment = {
    id: number,
    patientId: number,
    paymentAmount: number,
    paymentDate: Date,
    therapyId: number
}

type UpdateTherapy = {
    id: number,
    therapyDate: Date,
    paymentAmount: number,
    patientId: number,
    isCame: IsCame,
    workToBeDone: string,
    workDone: string,
    doctorId:number,
}

type UpdateImage = {
    id: number,
    imageData: byte[],
    patientId: number
}

type PatientResponse = {
    id: number,
    name: string,
    surname: string,
    diagnosis: string,
    arrivalDate: Date,
    address: string?,
    generalStateOfHealth: string?,
    drugAllergy: string?,
    reactionToAnesthesia: string?,
    delayedSurgeries: string?,
    gender: Gender,
    pregnancyStatus: PregnancyStatus?,
    injuryProblem: string?,
    birthDate: Date?,
    bleeding: string?,

    images: ImageResponse[],

}
type MyPaymentResponse = {
    id: number,
    paymentAmount: number,
    paymentDate: Date,
    patientId: number,
    patientName: string,
    patientSurname: string,
    doctorName: string,
    doctorSurname: string,
    therapyId: number,
}
type DoctorResponse = {
    id: number,
    name: string,
    surname: string 
}
type NotificationResponse ={
    id :number,
    title:string,
    content:string,
    notificationDate:Date
}

type TherapyResponse = {
    id: number,
    therapyDate: Date,
    paymentAmount: number,
    patientId: number,
    patientName: string,
    patientSurname: string,
    doctorId: number,
    doctorName: string,
    doctorSurname: string,
    isCame: IsCame,
    payments: MyPaymentResponse[],
    workToBeDone: string?,
    workDone: string?,
}



type DateIntervalRequest = {
    fromDate: Date | null,
    toDate: Date | null
}

type LoginUser = {
    userName: string,
    password: string
}
type AuthResponse = {
    accessToken: string,
    userId:int,
    role: Role

}
type RefreshTokenDto = {
    accessToken: string,
}


type UserResponse =
    {
        id: number,
        userName: string,
        password: string,
        role: Role
    }

type ImageResponse = {
    id: number,
    imageData: byte[],
    patientId: number
}