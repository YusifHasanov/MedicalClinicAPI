 
import React, { FC } from 'react'
import Navbar from './Navbar'
interface Props{
    children: React.ReactNode | React.ReactNodeArray
}



const MainLayout:FC<Props> = ({children}) => {
  return (
    <div>
        <Navbar />
        {children}
    </div>
  )
}

export default MainLayout