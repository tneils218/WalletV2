/* eslint-disable @typescript-eslint/no-explicit-any */
import axios, { AxiosRequestConfig } from 'axios'
const baseUrl = 'https://localhost:7016'

const fetchApi = async (endPoint: string, method: string, params: any | null, data = null, urlConcat = '') => {
  let configs: AxiosRequestConfig = {
    url: `${baseUrl}/api/${endPoint}${urlConcat ? urlConcat : ''}`,
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
export const transferWallet = async (params: any, urlConcat: string) => {
  return fetchApi(`Wallet`, 'PUT', null, params, urlConcat)
}

export const addWallet = async (params: any, urlConcat: string) => {
  return fetchApi(`Wallet`, 'POST', null, params, urlConcat)
}
export const withdrawWallet = async (params: any, urlConcat: string) => {
  return fetchApi(`Wallet`, 'POST', null, params, urlConcat)
}
