CREATE DATABASE domo;

USE domo;

CREATE TABLE [Domo] (
	[id_domo] int IDENTITY(1,1) NOT NULL UNIQUE,
	[nombre] nvarchar(50) NOT NULL,
	[ubicacion] nvarchar(150) NOT NULL,
	[fecha_creacion] datetime NOT NULL,
	PRIMARY KEY ([id_domo])
);

CREATE TABLE [Usuario] (
	[id_usuario] int IDENTITY(1,1) NOT NULL UNIQUE,
	[nombre] nvarchar(100) NOT NULL,
	[DNI] nvarchar(8) NOT NULL,
	[clave] nvarchar(50) NOT NULL,
	[es_admin] nvarchar(max) NOT NULL,
	PRIMARY KEY ([id_usuario])
);

CREATE TABLE [Data_Sensores] (
	[id_data_sensor] int IDENTITY(1,1) NOT NULL UNIQUE,
	[id_domo] int NOT NULL,
	[temperatura] float(53) NOT NULL,
	[humedad_tierra] float(53) NOT NULL,
	[humedad_ambiente] float(53) NOT NULL,
	[estado_ventilador] nvarchar(max) NOT NULL,
	[estado_ventana] nvarchar(max) NOT NULL,
	[fecha] datetime NOT NULL,
	PRIMARY KEY ([id_data_sensor])
);

CREATE TABLE [Perfil_Planta] (
	[id_perfil_planta] int IDENTITY(1,1) NOT NULL UNIQUE,
	[nombre_perfil] int NOT NULL,
	PRIMARY KEY ([id_perfil_planta])
);

CREATE TABLE [Parametro_Perfil] (
	[id_parametro_perfil] int IDENTITY(1,1) NOT NULL UNIQUE,
	[id_perfil_planta] int NOT NULL,
	[parametro] nvarchar(50) NOT NULL,
	[valor] nvarchar(10) NOT NULL,
	PRIMARY KEY ([id_parametro_perfil])
);

-- Creamos las relaciones

ALTER TABLE [Data_Sensores] ADD CONSTRAINT [Data_Sensores_fk1] FOREIGN KEY ([id_domo]) REFERENCES [Domo]([id_domo]);

ALTER TABLE [Parametro_Perfil] ADD CONSTRAINT [Parametro_Perfil_fk1] FOREIGN KEY ([id_perfil_planta]) REFERENCES [Perfil_Planta]([id_perfil_planta]);

-- Registramos el primer domo

INSERT INTO [domo].[dbo].[Domo] (nombre, ubicacion, fecha_creacion)
VALUES ('Domo GAL', 'Av. Bohemia Tacneña #95', GETDATE());

SELECT* from Data_Sensores