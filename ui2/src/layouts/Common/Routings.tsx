import { Route, Routes } from 'react-router-dom'

import Doctors from '../Doctor/Doctors'
import NotFound from '../NotFound'
import Patients from '../Patient/Patients'
import Payments from '../Payment/Payments'
import ProtectedRoute from './ProtectedRoute'
import Therapies from '../Therapy/Therapies'
import Notifications from './Notifications'


const pathes = [
  {
    id: "iewroweurow",
    name: "Xəstələr",
    path: "/patients",
    component: <Patients />
  },
  {
    id: "sasasaasdq",
    name: "Ödənişlər",
    path: "/payments",
    component: <Payments />
  },
  {
    id: "bbncvbcvb",
    name: "Həkimlər",
    path: "/doctors",
    component: <Doctors />
  }
  ,
  {
    id: "eyreertre",
    name: "Muayoneler",
    path: "/therapies",
    component: <Therapies />
  }

]

const Routing = () =>
(

<>
<ProtectedRoute>
    <Routes>
      {
        pathes.map((item) => (
          <Route key={item.id} path={item.path} element={item.component} />
        ))
      }
      <Route path={"/"} element={<Patients />} />
      <Route path={"*" } element={<NotFound />} />
    </Routes>
  </ProtectedRoute>
</>
 
 

)


export default Routing