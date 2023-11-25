import { createTheme } from "@mui/material";
 
 

const lightTheme = createTheme({
  palette: {
    mode: 'light', // Set the theme mode to light
    primary: {
      main: '#5503c7',
    },
    divider: '#5503c7',
  },
});

const darkTheme = createTheme({
  palette: {
    mode: 'dark', // Set the theme mode to dark
    primary: {
      main: '#5503c7',
    },
    background: {
        default: '#111827',
        paper: '#05132e',
    },
    text:{
        primary: '#f1f1f1',
    },
    divider: '#fff',
  },
});
 
export { lightTheme, darkTheme };
