import axios, { AxiosRequestConfig } from 'axios'
const baseUrl = 'https://localhost:7016'
const fetchApi = async (endPoint: string, method: string, params: string | null, data = null) => {
  let configs: AxiosRequestConfig = {
    url: `${baseUrl}/api/${endPoint}`,
    method: method,
    // headers: {
    //   Authorization: 'Bearer 2db3680f1c88c6d2e50c93033969a390f948de1d'
    // },
    params,
    data
  }

  let response = await axios(configs)
  return response
}
export const testApi = async (params: null) => {
  return fetchApi(`Wallet`, 'GET', null, params)
}
