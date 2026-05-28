USE CALLCENTERDB
GO

-- Roles
INSERT INTO ROLES (nombre, activo) VALUES ('Administrador', 1)
INSERT INTO ROLES (nombre, activo) VALUES ('Supervisor', 1)
INSERT INTO ROLES (nombre, activo) VALUES ('Telefonista', 1)

-- Estados del incidente
INSERT INTO ESTADOS_INCIDENTE (nombre, activo) VALUES ('Abierto', 1)
INSERT INTO ESTADOS_INCIDENTE (nombre, activo) VALUES ('Asignado', 1)
INSERT INTO ESTADOS_INCIDENTE (nombre, activo) VALUES ('En Analisis', 1)
INSERT INTO ESTADOS_INCIDENTE (nombre, activo) VALUES ('Resuelto', 1)
INSERT INTO ESTADOS_INCIDENTE (nombre, activo) VALUES ('Cerrado', 1)
INSERT INTO ESTADOS_INCIDENTE (nombre, activo) VALUES ('Reabierto', 1)

-- Prioridades
INSERT INTO PRIORIDADES (nombre, nivel, activo) VALUES ('Alta', 1, 1)
INSERT INTO PRIORIDADES (nombre, nivel, activo) VALUES ('Media', 2, 1)
INSERT INTO PRIORIDADES (nombre, nivel, activo) VALUES ('Baja', 3, 1)

-- Tipos de incidencia
INSERT INTO TIPOS_INCIDENCIA (nombre, descripcion, activo) VALUES ('Facturacion', 'Reclamos relacionados con cobros y facturas', 1)
INSERT INTO TIPOS_INCIDENCIA (nombre, descripcion, activo) VALUES ('Tecnico', 'Problemas tecnicos del servicio', 1)
INSERT INTO TIPOS_INCIDENCIA (nombre, descripcion, activo) VALUES ('Consulta', 'Consultas generales', 1)

--Clientes
INSERT INTO CLIENTES (nombre, email, telefono, identificacion, activo, fecha_alta)
VALUES ('Luis García', 'lgarcia@mail.com', '11-4455-6677', 'DNI 28450112', 1, GETDATE())

INSERT INTO CLIENTES (nombre, email, telefono, identificacion, activo, fecha_alta)
VALUES ('Ana Martínez', 'ana.martinez@mail.com', '11-5566-7788', 'DNI 31220445', 1, GETDATE())

INSERT INTO CLIENTES (nombre, email, telefono, identificacion, activo, fecha_alta)
VALUES ('Roberto López', 'rlopez@mail.com', '11-6677-8899', 'DNI 25100778', 1, GETDATE())

INSERT INTO CLIENTES (nombre, email, telefono, identificacion, activo, fecha_alta)
VALUES ('Paula Fernández', 'pfernandez@mail.com', NULL, 'DNI 33445566', 0, GETDATE())