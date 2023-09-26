import { useEffect } from 'react'

import { HubConnectionBuilder } from '@microsoft/signalr';
function App() {
  const startConnection = async () => {
    const connection = new HubConnectionBuilder().withUrl('https://localhost:7016/hub/Wallet').build()

    connection.on('ReceiveData', (data) => {
      // Handle the received data
      console.log('Received data:', data)
    })

    try {
      await connection.start();
      console.log('Connection to hub established');
    } catch (error) {
      console.error('Error connecting to hub:', error);
    }
  }

  useEffect(() => {
    startConnection();
  }, []);
  return <>
  
  </>
}

export default App
