import React, { useState } from 'react';
import Axios from 'axios';
import { useNavigate } from 'react-router-dom';

function Login({ setLoggedIn }) {
    const navigate = useNavigate();

    const [formData, setFormData] = useState({
        nombre: '',
        apellidoPaterno: '',
    });

    const [error, setError] = useState('');
    const [loginFailed, setLoginFailed] = useState(false);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await Axios.post('/api/alumno/validar', formData);
            if (response.data.mensaje === "Alumno encontrado correctamente") {
                // El inicio de sesión fue exitoso, establece el estado de inicio de sesión en verdadero
                setLoggedIn(true);
                // Redirige al usuario a la vista de alumnos
                navigate('/alumnos');
            } else {
                // El inicio de sesión falló, muestra un mensaje de error
                setError('No se pudo encontrar el alumno');
                setLoginFailed(true);
            }
        } catch (error) {
            // Manejo de errores, por ejemplo, mostrar un mensaje de error
            console.error('Error al enviar la solicitud:', error);
            setError('Error en la solicitud');
            setLoginFailed(true);
        }
    };

    return (
        <div>
            <h2>Iniciar Sesión</h2>
            {loginFailed && <div style={{ color: 'red' }}>{error}</div>}
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Nombre:</label>
                    <input
                        type="text"
                        name="nombre"
                        value={formData.nombre}
                        onChange={handleChange}
                    />
                </div>
                <div>
                    <label>Apellido Paterno:</label>
                    <input
                        type="text"
                        name="apellidoPaterno"
                        value={formData.apellidoPaterno}
                        onChange={handleChange}
                    />
                </div>
                <button type="submit">Iniciar Sesión</button>
            </form>
        </div>
    );
}

export default Login;
