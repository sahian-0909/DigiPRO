import { Col, Container, Row, Card, CardHeader, CardBody, Button } from "reactstrap"
import TablaMateria from './TablaMateria';
import { useState, useEffect } from "react";
import ModalMateria from "./ModalMateria";

const App = () => {

    const [materias, setMaterias] = useState([])
    const [mostrarModal, setMostrarModal] = useState(false);
    const [editar, setEditar] = useState(null)

    const mostrarMaterias = async () => {

        const response = await fetch("api/materia/Lista");
        if (response.ok) {
            const data = await response.json();
            setMaterias(data)
        } else {
            console.log("error al cargar la lista")
        }
    }

    useEffect(() => {
        mostrarMaterias()
    }, [])

    const guardarMateria = async (materia) => {

        const response = await fetch("api/materia/Agregar", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(materia)
        })

        if (response.ok) {
            setMostrarModal(!mostrarModal);
            mostrarMaterias();
        }
    }

    const editarMateria = async (materia) => {

        const response = await fetch("api/materia/Actualizar", {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(materia)
        })

        if (response.ok) {
            setMostrarModal(!mostrarModal);
            mostrarMaterias();
        }
    }

    const eliminarMateria = async (id) => {

        var respuesta = window.confirm("Desea eliminar la materia?")

        if (!respuesta) {
            return;
        }


        const response = await fetch("api/materia/Eliminar/" + id, {
            method: 'DELETE',
        })

        if (response.ok) {
            mostrarMaterias();
        }
    }

    return (
        <Container>
            <Row className="mt-5">
                <Col sm="12">
                    <Card>
                        <CardHeader>
                            <h5>Lista de Materias</h5>
                        </CardHeader>
                        <CardBody>
                            <Button size="sm" color="success"
                                onClick={() => setMostrarModal(!mostrarModal)}
                            >Agregar Materia</Button>
                            <hr></hr>
                            <TablaMateria data={materias}
                                setEditar={setEditar}
                                mostrarModal={mostrarModal}
                                setMostrarModal={setMostrarModal}
                                eliminarMateria={eliminarMateria}
                            />
                        </CardBody>
                    </Card>
                </Col>
            </Row>

            <ModalMateria
                mostrarModal={mostrarModal}

                setMostrarModal={setMostrarModal}
                guardarMateria={guardarMateria}


                editar={editar}
                setEditar={setEditar}
                editarMateria={editarMateria}
            />
        </Container>
    )
}

export default App;