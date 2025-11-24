import { BrowserRouter, Routes, Route } from "react-router-dom";
import Navbar from "./components/Navbar";
import Home from "./pages/Home";
import AgregarMascota from "./pages/AgregarMascota";
import ProgramarHorario from "./pages/ProgramarHorario";
import Horarios from "./pages/Horarios"; 
import Historial from "./pages/Historial"; 

function App() {
  return (
    <BrowserRouter>
      <Navbar />

      <div style={{ padding: "25px" }}>
        <Routes>
          <Route path="/" element={<Home />} />

          <Route path="/agregar" element={<AgregarMascota />} />
          <Route path="/mascota/:id" element={<ProgramarHorario />} />

          <Route path="/horarios" element={<Horarios />} />
          <Route path="/historial" element={<Historial />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
