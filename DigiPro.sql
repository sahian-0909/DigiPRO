CREATE DATABASE ControlEscolar;
GO

USE ControlEscolar;
GO

CREATE TABLE alumnos (
    idAlumno INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50),
    apellidoPaterno NVARCHAR(50),
    apellidoMaterno NVARCHAR(50)
);
GO

CREATE TABLE materias (
    idMateria INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50),
    costo DECIMAL(10, 2)
);
GO

CREATE TABLE alumnos_materias (
    idAlumno INT,
    idMateria INT,
    FOREIGN KEY (idAlumno) REFERENCES alumnos(idAlumno),
    FOREIGN KEY (idMateria) REFERENCES materias(idMateria)
);
GO
--*****************************    ALUMNOS
--Agregar Alumno
CREATE OR ALTER PROCEDURE sp_RegistrarAlumno(
@Nombre varchar(50),
@ApellidoPaterno varchar(50),
@ApellidoMaterno varchar(50),
@Registrado bit output,
@Mensaje varchar(50) output
)
AS
BEGIN
	IF(NOT EXISTS(SELECT * FROM alumnos WHERE nombre = @Nombre))
	BEGIN
		INSERT INTO alumnos (nombre, apellidoPaterno, apellidoMaterno) VALUES (@Nombre, @ApellidoPaterno, @ApellidoMaterno)

		SET @Registrado = 1
		SET @Mensaje = 'Alumno registrado'
	END
	ELSE
	BEGIN
		SET @Registrado = 0
		SET @Mensaje = 'El alumno ya existe'
	END
END

DECLARE @registrado bit, @mensaje varchar(50)

EXEC sp_RegistrarAlumno 'Karen', 'Perez', 'Torres', @registrado output, @mensaje output
SELECT @registrado
SELECT @mensaje

--Validacion del alumno
CREATE OR ALTER PROCEDURE sp_ValidarAlumno(
@Nombre varchar(50),
@ApellidoPaterno varchar(50)
)
AS
BEGIN
	IF(EXISTS(SELECT * FROM alumnos WHERE nombre = @Nombre AND apellidoPaterno = @ApellidoPaterno))
	BEGIN
		SELECT idAlumno FROM alumnos WHERE nombre = @Nombre AND apellidoPaterno = @ApellidoPaterno
	END
	ELSE
	BEGIN
		SELECT '0'
	END
END

EXEC sp_ValidarAlumno 'Erik', 'Salazar'

--Consulta de un alumno o todos
create or alter procedure consultarAlumnos
  @id int
 as
      select idAlumno, nombre, apellidoPaterno, apellidoMaterno
		from alumnos
			where idAlumno=@id OR @id<0

exec consultarAlumnos @id=-1;

--actualizar alumnos
CREATE OR ALTER PROCEDURE sp_ActualizarAlumnos(
@Id int,
@Nombre varchar(50),
@ApellidoPaterno varchar(50),
@ApellidoMaterno varchar(50),
@Actualizado bit output,
@Mensaje varchar(50) output
)
AS
BEGIN
	IF(NOT EXISTS(SELECT * FROM alumnos WHERE nombre = @Nombre))
	BEGIN
       UPDATE ALUMNOS SET
       nombre = @Nombre,
	   apellidoPaterno = @ApellidoPaterno,
	   apellidoMaterno = @ApellidoMaterno
       WHERE idAlumno = @Id

	   SET @Actualizado = 1
	   SET @Mensaje = 'Alumno actualizado'
	END
	ELSE
	BEGIN
		SET @Actualizado = 0
		SET @Mensaje = 'El alumno ya existe'
	END
END

DECLARE @actualizado bit, @mensaje varchar(50)

EXEC sp_ActualizarAlumnos @Id = 1, @Nombre = 'Angel', @ApellidoPaterno = 'Vazquez', @ApellidoMaterno = 'Leon', @Actualizado = @actualizado OUTPUT, @Mensaje = @mensaje OUTPUT

SELECT @actualizado
SELECT @mensaje

--Eliminar alumno
CREATE OR ALTER PROCEDURE sp_EliminarAlumno
@Id int
AS
BEGIN
    DELETE FROM Alumnos
    WHERE idAlumno = @Id
END

EXEC sp_EliminarAlumno @Id = 3
--*****************************************     MATERIAS
--Consulta de una o varias materias
create or alter procedure consultarMaterias
  @id int
 as
      select idMateria, nombre, costo
		from materias
			where idMateria=@id OR @id<0

exec consultarMaterias @id = 1

--Agregar Materia
CREATE OR ALTER PROCEDURE sp_AgregarMateria(
@Nombre varchar(50),
@Costo decimal(10,2)
)
AS
BEGIN
		INSERT INTO materias(nombre, costo) VALUES (@Nombre, @Costo)
END

EXEC sp_AgregarMateria 'Matematicas', 190.50


--Actualizar Materia

CREATE OR ALTER PROCEDURE sp_ActualizarMateria(
@Id int,
@Nombre varchar(50),
@Costo decimal(10,2)
)
AS
BEGIN
		UPDATE materias SET
       nombre = @Nombre,
	   costo = @Costo
       WHERE idMateria = @Id
END

EXEC sp_ActualizarMateria 1,'Matematicas', 190.50

--eliminar materia
CREATE OR ALTER PROCEDURE sp_EliminarMateria
@Id int
AS
BEGIN
    DELETE FROM materias
    WHERE idMateria = @Id
END

EXEC sp_EliminarMateria @Id = 3
USE ControlEscolar
SELECT * FROM alumnos

----

CREATE OR ALTER PROCEDURE sp_AgregarMateriaAlumno
(
    @idAlumno INT,
    @nombreMateria NVARCHAR(255),
    @costo DECIMAL(10, 2)
)
AS
BEGIN
    -- Eliminar caracteres específicos del nombre de la materia
    SET @nombreMateria = REPLACE(@nombreMateria, 'caracter_a_eliminar_1', '');
    SET @nombreMateria = REPLACE(@nombreMateria, 'caracter_a_eliminar_2', '');

    -- Insertar la materia en la tabla de materias
    INSERT INTO materias (nombre, costo)
    VALUES (@nombreMateria, @costo);

    -- Obtener el ID de la materia recién insertada
    DECLARE @idMateria INT;
    SET @idMateria = SCOPE_IDENTITY();

    -- Insertar la relación entre el alumno y la materia
    INSERT INTO alumnos_materias (idAlumno, idMateria)
    VALUES (@idAlumno, @idMateria);
END
USE ControlEscolar
SELECT * FROM alumnos_materias

CREATE OR ALTER PROCEDURE misMaterias
    @IdAlumno INT
AS
BEGIN
    SELECT a.nombre, a.apellidoPaterno, a.apellidoMaterno, m.nombre, m.costo
FROM alumnos_materias am
    INNER JOIN alumnos a ON a.idAlumno = am.idAlumno
    INNER JOIN materias m ON m.idMateria = am.idMateria
WHERE a.idAlumno = @IdAlumno
ORDER BY m.costo DESC;
END

EXEC misMaterias @IdAlumno = 4


SELECT a.nombre, a.apellidoPaterno, a.apellidoMaterno, m.nombre, m.costo
FROM alumnos_materias am
    INNER JOIN alumnos a ON a.idAlumno = am.idAlumno
    INNER JOIN materias m ON m.idMateria = am.idMateria
WHERE a.idAlumno = 1
ORDER BY m.costo DESC;

CREATE or ALTER PROCEDURE SumarCostoMaterias
    @IdAlumno INT,
    @SumaCosto DECIMAL(10, 2) OUTPUT
AS
BEGIN
    SELECT @SumaCosto = SUM(m.costo)
    FROM alumnos_materias am
    JOIN materias m ON am.idMateria = m.idMateria
    WHERE am.idAlumno = @IdAlumno
END

DECLARE @sumacosto DECIMAL(10,2)
EXECUTE SumarCostoMaterias @IdAlumno = 1, @SumaCosto = @sumacosto OUTPUT
PRINT @sumacosto