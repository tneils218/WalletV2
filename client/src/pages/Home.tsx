import { Autocomplete, Button, Dialog, DialogActions, DialogContent, DialogTitle, TextField } from '@mui/material'
import { useFormik } from 'formik'
import React, { SyntheticEvent } from 'react'
import { transferWallet, addWallet, withdrawWallet } from '~/api/base'
import InputTextField from './InputTextField'

const options = [
  { id: 1, label: 'Nạp tiền' },
  { id: 2, label: 'Chuyển tiền' },
  { id: 4, label: 'Rút tiền' }
]

const Home = () => {
  const [open, setOpen] = React.useState(false)
  // const [value, setValue] = React.useState<{ id: number; label: string } | null>(options[0])

  const handleClose = () => {
    setOpen(false)
  }
  const handleOpen = () => {
    setOpen(true)
  }
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  const handleSubmit = (values: any) => {
    switch (values.actionTypeId) {
      case 1:
        addWallet(
          {
            id: parseInt(values.id),
            walletId: parseInt(values.walletId),
            actionTypeId: parseInt(values.actionTypeId),
            amount: parseInt(values.amount)
          },
          `/add`
        )
        break
      case 2:
        transferWallet(
          {
            receiverId: parseInt(values.receiverId),
            receiverWalletId: parseInt(values.receiverWalletId),
            actionTypeId: parseInt(values.actionTypeId),
            amount: parseInt(values.amount)
          },
          `/${values.id}?WalletId=${values.walletId}`
        )
        break
      default:
        withdrawWallet(
          {
            id: parseInt(values.id),
            walletId: parseInt(values.walletId),
            actionTypeId: parseInt(values.actionTypeId),
            amount: parseInt(values.amount)
          },
          `/add`
        )
        break
    }
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
      actionTypeId: options[0].id,
      amount: null
    },
    validationSchema: null,
    onSubmit: (values) => {
      handleSubmit(values)
    }
  })

  return (
    <>
      {console.log(formik.values)}
      <Button variant='text' onClick={handleOpen}>
        Action Wallet
      </Button>
      <Dialog open={open} onClose={handleClose}>
        <DialogTitle>Action money</DialogTitle>
        <DialogContent>
          <Autocomplete
            value={(formik.values.actionTypeId ??= 1)}
            onChange={(_: SyntheticEvent, newValue: number | null) => formik.setFieldValue('actionTypeId', newValue)}
            options={options?.map((p) => p.id) ?? []}
            getOptionLabel={(option: number) => {
              return options.find((p) => p.id === option)?.label ?? ''
            }}
            sx={{ mt: 1, mb: 2 }}
            renderInput={(params) => <TextField {...params} placeholder='Action money' fullWidth />}
          />
          {formik.values.actionTypeId !== null ? (
            <>
              <InputTextField
                sx={{ mb: 2 }}
                fullWidth
                id='id'
                name='id'
                formik={formik}
                field='id'
                label='id'
                placeholder='id'
              />
              <InputTextField
                sx={{ mb: 2 }}
                fullWidth
                id='walletId'
                formik={formik}
                field='walletId'
                name='walletId'
                label='walletId'
                placeholder='walletId'
              />
              {formik.values.actionTypeId !== 1 && formik.values.actionTypeId !== 4 ? (
                <>
                  <InputTextField
                    sx={{ mb: 2 }}
                    fullWidth
                    id='receiverId'
                    formik={formik}
                    field='receiverId'
                    name='receiverId'
                    label='receiverId'
                    placeholder='receiverId'
                  />
                  <InputTextField
                    sx={{ mb: 2 }}
                    fullWidth
                    id='receiverWalletId'
                    field='receiverWalletId'
                    name='receiverWalletId'
                    label='receiverWalletId'
                    formik={formik}
                    placeholder='receiverWalletId'
                  />
                </>
              ) : null}

              <InputTextField
                fullWidth
                id='amount'
                name='amount'
                field='amount'
                label='amount'
                placeholder='amount'
                formik={formik}
              />
            </>
          ) : null}
        </DialogContent>
        <DialogActions>
          <Button
            onClick={() => {
              handleClose()
              formik.resetForm()
            }}
          >
            Cancel
          </Button>
          <Button
            onClick={() => {
              formik.handleSubmit()
              handleClose()
            }}
          >
            Save
          </Button>
        </DialogActions>
      </Dialog>
    </>
  )
}

export default Home
