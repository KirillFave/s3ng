import { createSlice } from '@reduxjs/toolkit'
import { removeAccessTokenToLocalStorage, setAccessTokenToLocalStorage } from '../../helpers/localstorage.helper'


/**
 * @typedef {Object} UserState
 * @property {UserData | null} userData
 * @property {boolean} isAuth
 */

/** @type {UserState} */
const initialState = {
    userData: null,
    isAuth: false
}

export const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    loginAction: (state, action) => {
        state.userData = action.payload
        state.isAuth = true
    },
    setAccessToken: (state, action) => {
      setAccessTokenToLocalStorage(action.payload.accessToken)
    },
    logoutAction: (state) => {
        state.isAuth = false
        state.userData = null
        removeAccessTokenToLocalStorage()
    }
  },
})

export const { loginAction, logoutAction, setAccessToken } = userSlice.actions
export default userSlice.reducer