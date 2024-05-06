use master
go
if exists(select * from sys.databases where name='DBWebEscuela')
drop database DBWebEscuela
go
create database DBWebEscuela
go
use DBWebEscuela
go
--Creación de tabla ROLES
create table Roles(
	Rol varchar(20) primary key
)
go
--Hardcoding ROLES
insert Roles values('ADMINISTRADOR')
insert Roles values('DIRECTOR DE ESTUDIOS')
insert Roles values('PROFESOR')
insert Roles values('PRECEPTOR')
insert Roles values('ALUMNO')
insert Roles values('INSCRIPTO')
insert Roles values('EXCLUIDO')
go

---------------------------------------------
-- TABLE USUARIOS
---------------------------------------------

create table Usuarios(
	ID int identity(1,1) primary key,
	Nombre varchar(30) not null,
	Dni int unique not null,
	Direccion varchar(50) not null,
	FechaNac smalldatetime not null,
	Mail varchar(40) unique not null,
	Telefono varchar(15) not null,
	Password varchar(40) not null
)
go

create procedure Usuarios_Insert(
@Nombre varchar(30),
@Dni int,
@Direccion varchar(50),
@FechaNac smalldatetime,
@Mail varchar(40),
@Telefono varchar(15),
@Password varchar(40)
)
as begin
	insert Usuarios values(@Nombre, @Dni, @Direccion, @FechaNac, @Mail, @Telefono, @Password)
	select @@IDENTITY
end
go

create procedure Usuarios_Update(
@ID int,
@Nombre varchar(30),
@Direccion varchar(50),
@FechaNac smalldatetime,
@Telefono varchar(15),
@Password varchar(40)
)
as
if @Password<>'' begin
update Usuarios set
Nombre=@Nombre,
Direccion=@Direccion,
FechaNac=@FechaNac,
Telefono=@Telefono,
Password=@Password
where ID=@ID
end
else begin
update Usuarios set
Nombre=@Nombre,
Direccion=@Direccion,
FechaNac=@FechaNac,
Telefono=@Telefono
where ID=@ID
end
go

create procedure Usuarios_Delete(@ID int)
as
delete Usuarios where ID=@ID and ID<>1
go

create procedure Usuarios_Find(@ID int)
as
select ID, Nombre, Dni, Direccion, FechaNac, Mail, Telefono from Usuarios where ID=@ID
go

--Procedures especiales de existencia de usuarios

create procedure Usuarios_MailExists(@ID int, @Mail varchar(40))
as
select count(*) from Usuarios where
ID<>@ID and Mail=@Mail
go

create procedure Usuarios_DniExists(@ID int, @Dni int)
as
select count(*) from Usuarios where
ID<>@ID and Dni=@Dni
go

create procedure Usuarios_FindByMail(@Mail varchar(40))
as
select ID, Nombre, Dni, Direccion, FechaNac, Mail, Telefono from Usuarios where Mail=@Mail
go

create procedure Usuarios_FindByDni(@Dni int)
as
select ID, Nombre, Dni, Direccion, FechaNac, Mail, Telefono from Usuarios where Dni=@Dni
go

create procedure Usuarios_FindByMailAndDni(@Mail varchar(40), @Dni int)
as
select ID, Nombre, Dni, Direccion, FechaNac, Mail, Telefono from Usuarios where Mail=@Mail and Dni=@Dni
go

create procedure Usuarios_Login(@Mail varchar(40), @Password varchar(40))
as
select ID, Nombre, Dni, Direccion, FechaNac, Mail, Telefono from Usuarios where Mail=@Mail and Password=@Password
go

create procedure Usuarios_List
as
select ID, Nombre, Dni, Direccion, FechaNac, Mail, Telefono from Usuarios
go

---------------------------------------------
-- TABLE USUARIOSROLES
---------------------------------------------
create table UsuariosRoles(
ID int identity(1,1) primary key,
IDUsuario int foreign key references Usuarios(ID) on delete cascade not null,
Rol varchar(20) foreign key references Roles(Rol) not null,
constraint UKUR unique(IDUsuario, Rol)
)
go

--Stored procedures de UsuariosRoles
create procedure UsuariosRoles_Insert(@IDUsuario int, @Rol varchar(20))
as begin
insert UsuariosRoles values (@IDUsuario, @Rol)
select @@IDENTITY
end
go

create procedure UsuariosRoles_Update(@ID int, @IDUsuario int, @Rol varchar(20))
as
update UsuariosRoles set
IDUsuario=@IDUsuario,
Rol=@Rol
where ID=@ID
go

create procedure UsuariosRoles_Delete(@IDUsuario int, @Rol varchar(20))
as
delete UsuariosRoles where IDUsuario=@IDUsuario and Rol=@Rol
go

create procedure UsuariosRoles_Find(@IDUsuario int, @Rol varchar(20))
as
select * from UsuariosRoles where IDUsuario=@IDUsuario and Rol=@Rol
go

create procedure UsuariosRoles_ListByUsuario(@IDUsuario int)
as
select * from UsuariosRoles where IDUsuario=@IDUsuario
go

create procedure UsuariosRoles_ListByRol(@Rol varchar(20))
as
select * from UsuariosRoles where Rol=@Rol
go

--Hardcoding Usuario Administrador
--Nombre='admin',
--Dni=1,
--Direccion='dir admin',
--FechaNac=01-01-1990,
--Mail='admin@mail.com'
--Telefono='00000000000'
--Password='D033E22AE348AEB5660FC2140AEC35850C4DA997'
insert Usuarios values(
'admin',
1,
'dir admin',
'01-01-1990',
'admin@mail.com',
'000000000000',
'D033E22AE348AEB5660FC2140AEC35850C4DA997')

insert UsuariosRoles values(1,'ADMINISTRADOR')
insert UsuariosRoles values(1,'DIRECTOR DE ESTUDIOS')

---------------------------------------------
-- TABLE CARRERAS
---------------------------------------------

create table Carreras(
ID int identity(1,1) primary key,
Sigla varchar(10) unique not null,
Nombre varchar(60) unique not null,
Duracion int not null,
Titulo varchar(60) not null,
Estado varchar(10) not null check (Estado='ACTIVO' or Estado='INACTIVO')
)
go

create procedure Carreras_Insert(
@Sigla varchar(10),
@Nombre varchar(60),
@Duracion int,
@Titulo varchar(60),
@Estado varchar(10)
)
as begin
insert Carreras values (@Sigla, @Nombre, @Duracion, @Titulo, @Estado)
select @@IDENTITY
end
go

create procedure Carreras_Update(
@ID int,
@Sigla varchar(10),
@Nombre varchar(60),
@Duracion int,
@Titulo varchar(60),
@Estado varchar(10)
)
as
update Carreras set
Sigla = @Sigla,
Nombre = @Nombre,
Duracion = @Duracion,
Titulo = @Titulo,
Estado = @Estado
where ID = @ID
go

create procedure Carreras_Delete(@ID int)
as
delete from Carreras where ID=@ID
go

create procedure Carreras_Find(@ID int)
as
select * from Carreras where ID=@ID
go

create procedure Carreras_List(@Estado varchar(10))
as
select * from Carreras where Estado = @Estado
go

create procedure Carreras_FindBySigla(@Sigla varchar(10))
as
select * from Carreras where Sigla = @Sigla
go

create procedure Carreras_SiglaExists(@ID int, @Sigla varchar(10))
as
select Count(*) from Carreras where @ID!=ID and Sigla=@Sigla
go

create procedure Carreras_NombreExists(@ID int, @Nombre varchar(60))
as
select Count(*) from Carreras where @ID!=ID and Nombre=@Nombre
go