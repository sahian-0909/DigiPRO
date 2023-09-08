import { Table, Button } from "reactstrap"

const TablaAlumno = ({ data, setEditar, mostrarModal, setMostrarModal, eliminarAlumno }) => {

    const enviarDatos = (alumno) => {
        setEditar(alumno)
        setMostrarModal(!mostrarModal)

    }
    return (
        <Table striped responsive>
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Nombre</th>
                    <th>Apellido Paterno</th>
                    <th>Apellido Materno</th>
                </tr>
            </thead>
            <tbody>
                {
                    (data.length < 1) ? (
                        <tr>
                            <td colSpan="4">Sin registros</td>
                        </tr>
                    ) : (
                        data.map((item) => (
                            <tr key={item.idAlumno}>
                                <td>{item.idAlumno}</td>
                                <td>{item.nombre}</td>
                                <td>{item.apellidoPaterno}</td>
                                <td>{item.apellidoMaterno}</td>
                                <td>
                                    <Button color="primary" size="sm" className="me-2"
                                        onClick={() => enviarDatos(item)}
                                    >Editar</Button>
                                    <Button color="danger" size="sm"
                                        onClick={() => eliminarAlumno(item.idAlumno)}
                                    >Eliminar</Button>
                                </td>
                            </tr>
                        ))
                    )
                }
            </tbody>
        </Table>
    )
}

export default TablaAlumno;