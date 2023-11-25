

class ConstantClas {

    public constructor() {

    }
    public readonly API_URL = process.env.REACT_APP_API_URL;
    public readonly API_USERNAME = process.env.REACT_APP_API_USERNAME;
    public readonly API_PASSWORD = process.env.REACT_APP_API_PASSWORD;
    public readonly AUTH_HEADER = `Basic ${btoa(`${this.API_USERNAME}:${this.API_PASSWORD}`)}`;
    public readonly JWT_SECRET = process.env.REACT_APP_JWT_SECRET;
    public readonly DEFAULT_JWT = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJwYXNzd29yZCI6IiQyYSQxMSRmZmVKY2k4a0pnOUo1Ui5TbFd1QVZPRHBhQ0c4d09SRG1XU0pSNHkzdXlML0JDekxLTjdkZSIsIm5hbWUiOiJ5dXNpZiIsInJvbGUiOiJBZG1pbiIsImV4cCI6MTY5MjE2Mzc4MywiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIn0.c12OEuIpR_oJbzWqtEZ8kHj_W_hYLxi2WEt4ZcX9LxU";
}





export const PATIENT_COLS = {
    id: "id",
    name: "name",
    surname: "surname",
    diagnosis: "diagnosis",
    arrivalDate: "arrivalDate",
    address: "address",
    generalStateOfHealth: "generalStateOfHealth",
    drugAllergy: "drugAllergy",
    reactionToAnesthesia: "reactionToAnesthesia",
    delayedSurgeries: "delayedSurgeries",
    gender: "gender",
    pregnancyStatus: "pregnancyStatus",
    injuryProblem: "injuryProblem",
    birthDate: "birthDate",
    bleeding: "bleeding",
    details: "details",
}
export const DOCTOR_ROW = {
    id: "id",
    fullName: "fullName",
    name: "name",
    surname: "surname",



}

export const PAYMENT_ROW = {
    id: "id",
    amount: "amount",
    paymentDate: "paymentDate",
    patientId: "patientId",
    patientFullName: "patientFullName",
    doctorFullName: "doctorFullName",
}
export const USERS_ROW = {
    id: "id",
    userName: "userName",
    password: "password",
    role: "role",

}



const Constant = new ConstantClas();
export default Constant;
