import React from 'react'
import { Link } from 'react-router-dom'

const ErrorPage = () => {
  return (
    <div className='min-h-screen flex items-center justify-center gap-10 bg-slate-900 font-black'>
        PAGE NOT FOUND
        <Link to={"/"} className="rounder-md bg-sky-600 px-6 py-2 hover:bg-sky-700">
            Back
        </Link>
    </div>
  )
}

export default ErrorPage