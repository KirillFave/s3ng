import React, { useEffect } from 'react';
import { RouterProvider } from 'react-router-dom';
import { router } from './router/router';
import { useDispatch } from 'react-redux';
import { AuthService } from './services/auth.service';
import { loginAction, logoutAction } from './store/user/userSlice';

function App() {
  const dispatch = useDispatch()

  const checkAuth = async () => {
    const data = await AuthService.getCurrentProfile()
    if(data) {
      dispatch(loginAction(data))
    } else {
      dispatch(logoutAction())
    }
  }
  
  useEffect(() => {
    checkAuth()
  }, [])

  return <RouterProvider router={router}/>
}

export default App