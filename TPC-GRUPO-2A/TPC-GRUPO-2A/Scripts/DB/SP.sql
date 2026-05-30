
--LISTAR CLIENTES
USE [CALLCENTERDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_ListarClientes]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        id,
        nombre,
        email,
        telefono,
        identificacion,
        activo,
        fecha_alta
    FROM Clientes;
END

--LISTAR USUARIOS
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_ListarUsuarios]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        nombre,
        email,
        login,
        hash_password,
        rol_id,
        activo,
        fecha_creacion
    FROM Usuarios;
END
