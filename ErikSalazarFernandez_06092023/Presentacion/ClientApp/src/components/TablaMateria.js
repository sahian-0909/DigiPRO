import { Table, Button } from "reactstrap"

const TablaMateria = ({ data, setEditar, mostrarModal, setMostrarModal, eliminarMateria }) => {

    const enviarDatos = (materia) => {
        setEditar(materia)
        setMostrarModal(!mostrarModal)

    }
    return (
        <Table striped responsive>
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Nombre</th>
                    <th>Costo</th>
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
                            <tr key={item.idMateria}>
                                <td>{item.idMateria}</td>
                                <td>{item.nombre}</td>
                                <td>{item.costo}</td>
                                <td>
                                    <Button color="primary" size="sm" className="me-2"
                                        onClick={() => enviarDatos(item)}
                                    >Editar</Button>
                                    <Button color="danger" size="sm"
                                        onClick={() => eliminarMateria(item.idMateria)}
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

export default TablaMateria;