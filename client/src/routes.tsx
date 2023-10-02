import React from 'react'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import Header from './pages/Header'
import Home from './pages/Home'

const RoutesComponent: React.FC = () => {
  return (
    <BrowserRouter>
      <React.Suspense>
        <Header />
        <Routes>
          <Route path='/' element={<Home />} />
          {/* <Route path='/login' element={<Index />} />
          <Route path='/register' element={<Index />} />
          <Route path='*' element={<ErrorPage />} /> */}
        </Routes>
      </React.Suspense>
    </BrowserRouter>
  )
}

export default RoutesComponent
