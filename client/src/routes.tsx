import React from 'react'
import { BrowserRouter, Routes } from 'react-router-dom'
import Header from './pages/Header'

const RoutesComponent: React.FC = () => {
  return (
    <BrowserRouter>
      <React.Suspense>
        <Header />
        <Routes>
          {/* <Route path='/' element={Object.keys(user).length > 0 ? <Home /> : <Navigate to='/login' />} />
          <Route path='/login' element={<Index />} />
          <Route path='/register' element={<Index />} />
          <Route path='*' element={<ErrorPage />} /> */}
        </Routes>
      </React.Suspense>
    </BrowserRouter>
  )
}

export default RoutesComponent
