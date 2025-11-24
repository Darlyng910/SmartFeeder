import { Link } from "react-router-dom";
import "./Navbar.css";
import logo from "../assets/images/logo.png";

export default function Navbar() {
  return (
    <nav className="navbar">
      <div className="navbar-left">
        <img src={logo} alt="logo" className="logo-icon" />
        <h2 className="logo-text">SmartFeeder</h2>
      </div>

      <ul className="nav-links">
        <li><Link to="/">Inicio</Link></li>
        <li><Link to="/horarios">Horarios</Link></li>
        <li><Link to="/historial">Historial</Link></li>
      </ul>
    </nav>
  );
}
