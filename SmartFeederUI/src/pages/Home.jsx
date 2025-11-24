import { useEffect, useState } from "react";
import "./Home.css";
import dogIcon from "../assets/images/huella.png";  
import addIcon from "../assets/images/agregar.png";
import { Link } from "react-router-dom";

const API = "http://localhost:8001/api";

export default function Home() {
  const [mascotas, setMascotas] = useState([]);

  useEffect(() => {
    fetch(`${API}/mascotas`)
      .then(res => res.json())
      .then(data => setMascotas(data));
  }, []);

  return (
    <div className="home">
      <h1>Mis Mascotas</h1>

      <div className="mascota-grid">
        {mascotas.map(m => (
            <Link
            to={`/mascota/${m.id}`}
            key={m.id}
            className="mascota-card-link"
            >
            <div className="mascota-card">
                <img src={dogIcon} alt="Mascota" className="mascota-icon" />
                <h3>{m.nombre}</h3>
                <p className="especie">{m.especie}</p>
                <p className="edad">{m.edad} años</p>
            </div>
            </Link>
        ))}
        <div className="mascota-card agregar">
        <Link to="/agregar" className="add-link">
            <img src={addIcon} alt="Agregar" className="mascota-icon add" />
            <h3>Agregar Mascota</h3>
        </Link>
        </div>
      </div>
    </div>
  );
}
