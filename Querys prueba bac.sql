----------------------------- BASE DE DATOS -----------------------------------------

CREATE DATABASE PruebaBAC;
use PruebaBAC;

CREATE TABLE usuarios(
usuario NVARCHAR(15) PRIMARY KEY NOT NULL,
contrasena VARBINARY(MAX) NOT NULL,
fCreacion DATETIME, 
fModificacion DATETIME);

CREATE TABLE preguntas(
idPregunta INT IDENTITY(1,1) PRIMARY KEY,
usuario NVARCHAR(15) FOREIGN KEY REFERENCES usuarios (usuario),
pregunta NVARCHAR(600),
estado CHAR(1),
fCreacion DATETIME
);

CREATE TABLE respuestas(
idRespuesta INT IDENTITY(1,1) PRIMARY KEY,
usuario NVARCHAR(15) FOREIGN KEY REFERENCES usuarios (usuario),
idPregunta INT FOREIGN KEY REFERENCES preguntas (idPregunta),
respuesta NVARCHAR(600),
fCreacion DATETIME
);

-----------------------------------USUARIOS----------------------------------------------------
CREATE PROC CrearUsuario @usuario NVARCHAR(15), @contrasena NVARCHAR(50), @r BIT OUTPUT
AS BEGIN
DECLARE @c INT
SELECT @c = COUNT(*) FROM usuarios WHERE usuario = @usuario;

IF (@c = 0)
BEGIN
	INSERT INTO usuarios VALUES (@usuario, ENCRYPTBYPASSPHRASE('pruebaBAC072024', @contrasena), GETDATE(),null);
	SET @r = 1;
END
ELSE
	SET @r = 0;
END
GO


DECLARE @e BIT
EXEC CrearUsuario 'danielafp1', 'prueba123', @e out
SELECT @e E

SELECT * FROM usuarios



CREATE PROC ComprobarCredenciales @usuario NVARCHAR(15), @contrasena NVARCHAR(50), @r BIT OUTPUT
AS BEGIN
DECLARE @contrasenaBDEncriptada VARBINARY(MAX)
DECLARE @contrasenaBDDesencriptada NVARCHAR(50)

SELECT @contrasenaBDEncriptada = contrasena FROM usuarios WHERE usuario = @usuario
SET @contrasenaBDDesencriptada = DECRYPTBYPASSPHRASE('pruebaBAC072024',@contrasenaBDEncriptada)

SELECT @r= IIF(@contrasenaBDDesencriptada = @contrasena,1,0)
END


DECLARE @r BIT 
EXEC ComprobarCredenciales 'danielafp', 'prueba123',@r out
SELECT @r AS R


----------------------------------- PREGUNTAS ----------------------------------------------------
CREATE PROC GuardarPregunta @usuario NVARCHAR(15), @pregunta NVARCHAR(600)
AS BEGIN
INSERT INTO preguntas VALUES (@usuario, @pregunta, 'A', GETDATE())
END

EXEC GuardarPregunta 'danielafp','�Que color es el cielo2?'

SELECT * from preguntas


CREATE PROC CerrarPregunta @idPregunta INT
AS BEGIN
UPDATE preguntas SET estado = 'C' WHERE idPregunta = @idPregunta
END

EXEC CerrarPregunta 2


CREATE PROC ObtenerPreguntas 
AS BEGIN
SELECT * FROM preguntas ORDER BY fCreacion DESC
END

EXEC ObtenerPreguntas


--------------------------- RESPUESTAS -------------------------------------------------------

CREATE PROC GuardarRespuesta @usuario NVARCHAR(15), @idPregunta INT, @respuesta NVARCHAR(600)
AS BEGIN
INSERT INTO respuestas VALUES (@usuario, @idPregunta, @respuesta, GETDATE())
END

EXEC GuardarRespuesta 'danielafp2', 1, 'Azul'

SELECT * FROM respuestas



SELECT p.*, r.respuesta from preguntas p 
INNER JOIN respuestas r on p.idPregunta = r.idRespuesta



CREATE PROC ObtenerRespuestas @idPregunta INT
AS BEGIN
SELECT * FROM respuestas WHERE idPregunta = @idPregunta
END

EXEC ObtenerRespuestas 1


