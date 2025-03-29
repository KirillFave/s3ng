import { Outlet } from "react-router-dom"
import Header from "../components/Header"

function Layout() {
    return <div className="min-h-screeen bg-slate-900 pb-20 font-roboro text-white">
                <Header/>
                <div className="container">
                    <Outlet />
                </div>
            </div>
}
  
export default Layout