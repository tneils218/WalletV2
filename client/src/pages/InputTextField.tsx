/* eslint-disable @typescript-eslint/no-explicit-any */
import React from 'react'
import { TextField } from '@mui/material'

const InputTextField = (props: any) => {
  const { formik, label, placeholder, field, id, autoComplete, required, autoFocus, size, onBlur, reset, sx } = props

  return (
    <TextField
      required={required}
      fullWidth
      id={id}
      type='text'
      label={label}
      size={size}
      sx={sx}
      placeholder={placeholder}
      autoComplete={autoComplete}
      autoFocus={autoFocus}
      value={formik.values[field]}
      onBlur={onBlur}
      onChange={(e) => {
        if (!reset) {
          formik.setFieldValue(field, e.target.value)
        }
      }}
      error={formik.touched[field] && Boolean(formik.errors[field])}
      helperText={formik.touched[field] && formik.errors[field]}
    />
  )
}

export default InputTextField
