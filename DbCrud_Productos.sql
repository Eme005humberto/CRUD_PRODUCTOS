CREATE DATABASE CRUD_PRODUCTOS

USE CRUD_PRODUCTOS

CREATE TABLE PRODUCTOS(
IdProducto INT IDENTITY (1,1) PRIMARY KEY,
Nombre VARCHAR(100),
Precio DECIMAL(10,2),
Stock BIGINT,
IdCategoria INT,
CONSTRAINT FK_Categoria_Producto FOREIGN KEY(IdCategoria) REFERENCES CATEGORIA(IdCategoria)
)

CREATE TABLE CATEGORIA(
IdCategoria INT PRIMARY KEY IDENTITY(1,1),
Categoria VARCHAR(100)
)

CREATE PROCEDURE SP_MostrarCategorias
AS BEGIN
SELECT * FROM CATEGORIA
END

EXEC SP_MostrarCategorias

ALTER PROCEDURE SP_MostrarProductos
AS BEGIN
SELECT p.IdProducto, p.Nombre, p.Precio , p.Stock ,c.Categoria FROM PRODUCTOS AS p
LEFT JOIN CATEGORIA AS c  ON p.IdCategoria = c.IdCategoria
END

EXEC SP_MostrarProductos
--Ingresamos data a la tabla productos
INSERT INTO PRODUCTOS(Nombre,Precio,Stock,IdCategoria)VALUES('Amoxicilina 500',0.15,100,1)
--Ingresamos data a la tabla categoria
INSERT INTO CATEGORIA(Categoria)VALUES('Medicina')
INSERT INTO CATEGORIA(Categoria)VALUES('Bebidas')

--Procedimiento almacenado para agregar productos
CREATE PROCEDURE SP_InsertarProductos(
@IdProducto INT,
@Nombre VARCHAR(100),
@Precio DECIMAL(10,2),
@Stock BIGINT,
@IdCategoria INT
)AS BEGIN
SET NOCOUNT ON;
IF EXISTS (SELECT 1 FROM PRODUCTOS WHERE IdProducto = @IdProducto)
   BEGIN
      PRINT 'Ya existe este registro';
	  RETURN;
   END
ELSE
   BEGIN TRY
      INSERT INTO PRODUCTOS(Nombre,Precio,Stock,IdCategoria)
	  VALUES(@Nombre,@Precio,@Stock,@IdCategoria)

	  IF @@ROWCOUNT > 0
	     BEGIN 
		   PRINT ('Se ha ingresado correctamente!!')
		 END
	END TRY
	BEGIN CATCH
	    PRINT 'Error al ingresar el registro '+ERROR_MESSAGE();
	END CATCH
END
  
--Procedimiento almacenado para actualizar el producto
CREATE PROCEDURE SP_ActualizarProducto(
@IdProducto INT,
@Nombre VARCHAR(100),
@Precio DECIMAL(10,2),
@Stock BIGINT,
@IdCategoria INT
)AS BEGIN
IF EXISTS (SELECT 1 FROM PRODUCTOS WHERE IdProducto = @IdProducto)
   BEGIN
     UPDATE PRODUCTOS SET Nombre = @Nombre , Precio = @Precio , Stock = @Stock , IdCategoria = @IdCategoria 
	 WHERE IdProducto = @IdProducto
	 PRINT 'Se ha modificado correctamente'
   END
ELSE
    PRINT 'El registro no existe!!'
	RETURN;
END

--Procedimiento almacenado de Eliminar Producto
CREATE PROCEDURE SP_EliminarProducto(
@IdProducto INT
)AS BEGIN
   DELETE FROM PRODUCTOS WHERE IdProducto = @IdProducto
END

--Procedimiento almacenado categoria Insert 
CREATE PROCEDURE SP_InsertarCategorias(
@IdCategoria INT,
@Categoria VARCHAR(100)
)AS BEGIN
IF EXISTS (SELECT 1 FROM CATEGORIA WHERE IdCategoria = @IdCategoria)
BEGIN
    PRINT 'Ya existe este registro'
	RETURN;
END
BEGIN TRY
  INSERT INTO CATEGORIA(Categoria)VALUES(@Categoria)
  PRINT 'Se ha ingresado correctamente'

  IF @@ROWCOUNT > 0
	     BEGIN 
		   PRINT ('Se ha ingresado correctamente!!')
		 END
	END TRY
	BEGIN CATCH
	    PRINT 'Error al ingresar el registro '+ERROR_MESSAGE();
	END CATCH
END

--Procedimiento almacenado para modificar
CREATE PROCEDURE SP_ModificarCategorias(
@IdCategoria INT,
@Categoria VARCHAR(100)
)AS BEGIN
IF EXISTS (SELECT 1 FROM CATEGORIA WHERE IdCategoria = @IdCategoria)
   BEGIN
     UPDATE CATEGORIA SET Categoria = @Categoria  
	 WHERE IdCategoria = @IdCategoria
	 PRINT 'Se ha modificado correctamente'
   END
ELSE
    PRINT 'El registro no existe!!'
	RETURN;
END

CREATE PROCEDURE SP_EliminarCategoria(
@IdCategoria INT
)AS BEGIN
DELETE FROM CATEGORIA WHERE IdCategoria = @IdCategoria
END

