import axios from "axios";

import { store } from "../store/store";
import { logoutAction } from "../store/user/userSlice";
import { getTokenFromLocalStorage, setAccessTokenToLocalStorage } from "../helpers/localstorage.helper";

export const instance = axios.create(
    {
        baseURL: "/api",
        withCredentials: true
    }
)

instance.interceptors.request.use((config) => {
    const token = getTokenFromLocalStorage()
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
})

instance.interceptors.response.use((response) => response,
    async (error) => {
        const originalRequest = error.config;
        
        const token = getTokenFromLocalStorage();

        //if (error.response?.status === 401) {

        if (token && isTokenExpired(token)) {
            originalRequest._retry = true;
            try {
                const { data } = await instance.post("refresh", {})
                setAccessTokenToLocalStorage(data.accessToken)
                originalRequest.headers.Authorization = `Bearer ${data.accessToken}`
                return instance(originalRequest)
            } catch (err) {
                setTimeout(resolve, 2000)
                store.dispatch(logoutAction())
            }
        }
        return Promise.reject(error);
    }
)

function isTokenExpired(token) {
    const payload = JSON.parse(atob(token.split('.')[1]));
    const expirationDate = new Date(payload.exp * 1000)
    const now = new Date();
    return now > expirationDate;
}