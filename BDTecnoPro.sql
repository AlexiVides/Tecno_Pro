create database TecnoPro;

use TecnoPro;

create table Usuario
(
idUsuario int primary key identity(1,1)not null,
correo varchar(50)unique not null,
contra varchar(50)not null,
estado varchar(50)default 'activo' null,
);

create table Empleado
(
idEmpleado int primary key identity(1,1)not null,
nombre varchar(50)not null,
apellido varchar(50)not null,
genero varchar (50)not null,
dui varchar (10) unique not null,
telefono varchar(9) not null,
direccion varchar(200)not null,
estado varchar(50)default 'activo' null,
idUsuario int foreign key references Usuario(idUsuario)not null,
);

create table Producto
(
idProducto int primary key identity(1,1)not null,
nombre varchar(50)not null,
cantidad int not null,
precio decimal not null,
detalle varchar(200)null,
marca varchar(50)not null,
modelo varchar(50)not null,
codigo varchar(50)not null,
imagen varbinary(max),
estado varchar(50)default 'activo'null,
);

create table Venta
(
idVenta int primary key identity(1,1)not null,
nombreCliente varchar(100)not null,
cantidad int not null,
total decimal not null,
descripcion varchar(200) not null,
fecha date default getdate()not null,
estado varchar(50)default 'activo'null,
idProducto int foreign key references Producto(idProducto)not null,
idEmpleado int foreign key references Empleado(idEmpleado)not null,
);

insert into Usuario (correo,contra)values ('tilin02@gmail.com','1234');

insert into Empleado(nombre,apellido,genero,dui,telefono,direccion,idUsuario)values ('Adiel','Carcamo','Masculino','0012345245','76102394','Donde matan,casa#12',1);

insert into Producto(nombre,cantidad,precio,detalle,marca,modelo,codigo)values ('Teclado',10,80,'Gamer,RGB','Razer','Premiun','0901');

insert into Venta(nombreCliente,cantidad,total,descripcion,idProducto,idEmpleado)values ('Neto',2,160,'Teclado Gamer RGB ,Razer Premiun',1,1);
