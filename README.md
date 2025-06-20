Pasos Para el Funcionamiento del Programa:
1. Descargar el repositorio y descomprimir el Archivo .ZIP
2. Abrir el Programa
3. Abrir SQL Management 20 y ejecutar el siguiente codigo:
   Código usado:
BASE DE DATOS	
-- Crear la base de datos
CREATE DATABASE BancoTI;
GO

USE BancoTI;
GO
-- Crear tabla Cliente
CREATE TABLE Cliente (
    Cedula VARCHAR(20) PRIMARY KEY,
    Nombre VARCHAR(100),
    Apellido VARCHAR(100)
);
-- Crear tabla Cuenta
CREATE TABLE Cuenta (
    Numero VARCHAR(20) PRIMARY KEY,
    Tipo VARCHAR(50),
    Saldo DECIMAL(18, 2),
    CedulaCliente VARCHAR(20),
    FOREIGN KEY (CedulaCliente) REFERENCES Cliente(Cedula)
);
-- Crear tabla Transferencia
CREATE TABLE Transferencia (
    Numero INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATETIME,
    Valor DECIMAL(18, 2),
    CuentaOrigen VARCHAR(20),
    CuentaDestino VARCHAR(20),
    FOREIGN KEY (CuentaOrigen) REFERENCES Cuenta(Numero),
    FOREIGN KEY (CuentaDestino) REFERENCES Cuenta(Numero)
);
-- Insertar 5 registros en Cliente
INSERT INTO Cliente (Cedula, Nombre, Apellido) VALUES
('0101010101', 'Luis', 'Martínez'),
('0202020202', 'Ana', 'Pérez'),
('0303030303', 'Carlos', 'Gómez'),
('0404040404', 'María', 'López'),
('0505050505', 'Jorge', 'Andrade');
-- Insertar 5 registros en Cuenta (vinculadas con los clientes anteriores)
INSERT INTO Cuenta (Numero, Tipo, Saldo, CedulaCliente) VALUES
('10001', 'Ahorros', 1500.00, '0101010101'),
('10002', 'Corriente', 2500.50, '0202020202'),
('10003', 'Ahorros', 800.75, '0303030303'),
('10004', 'Corriente', 300.00, '0404040404'),
('10005', 'Ahorros', 5200.20, '0505050505');

4. Entrar a .json del Programa en Visual Studio 2022 y cambiar esta parte del codigo como se muestra a continuación:
   "ConnectionStrings": {
    "BancoTI": "Server=Nombre_De_Su_Equipo;Database=BancoTI;Trusted_Connection=True;TrustServerCertificate=True;"
}

5. Ejecutar el programa y Listo.
   
