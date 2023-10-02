import { Autocomplete, Button, Dialog, DialogActions, DialogContent, DialogTitle, TextField } from '@mui/material'
import { useFormik } from 'formik'
import React from 'react'
import { testApi } from '~/api/base'

const options = ['Option 1', 'Option 2']

const Home = () => {
  const [open, setOpen] = React.useState(false)
  const [value, setValue] = React.useState<string | null>(options[0])
  const [inputValue, setInputValue] = React.useState('')
  React.useEffect(() => {
    testApi(null)
  }, [])
  const handleClose = () => {
    setOpen(false)
  }
  const handleOpen = () => {
    setOpen(true)
  }
  const handleSubmit = () => {
    console.log('a')
  }

  // const validationSchema = yup.object({
  //   email: yup.string().email('Enter a valid email').required('Email is required'),
  //   password: yup.string().min(8, 'Password should be of minimum 8 characters length').required('Password is required')
  // })
  const formik = useFormik({
    initialValues: {
      id: null,
      walletId: null,
      receiverId: null,
      receiverWalletId: null,
      actionTypeId: null,
      amount: null
    },
    validationSchema: {},
    onSubmit: (values) => {
      alert(JSON.stringify(values, null, 2))
    }
  })
  return (
    <>
      <Button variant='text' onClick={handleOpen}>
        Action Wallet
      </Button>
      <Dialog open={open} onClose={handleClose}>
        <DialogTitle>Subscribe</DialogTitle>
        <DialogContent>
          <Autocomplete
            value={value}
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            onChange={(event: any, newValue: string | null) => {
              setValue(newValue)
            }}
            inputValue={inputValue}
            onInputChange={(event, newInputValue) => {
              setInputValue(newInputValue)
            }}
            id='controllable-states-demo'
            options={options}
            sx={{ width: 300, height: 150, mt: 1 }}
            renderInput={(params) => <TextField {...params} label='Controllable' />}
          />
          {formik.values.actionTypeId === 1 ? (
            <>
              <TextField
                sx={{ mb: 2 }}
                fullWidth
                id='id'
                name='id'
                label='id'
                value={formik.values.id}
                onChange={formik.handleChange}
                onBlur={formik.handleBlur}
                error={formik.touched.id && Boolean(formik.errors.id)}
                helperText={formik.touched.id && formik.errors.id}
              />
              <TextField
                sx={{ mb: 2 }}
                fullWidth
                id='walletId'
                name='walletId'
                label='walletId'
                value={formik.values.walletId}
                onChange={formik.handleChange}
                onBlur={formik.handleBlur}
                error={formik.touched.walletId && Boolean(formik.errors.walletId)}
                helperText={formik.touched.walletId && formik.errors.walletId}
              />
              <TextField
                sx={{ mb: 2 }}
                fullWidth
                id='receiverId'
                name='receiverId'
                label='receiverId'
                value={formik.values.receiverId}
                onChange={formik.handleChange}
                onBlur={formik.handleBlur}
                error={formik.touched.receiverId && Boolean(formik.errors.receiverId)}
                helperText={formik.touched.receiverId && formik.errors.receiverId}
              />
              <TextField
                sx={{ mb: 2 }}
                fullWidth
                id='receiverWalletId'
                name='receiverWalletId'
                label='receiverWalletId'
                value={formik.values.receiverWalletId}
                onChange={formik.handleChange}
                onBlur={formik.handleBlur}
                error={formik.touched.receiverWalletId && Boolean(formik.errors.receiverWalletId)}
                helperText={formik.touched.receiverWalletId && formik.errors.receiverWalletId}
              />
              <TextField
                sx={{ mb: 2 }}
                fullWidth
                id='actionTypeId'
                name='actionTypeId'
                label='actionTypeId'
                value={formik.values.actionTypeId}
                onChange={formik.handleChange}
                onBlur={formik.handleBlur}
                error={formik.touched.actionTypeId && Boolean(formik.errors.actionTypeId)}
                helperText={formik.touched.actionTypeId && formik.errors.actionTypeId}
              />
              <TextField
                fullWidth
                id='amount'
                name='amount'
                label='amount'
                value={formik.values.amount}
                onChange={formik.handleChange}
                onBlur={formik.handleBlur}
                error={formik.touched.amount && Boolean(formik.errors.amount)}
                helperText={formik.touched.amount && formik.errors.amount}
              />
            </>
          ) : null}
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Cancel</Button>
          <Button onClick={handleSubmit}>Subscribe</Button>
        </DialogActions>
      </Dialog>
    </>
  )
}

export default Home
