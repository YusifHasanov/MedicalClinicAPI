import jwtDecode from 'jwt-decode';
import React, { FC, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { isValidJWT } from '../../components/JWT';
import { selectIsValidToken, setAll, setIsValidToken, setRole, setUserId } from '../../redux/slices/authSlice';
import { useRefreshAccessTokenMutation } from '../../redux/slices/userSlice';
import Constant from '../../components/Constants';

interface ProtectedRouteProps {
  children: React.ReactNode;
}

const ProtectedRoute: FC<ProtectedRouteProps> = ({ children }) => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const isValidToken = useSelector(selectIsValidToken);
  const storedToken = localStorage.getItem('accessToken');
  const token = storedToken !== null && storedToken !== '' ? storedToken : Constant.DEFAULT_JWT;
  const [refreshToken, { isSuccess: isSuccessRefresh, data, isLoading }] = useRefreshAccessTokenMutation();
  const expirationTimeInMilliseconds = 1000;
  const decodedToken = jwtDecode<{ exp: number }>(token);
  const expirationDate = decodedToken.exp * expirationTimeInMilliseconds;
  const isTokenValid = isValidJWT(token);
  let isTokenExpired = Date.now() >= expirationDate;

  useEffect(() => {
    const refreshTokenAndDispatch = () => {
      refreshToken({ accessToken: token })
        .unwrap()
        .then((result) => {
          localStorage.setItem('accessToken', result.accessToken);
          dispatch(setAll({
            accessToken: result.accessToken,
            userId: result.userId,
            role: result.role,
            isValidToken: true
          }));
        })
        .catch((error) => {
          console.log("error");
          localStorage.setItem('accessToken', data?.accessToken ?? Constant.DEFAULT_JWT);
          dispatch(setIsValidToken(false)); // Set to false on error
          navigate('/login');
        });
    };
    if (isTokenValid && !isTokenExpired) {
      dispatch(setIsValidToken(true));
      refreshTokenAndDispatch();
    } else if (!isValidToken && token) {
      refreshTokenAndDispatch();
    }
  }, [dispatch, isValidToken, refreshToken, token, navigate]);

  // useEffect(() => {

  //   if (isTokenValid && !isTokenExpired) {
  //     dispatch(setIsValidToken(true));
  //     refreshToken({ accessToken: token })
  //     .unwrap()
  //     .then((result) => {
  //       localStorage.setItem('accessToken', result.accessToken);
  //       dispatch(setAll({
  //         accessToken: result.accessToken,
  //         userId: result.userId,
  //         role: result.role,
  //         isValidToken: true
  //       }));
  //     })
  //     .catch((error) => {
  //       localStorage.setItem('accessToken', data?.accessToken ?? Constant.DEFAULT_JWT);
  //       isTokenExpired = false;
  //       dispatch(setIsValidToken(true));
  //     });
  //     return;
  //   }

  //   if (!isValidToken && token) {

  //     refreshToken({ accessToken: token })
  //       .unwrap()
  //       .then((result) => {
  //         localStorage.setItem('accessToken', result.accessToken);
  //         dispatch(setAll({
  //           accessToken: result.accessToken,
  //           userId: result.userId,
  //           role: result.role,
  //           isValidToken: true
  //         }));
  //       })
  //       .catch((error) => {
  //         localStorage.setItem('accessToken', data?.accessToken ?? Constant.DEFAULT_JWT);
  //         isTokenExpired = false;
  //         dispatch(setIsValidToken(true));
  //       });

  //     return;
  //   }
  //   navigate('/login');

  // }, [dispatch, isValidToken, refreshToken, token, navigate]);

  if (isLoading && !isSuccessRefresh && !isValidToken) {
    return <div>Loading...</div>;
  }

  return <>{children}</>;
};

export default ProtectedRoute;