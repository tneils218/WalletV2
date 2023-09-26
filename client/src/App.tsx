import { useEffect, useState } from 'react'
import axios from 'axios';

function App() {
  const [data, setData] = useState(null);
  const apiUrl = 'https://localhost:7016/api/Wallet';
  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get(apiUrl);
        setData(response.data);
      } catch (error) {
          console.log(error)
      }
    };

    fetchData();
  }, []); //
  return (
    <>

   </>
  )
}

export default App
