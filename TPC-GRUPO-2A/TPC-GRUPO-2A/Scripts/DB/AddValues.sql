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
