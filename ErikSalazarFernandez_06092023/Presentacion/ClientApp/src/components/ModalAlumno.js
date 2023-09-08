import { useEffect, useState } from "react"
import { Modal, ModalBody, ModalHeader, Form, FormGroup, Input, Label, ModalFooter, Button } from "reactstrap"

const modeloAlumno = {
    idAlumno: 0,
    nombre: "",
    apellidoPaterno: "",
    apellidoMaterno: ""
}


const ModalAlumno = ({ mostrarModal, setMostrarModal, guardarAlumno, editar, setEditar, editarAlumno }) => {
    const [alumno, setAlumno] = useState([modeloAlumno])

    const actualizarDato = (e) => {

        console.log(e.target.name + " : " + e.target.value)
        setAlumno(
            {
                ...alumno,
                [e.target.name]: e.target.value
            }
        )
    }

    const enviarDatos = () => {

        if (alumno.idAlumno == 0) {
            guardarAlumno(alumno)
        } else {
            editarAlumno(alumno)
        }

        setAlumno(modeloAlumno)

    }

    useEffect(() => {
        if (editar != null) {
            setAlumno(editar)
        } else {
            setAlumno(modeloAlumno)
        }
    }, [editar])

    const cerrarModal = () => {

        setMostrarModal(!mostrarModal)
        setEditar(null)
    }


    return (

        <Modal isOpen={mostrarModal}>
            <ModalHeader>

                {alumno.idAlumno == 0 ? "Nuevo Alumno" : "Editar Alumno"}

            </ModalHeader>
            <ModalBody>
                <Form>
                    <FormGroup>
                        <Label>Nombre</Label>
                        <Input name="nombre" onChange={(e) => actualizarDato(e)} value={alumno.nombre} />
                    </FormGroup>
                    <FormGroup>
                        <Label>Apellido Paterno</Label>
                        <Input name="apellidoPaterno" onChange={(e) => actualizarDato(e)} value={alumno.apellidoPaterno} />
                    </FormGroup>
                    <FormGroup>
                        <Label>Apellido Materno</Label>
                        <Input name="apellidoMaterno" onChange={(e) => actualizarDato(e)} value={alumno.apellidoMaterno} />
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

export default ModalAlumno;