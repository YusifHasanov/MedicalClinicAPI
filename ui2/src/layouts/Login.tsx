import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Avatar from '@mui/material/Avatar';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Container from '@mui/material/Container';
import CssBaseline from '@mui/material/CssBaseline';
import Grid from '@mui/material/Grid';
import Link from '@mui/material/Link';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import TextField from '@mui/material/TextField';
import Typography from '@mui/material/Typography';
import * as React from 'react';
import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { isValidJWT } from '../components/JWT';
import { selectIsValidToken, setAll, setIsValidToken, setRole, setUserId } from '../redux/slices/authSlice';
import { useLoginUserMutation, useRefreshAccessTokenMutation } from '../redux/slices/userSlice';
import Toast from './../components/Toast';
import Constant from '../components/Constants';

function Copyright(props: any) {
  return (
    <Typography variant="body2" color="text.secondary" align="center" {...props}>
      {'Copyright © '}
      <Link color="inherit" href="https://mui.com/">
        Your Website
      </Link>{' '}
      {new Date().getFullYear()}
      {'.'}
    </Typography>
  );
}

// TODO remove, this demo shouldn't need to reset the theme.
const defaultTheme = createTheme();

const SignIn = () => {


  const [refreshToken, { isSuccess: isSuccessRefresh, data: refreshData, isLoading, isError }] = useRefreshAccessTokenMutation();
  const [loginUser, { isSuccess, data }] = useLoginUserMutation();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const token = localStorage.getItem("accessToken");
  const isValidToken = useSelector(selectIsValidToken);
  const [formData, setFormData] = useState<LoginUser>({
    userName: "",
    password: ""
  })
  const handleChange = (event: any) => {
    const { name, value } = event.target;

    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));

  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    try {
 
 

      const result = await loginUser(formData)

      if ('data' in result) {
        localStorage.setItem("accessToken", result.data?.accessToken || "");
        dispatch(setAll(result));
        Toast.success("Uğurla daxil oldunuz");
        navigate("/patients");
      } 
    } catch (error: any) {
      Toast.error(error?.message || "Daxil olarkən xəta baş verdi");
    }
  };

  useEffect(() => {

    if (isValidJWT(token)) {

      navigate('/patients');

    } else if (!isValidToken && token) {

      refreshToken({ accessToken: token })
        .unwrap()
        .then((result) => {
          
          localStorage.setItem('accessToken', result.accessToken);
          dispatch(setIsValidToken(true));

        })
        .catch((error) => {

          localStorage.setItem('accessToken', data?.accessToken ?? Constant.DEFAULT_JWT);
          dispatch(setIsValidToken(true));

        });

    }

  }, [dispatch, isValidToken, refreshToken, token, navigate]);

  return (
    <ThemeProvider theme={defaultTheme}>
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            marginTop: 8,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
          }}
        >
          <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Sign in
          </Typography>
          <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
            <TextField
              margin="normal"
              required
              fullWidth
              id="userName"
              label="Usrename"
              name="userName"
              autoComplete="userName"
              type='text'
              onChange={handleChange}
              value={formData.userName}
              autoFocus
            />
            <TextField
              margin="normal"
              required
              fullWidth
              name="password"
              label="Password"
              type="password"
              id="password"
              autoComplete="password"
              onChange={handleChange}
              value={formData.password}
            />
            {/* <FormControlLabel
              control={<Checkbox value="remember" color="primary" />}
              label="Remember me"
            /> */}
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Daxil ol
            </Button>
            <Grid container>
              {/* <Grid item xs>
                <Link href="#" variant="body2">
                  Parolu unutmusunuz?
                </Link>
              </Grid> */}
              {/* <Grid item>
                <Link href="#" variant="body2">
                  {"Don't have an account? Sign Up"}
                </Link>
              </Grid> */}
            </Grid>
          </Box>
        </Box>
        {/* <Copyright sx={{ mt: 8, mb: 4 }} /> */}
      </Container>
    </ThemeProvider>
  );
}

export default SignIn;