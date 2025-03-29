import { instance } from "../api/axios.api";
import { ResponceLogin } from "../models/ResponceLogin";
import { ResponceProfile } from "../models/ResponceProfile";
import { ResponceUserData } from "../models/ResponceUserData";
import { UserData } from "../models/UserData";

export const AuthService = {
    /**
     * @param {UserData} userData
     * @returns {Promise<ResponceUserData>}
     */
    async registration(userData) {
        const payload = {
            email: userData.login,
            password: userData.password
        }

        const {data} = await instance.post("register", payload)
        return new ResponceUserData(
            data
        );
    },

    /**
     * @param {UserData} userData
     * @returns {Promise<ResponceLogin>}
     */
    async login(userData) {
        const payload = {
            email: userData.login,
            password: userData.password
        }

        const {data} = await instance.post("auth", payload)
        return new ResponceLogin(
            data
        );
    },

    /**
     * @returns {Promise<ResponceProfile>}
     */
    async getCurrentProfile() {
        const { data } = await instance.get("IAM/profile", { withCredentials: true })
        if (data) return data
    },

    async logout() {
        await instance.post("logout")
    },
}