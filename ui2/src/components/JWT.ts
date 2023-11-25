import exp from "constants";
import jwtDecode from "jwt-decode";


export const isValidJWT = (accessToken: string | null) => {
    if (!accessToken) {
        return false;
    }

    try {
        // Use jsonwebtoken for verification
        const decoded = jwtDecode<{ exp: number }>(accessToken);
        const expirationDate = decoded.exp * 1000;
        return Date.now() < expirationDate;
    } catch (error) {
        console.log(error)
        return false;
    }
};


 