import { useState, useEffect } from 'react'
import * as signalR from '@microsoft/signalr'
interface StateData {
  walletId: number
  amount: number
  accountTypeId: number
  actionTypeId: number
  sourceWalletId: number
  destinationWalletId: number
  fee: number
  CreatedAt: string
}
const SignalRComponent = () => {
  const [message, setMessage] = useState<StateData | null>(null)

  useEffect(() => {
    const connection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7016/hub/Wallet') // Replace with your SignalR hub URL
      .configureLogging(signalR.LogLevel.Information)
      .build()

    const startConnection = () => {
      connection
        .start()
        .then(() => {
          console.log('SignalR connected.')
        })
        .catch((error) => {
          console.error('Error connecting to SignalR:', error)
          // Retry connection after a delay (e.g., 5 seconds)
          if (error) {
            setTimeout(startConnection, 5000)
          }
        })
    }

    connection.onclose((error) => {
      console.log('SignalR connection closed. Attempting to reconnect...', error)
      // Retry connection after a delay (e.g., 5 seconds)
      if (error) {
        setTimeout(startConnection, 5000)
      }
    })

    startConnection()

    // Handle incoming messages or events
    connection.on('ReceiveData', (data) => {
      console.log('Received data from SignalR:', data)
      if (data) {
        setMessage(data)
      }
    })

    return () => {
      connection.stop()
    }
  }, [])

  return message
}

export default SignalRComponent
