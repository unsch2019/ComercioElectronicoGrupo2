
DROP TABLE IF EXISTS UsuarioMenu;
DROP TABLE IF EXISTS EntradaDetalle;
DROP TABLE IF EXISTS PedidoDetalle;
DROP TABLE IF EXISTS ProductoImagen;

DROP TABLE IF EXISTS Entrada;
DROP TABLE IF EXISTS Pedido;
DROP TABLE IF EXISTS Direccion;

DROP TABLE IF EXISTS Proveedor;
DROP TABLE IF EXISTS Cliente;
DROP TABLE IF EXISTS Producto;
DROP TABLE IF EXISTS Marca;
DROP TABLE IF EXISTS Categoria;
DROP TABLE IF EXISTS Usuario;
DROP TABLE IF EXISTS Menu;

CREATE TABLE Usuario(
	Id int primary key identity(1,1),
	Nombre varchar(250) not null,
	Correo varchar(100) not null,
	Clave varchar(30) not null,
	Celular varchar(20),
	Activo bit not null default 1,
	IndCambio bit not null default 1
)
CREATE TABLE Marca(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Denominacion VARCHAR(255) NOT NULL	
)
CREATE TABLE Categoria(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Denominacion VARCHAR(255) NOT NULL	
)
CREATE TABLE Producto
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Codigo VARCHAR(50) NOT NULL,	
	Denominacion VARCHAR(255) NOT NULL,
	Descripcion VARCHAR(MAX) NOT NULL,
	Costo DECIMAL(10,2) NOT NULL DEFAULT 0,
	Precio DECIMAL(10,2) NOT NULL DEFAULT 0,
	Existencias INT NOT NULL DEFAULT 0,
	MarcaId INT REFERENCES Marca(Id) NOT NULL,
	CategoriaId INT REFERENCES Categoria(Id) NOT NULL,	
	Estado CHAR(1) NOT NULL,
	UsuarioCreacionId int not null references Usuario(Id),
	FechaCreacion datetime not null
)
CREATE TABLE ProductoImagen
(
	ProductoImagenId INT PRIMARY KEY IDENTITY(1,1),
	ProductoId INT REFERENCES Producto(Id) NOT NULL,
	Imagen VARCHAR(255) NOT NULL ,
	Titulo VARCHAR(255)
)

CREATE TABLE Cliente
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Nombres VARCHAR(255) NOT NULL,
	Apellidos VARCHAR(255) NOT NULL,
	Correo VARCHAR(255) NOT NULL ,	
	Clave VARCHAR(255) NOT NULL,
	Celular VARCHAR(255) 
)
CREATE TABLE Direccion(
	Id Int Primary Key Identity(1,1),
	Denominacion VARCHAR(255) NOT NULL,
	Estado CHAR(1) NOT NULL,
	ClienteId INT References Direccion(Id)
)
CREATE TABLE Pedido
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	ClienteId INT REFERENCES Cliente(Id) NOT NULL,
	DireccionId INT REFERENCES Direccion(Id),
	Fecha DATETIME NOT NULL,
	Total decimal(10,2),
	Estado CHAR(1) NOT NULL
)
CREATE TABLE PedidoDetalle
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	PedidoId INT REFERENCES Pedido(Id) NOT NULL,
	ProductoId INT REFERENCES Producto(Id) NOT NULL,
	Precio decimal(10,2),
	Cantidad int NOT NULL
)
CREATE TABLE Proveedor
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Nombres VARCHAR(255) NOT NULL,
	TipoDocumento CHAR(3),
	NumeroDocumento VARCHAR(11),
	Correo VARCHAR(255),		
	Celular VARCHAR(255),
	UsuarioCreacionId int not null references Usuario(Id),
	FechaCreacion datetime not null
)
CREATE TABLE Entrada
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	ProveedorId INT REFERENCES Proveedor(Id) NOT NULL,
	TipoDocumento VARCHAR(20),
	NumeroDocumento VARCHAR(50),
	Fecha DATETIME NOT NULL,
	Total decimal(10,2),
	Estado CHAR(1) NOT NULL,
	UsuarioCreacionId int not null references Usuario(Id),
	FechaCreacion datetime not null
)
CREATE TABLE EntradaDetalle
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	EntradaId INT REFERENCES Entrada(Id) NOT NULL,
	ProductoId INT REFERENCES Producto(Id) NOT NULL,
	Costo decimal(10,2),
	Cantidad int NOT NULL,
	Lote varchar(50),
	FechaVencimiento DATE,
)
CREATE TABLE Menu(
	Id int primary key,
	Nombre varchar(250) not null,
	Modulo varchar(100) not null,
	Icono varchar(30),
	IndPadre bit not null default 0,
	Referencia int
)
CREATE TABLE UsuarioMenu(
	Id int primary key IDENTITY(1,1),
	UsuarioId int not null references Usuario(Id),
	MenuId int not null references Menu(Id),
)

INSERT INTO menu(Id,Nombre,Modulo,Icono,IndPadre,Referencia) VALUES (10, 'Seguridad', 'Usuario','mdi-hardware-security', 1, null);
INSERT INTO menu(Id,Nombre,Modulo,Icono,IndPadre,Referencia) VALUES (11, 'Usuarios', 'Usuario', 	null,0, 10);

INSERT INTO usuario VALUES ('Administrador', 'admin@gmail.com', '123',null,1, 0);

INSERT INTO UsuarioMenu(UsuarioId,MenuId) VALUES (1,11);