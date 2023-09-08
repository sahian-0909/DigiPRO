import { useEffect, useState } from "react"
import { Modal, ModalBody, ModalHeader, Form, FormGroup, Input, Label, ModalFooter, Button } from "reactstrap"

const modeloMateria = {
    idMateria: 0,
    nombre: "",
    costo: 0.0,
}


const ModalMateria = ({ mostrarModal, setMostrarModal, guardarMateria, editar, setEditar, editarMateria }) => {
    const [materia, setMateria] = useState([modeloMateria])

    const actualizarDato = (e) => {

        console.log(e.target.name + " : " + e.target.value)
        setMateria(
            {
                ...materia,
                [e.target.name]: e.target.value
            }
        )
    }

    const enviarDatos = () => {

        if (materia.idMateria == 0) {
            guardarMateria(materia)
        } else {
            editarMateria(materia)
        }

        setMateria(modeloMateria)

    }

    useEffect(() => {
        if (editar != null) {
            setMateria(editar)
        } else {
            setMateria(modeloMateria)
        }
    }, [editar])

    const cerrarModal = () => {

        setMostrarModal(!mostrarModal)
        setEditar(null)
    }


    return (

        <Modal isOpen={mostrarModal}>
            <ModalHeader>

                {materia.idMateria == 0 ? "Nueva Materia" : "Editar Materia"}

            </ModalHeader>
            <ModalBody>
                <Form>
                    <FormGroup>
                        <Label>Nombre</Label>
                        <Input name="nombre" onChange={(e) => actualizarDato(e)} value={materia.nombre} />
                    </FormGroup>
                    <FormGroup>
                        <Label>Costo</Label>
                        <Input name="costo" onChange={(e) => actualizarDato(e)} value={materia.costo} />
                    </FormGroup>
                </Form>
            </ModalBody>

            <ModalFooter>
                <Button color="primary" size="sm" onClick={enviarDatos}>Guardar</Button>
                <Button color="danger" size="sm" onClick={cerrarModal} >Cerrar</Button>
            </ModalFooter>
        </Modal>

    )
}

export default ModalMateria;