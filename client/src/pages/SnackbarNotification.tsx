/* eslint-disable @typescript-eslint/no-explicit-any */
import MuiAlert, { AlertProps } from '@mui/material/Alert'
import Slide, { SlideProps } from '@mui/material/Slide'
import Snackbar from '@mui/material/Snackbar'
import * as React from 'react'

type TransitionProps = Omit<SlideProps, 'direction'>

const Alert = React.forwardRef<HTMLDivElement, AlertProps>(function Alert(props, ref) {
  return <MuiAlert elevation={6} ref={ref} variant='filled' {...props} />
})

function TransitionUp(props: TransitionProps) {
  return <Slide {...props} direction='up' />
}

interface CustomizedSnackBarsProps {
  open: boolean
  setOpen: any
  message: string
  code: number
  style?: any
  icon?: any
}

export default function CustomizedSnackBars(props: CustomizedSnackBarsProps) {
  const { open, setOpen, message, code, style, icon } = props

  const handleClose = (event?: React.SyntheticEvent | Event, reason?: string) => {
    if (reason === 'clickaway') {
      return
    }
    setOpen(false)
  }

  return (
    <Snackbar
      open={open}
      autoHideDuration={5000}
      TransitionComponent={TransitionUp}
      onClose={handleClose}
      sx={{ ...style, padding: '4px', zIndex: 100000 }}
    >
      <Alert
        onClose={handleClose}
        severity={code === 200 ? 'success' : 'error'}
        sx={{
          width: '100%',
          backgroundColor: code === 200 ? '#262626' : 'white',
          color: code === 200 ? 'white' : '#dc3545'
        }}
        variant='standard'
        icon={icon}
      >
        {message}
      </Alert>
    </Snackbar>
  )
}
