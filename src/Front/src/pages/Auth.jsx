import React from 'react'
import { useState } from 'react'
import { toast } from 'react-toastify'
import { useDispatch } from 'react-redux'
import { useNavigate } from 'react-router-dom'

import { AuthService } from '../services/auth.service'
import { loginAction, setAccessToken } from "../store/user/userSlice";

const Auth = props => {
  const [login, setLogin] = useState("")
  const [password, setPassword] = useState("")
  const [isLogin, setIsLogin] = useState(false)
  const dispatch = useDispatch()
  const navigate = useNavigate()

  /**
   * @param {React.FormEvent<HTMLFormElement>}
   */
  const registrationHandler = async (event) => {
    try {
        event.preventDefault();
        const data = await AuthService.registration({login, password})
        if (data) {
            toast.success("Account has been created")
            setIsLogin(!isLogin)
        }
    } catch (/** @type {Error | any} */ error) {
        if (error instanceof Error) {
            toast.error(error.message);
        } else if (error.response && error.response.data) {
            toast.error(error.response.data.message || "Ошибка регистрации");
        } else {
            toast.error("Произошла неизвестная ошибка");
        }
    }
  }

  /**
   * @param {React.FormEvent<HTMLFormElement>}
   */
    const loginHandler = async (event) => {
        try {
            event.preventDefault();

            const data = await AuthService.login({login, password})
            if (data) {
                dispatch(loginAction({ login, password }))
                dispatch(setAccessToken(data.accessToken))
                toast.success("Login successfully")
                navigate("/")
            }
        } catch (/** @type {Error | any} */ error) {
            if (error instanceof Error) {
                toast.error(error.message);
            } else if (error.response && error.response.data) {
                toast.error(error.response.data.message || "Ошибка при логине");
            } else {
                toast.error("Произошла неизвестная ошибка");
            }
        }
    }

  return (
    <div className='mt-40 mx-50 flex flex-col items-center justify-center bg-slate-800 text-white'>
        <h1 className='mb-10 text-center text-xl'>
            {isLogin ? "Login" : "Registration"}
        </h1>

        <form
            onSubmit={isLogin ? loginHandler : registrationHandler} 
            className='mx-auto flex w-1/3 flex-col gap-5'>
            <input
                type='text' 
                className='input p-2 rounded bg-slate-700 text-white' 
                placeholder='Email' 
                onChange={(e) => setLogin(e.target.value)} 
            />
            <input 
                type='password' 
                className='input p-2 rounded bg-slate-700 text-white' 
                placeholder='Password' 
                onChange={(e) => setPassword(e.target.value)} 
            />

            <button className='btn btn-green mx-auto'>Submit</button>
        </form>

        <div className='flex items-center mt-5'>
            {
                isLogin ? (
                    <button
                    onClick={() => setIsLogin(!isLogin)}
                    className='text-slate-300 hover:text-white'>
                        You don`t have an account?
                    </button>
                ) : (
                    <button 
                    onClick={() => setIsLogin(!isLogin)}
                    className='text-slate-300 hover:text-white'>
                        Already have an account?
                    </button>
                )
            }
        </div>
    </div>
  )
}

Auth.propTypes = {}

export default Auth