/* eslint-disable @typescript-eslint/no-explicit-any */
import * as React from 'react'
import AppBar from '@mui/material/AppBar'
import Box from '@mui/material/Box'
import Toolbar from '@mui/material/Toolbar'
import IconButton from '@mui/material/IconButton'
import NotificationsIcon from '@mui/icons-material/Notifications'
import { Badge, Popover, Typography } from '@mui/material'
import SignalRComponent from '~/SignalRComponent'
import SnackbarNotification from './SnackbarNotification'
import { fetchWallet } from '~/api/base'

export default function Header() {
  const [anchorEl, setAnchorEl] = React.useState<HTMLButtonElement | null>(null)
  const [data, setData] = React.useState([])
  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget)
  }

  const fetchWalletApi = async () => {
    let res = await fetchWallet('')
    if (res.status === 200) {
      setData(res.data)
    }
  }

  React.useEffect(() => {
    fetchWalletApi()
  }, [])

  const handleClose = () => {
    setAnchorEl(null)
  }

  function notificationsLabel(count: number) {
    if (count === 0) {
      return 'no notifications'
    }
    if (count > 99) {
      return 'more than 99 notifications'
    }
    return `${count} notifications`
  }
  const { message, openSnackbar, setOpenSnackbar } = SignalRComponent()

  const open = Boolean(anchorEl)
  const id = open ? 'simple-popover' : undefined

  const sender: any = data.filter((el: any) => el.id === message?.walletId)[0]
  const receiver: any = data.filter((el: any) => el.id === message?.destinationWalletId)[0]

  const messageSnackbar = () => {
    let msg = ''
    switch (message?.actionTypeId) {
      case 1:
        msg = `${sender?.account?.fullName} đã nạp ${message.amount} vào tài khoản`
        return msg
      case 4:
        msg = `${sender?.account?.fullName} đã rút ${message.amount} khỏi tài khoản`
        return msg
      case 2:
        msg = `${sender?.account?.fullName} đã chuyển cho ${receiver?.account.fullName} ${message?.amount} với mức phí là ${message?.fee}`
        return msg
      default:
        msg = 'lỗi'
        return msg
    }
  }
  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position='static'>
        <Toolbar sx={{ justifyContent: 'space-between' }}>
          <Typography variant='h6' component='div'></Typography>
          <IconButton aria-label={notificationsLabel(100)} onClick={handleClick}>
            <Badge badgeContent={100} color='secondary'>
              <NotificationsIcon />
            </Badge>
          </IconButton>
          <Popover
            id={id}
            open={open}
            anchorEl={anchorEl}
            onClose={handleClose}
            anchorOrigin={{
              vertical: 'bottom',
              horizontal: 'left'
            }}
            transformOrigin={{
              vertical: 'top',
              horizontal: 'center'
            }}
          ></Popover>
          <SnackbarNotification
            open={openSnackbar}
            setOpen={() => setOpenSnackbar(false)}
            code={200}
            message={message !== null ? messageSnackbar() : ''}
          />
        </Toolbar>
      </AppBar>
    </Box>
  )
}
