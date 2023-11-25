import styled from '@emotion/styled';
import MenuIcon from '@mui/icons-material/Menu';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import Divider from '@mui/material/Divider';
import Drawer from '@mui/material/Drawer';
import IconButton from '@mui/material/IconButton';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemText from '@mui/material/ListItemText';
import Switch from '@mui/material/Switch';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import * as React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
// import { setIsValidToken } from '../../redux/slices/authSlice';
import { selectTheme, toggleTheme } from '../../redux/slices/themeSlice';
import Notifications from './Notifications';
import { Tooltip } from '@mui/material';
import { setAll, setIsValidToken } from '../../redux/slices/authSlice';
import Constant from '../../components/Constants';

const drawerWidth = 240;

export default function DrawerAppBar({ children }: { children: React.ReactNode }) {
  const isDark = useSelector(selectTheme);
  const dispatch = useDispatch();
  const MaterialUISwitch = styled(Switch)(({ theme }) => ({
    width: 60,
    height: 34,
    padding: 7,
    transition: "all 0.3s ease-in-out",
    '& .MuiSwitch-switchBase': {
      margin: 1,
      padding: 0,
      transform: 'translateX(6px)',
      '&.Mui-checked': {
        color: '#fff',
        transform: 'translateX(22px)',
        '& .MuiSwitch-thumb:before': {
          backgroundImage: `url('data:image/svg+xml;utf8,<svg xmlns="http://www.w3.org/2000/svg" height="20" width="20" viewBox="0 0 20 20"><path fill="${encodeURIComponent(
            '#fff',
          )}" d="M4.2 2.5l-.7 1.8-1.8.7 1.8.7.7 1.8.6-1.8L6.7 5l-1.9-.7-.6-1.8zm15 8.3a6.7 6.7 0 11-6.6-6.6 5.8 5.8 0 006.6 6.6z"/></svg>')`,
        },
        '& + .MuiSwitch-track': {
          opacity: 1,
          backgroundColor: isDark ? '#8796A5' : '#aab4be',
        },
      },
    },
    '& .MuiSwitch-thumb': {
      backgroundColor: isDark ? '#003892' : '#001e3c',
      width: 33,
      height: 33,
      '&:before': {
        content: "''",
        position: 'absolute',
        width: '100%',
        height: '100%',
        left: 0,
        top: 0,
        backgroundRepeat: 'no-repeat',
        backgroundPosition: 'center',
        backgroundImage: `url('data:image/svg+xml;utf8,<svg xmlns="http://www.w3.org/2000/svg" height="20" width="20" viewBox="0 0 20 20"><path fill="${encodeURIComponent(
          '#fff',
        )}" d="M9.305 1.667V3.75h1.389V1.667h-1.39zm-4.707 1.95l-.982.982L5.09 6.072l.982-.982-1.473-1.473zm10.802 0L13.927 5.09l.982.982 1.473-1.473-.982-.982zM10 5.139a4.872 4.872 0 00-4.862 4.86A4.872 4.872 0 0010 14.862 4.872 4.872 0 0014.86 10 4.872 4.872 0 0010 5.139zm0 1.389A3.462 3.462 0 0113.471 10a3.462 3.462 0 01-3.473 3.472A3.462 3.462 0 016.527 10 3.462 3.462 0 0110 6.528zM1.665 9.305v1.39h2.083v-1.39H1.666zm14.583 0v1.39h2.084v-1.39h-2.084zM5.09 13.928L3.616 15.4l.982.982 1.473-1.473-.982-.982zm9.82 0l-.982.982 1.473 1.473.982-.982-1.473-1.473zM9.305 16.25v2.083h1.389V16.25h-1.39z"/></svg>')`,
      },
    },
    '& .MuiSwitch-track': {
      opacity: 1,
      backgroundColor: isDark ? '#8796A5' : '#aab4be',
      borderRadius: 20 / 2,
    },
  }));
  const [mobileOpen, setMobileOpen] = React.useState(false);
  const navigate = useNavigate()
  const handleDrawerToggle = () => {
    setMobileOpen((prevState) => !prevState);
  };
  const toggle = () => {
    dispatch(toggleTheme())
  }

  const navItems = [

    {
      id: "fdgegfdsf",
      name: "Mualiceler",
      path: "/therapies",
      color: "primary",
      onclick: () => { navigate("/therapies") }
    },
    {
      id: "kahdgabsdbka",
      name: "Xəstələr",
      path: "/patients",
      color: "primary",
      onclick: () => { navigate("/patients") }

    },
    {
      id: "pokhjryokhj",
      name: "Ödənişlər",
      path: "/payments",
      color: "primary",
      onclick: () => { navigate("/payments") }
    },
    {
      id: "vkjefgwbsaasd",
      name: "Həkimlər",
      path: "/doctors",
      color: "primary",
      onclick: () => { navigate("/doctors") }
    },

    // {
    //   id: "vkhzsdcsdwd",
    //   name: "İstifadəçilər",
    //   path: "/users",
    //   color: "primary",
    //   onclick: () => { navigate("/users") }
    // },
    {
      id: "hfduihbdfib",
      name: "Çıxış",
      path: "/login",
      color: "error",
      onclick: () => {
        localStorage.removeItem("accessToken")
        dispatch(setIsValidToken(false))
        dispatch(setAll({
          accessToken:  Constant.DEFAULT_JWT,
          userId:null,
          role: null,
        }))
        navigate("/login")
      }
    },
  ];
  const drawer = (
    <Box onClick={handleDrawerToggle} sx={{ textAlign: 'center' }}>
      <Typography variant="h6" sx={{ my: 2 }}>
        Medical Managment System
      </Typography>
      <Divider />
      <List>
        <ListItem disablePadding sx={{ textAlign: 'center', width: "100%", justifyContent: "center" }}>
          <MaterialUISwitch onClick={toggle} sx={{ m: 1 }} defaultChecked={isDark} />
        </ListItem>
        <Notifications />
        {navItems.map((item) => (
          <ListItem onClick={() => { navigate(item.path) }} key={item.id} disablePadding>
            <ListItemButton sx={{ textAlign: 'center' }}>
              <ListItemText primary={item.name} />
            </ListItemButton>
          </ListItem>
        ))}

      </List>
    </Box>
  );

  return (
    <Box sx={{ display: 'flex' }}>
      <CssBaseline />
      <AppBar component="nav">
        <Toolbar>
          <IconButton
            color="inherit"
            aria-label="open drawer"
            edge="start"
            onClick={handleDrawerToggle}
            sx={{ mr: 2, display: { sm: 'none' } }}
          >
            <MenuIcon />
          </IconButton>
          <Typography
            variant="h6"
            component="div"
            sx={{ flexGrow: 1, display: { xs: 'none', sm: 'block' } }}
          >
            Medical Managment System
          </Typography>
          <Notifications />
          <Box sx={{ display: { xs: 'none', sm: 'block' } }}>

            <span onClick={toggle} >
              <MaterialUISwitch sx={{ m: 1 }} defaultChecked={isDark} />
            </span>
         
          
            
            {navItems.map((item) => (
              <Button onClick={item.onclick} color={item.color as any} variant={"contained"} style={{ marginRight: "5px", fontSize: "12px", }} key={item.id} sx={{ color: '#fff' }}>
                {item.name}
              </Button>
            ))}

          </Box>
        </Toolbar>
      </AppBar>
      <Box component="nav">
        <Drawer
          variant="temporary"
          open={mobileOpen}
          onClose={handleDrawerToggle}
          ModalProps={{
            keepMounted: true, // Better open performance on mobile.
          }}
          sx={{
            display: { xs: 'block', sm: 'none' },
            '& .MuiDrawer-paper': { boxSizing: 'border-box', width: drawerWidth },
          }}
        >
          {drawer}
        </Drawer>
      </Box>
      <Box style={{ width: "100%" }} component="main" sx={{ p: 3 }}>
        <Toolbar />
        <>
          {children}
        </>
      </Box>
    </Box >
  );
}
