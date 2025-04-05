--Criação do banco
CREATE DATABASE orderManagement;
GO

USE orderManagement;    
GO

-- IF OBJECT_ID('ClientOrderProduct', 'U') IS NOT NULL DROP TABLE ClientOrderProduct;
-- IF OBJECT_ID('ClientOrder', 'U') IS NOT NULL DROP TABLE ClientOrder;
-- IF OBJECT_ID('Product', 'U') IS NOT NULL DROP TABLE Product;
-- IF OBJECT_ID('DealerDeliveryAddress', 'U') IS NOT NULL DROP TABLE DealerDeliveryAddress;
-- IF OBJECT_ID('DealerContact', 'U') IS NOT NULL DROP TABLE DealerContact;
-- IF OBJECT_ID('Dealer', 'U') IS NOT NULL DROP TABLE Dealer;
-- IF OBJECT_ID('ProductCategory', 'U') IS NOT NULL DROP TABLE ProductCategory;
-- IF OBJECT_ID('OrderStatus', 'U') IS NOT NULL DROP TABLE OrderStatus;

-- Tabela de status dos pedidos
CREATE TABLE OrderStatus (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL
);
GO

-- Tabela de categorias de produto
CREATE TABLE ProductCategory (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL
);
GO

-- Tabela Dealer (representa o revendedor)
CREATE TABLE Dealer (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Cnpj VARCHAR(20) NOT NULL,
    RazaoSocial VARCHAR(100) NOT NULL,
    NomeFantasia VARCHAR(100) NULL,
    Email NVARCHAR(100) NULL
);
GO

-- Contatos do revendedor
CREATE TABLE DealerContact (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NULL,
    PhoneNumber NVARCHAR(15) NULL,
    DealerId INT NOT NULL,
    FOREIGN KEY (DealerId) REFERENCES Dealer(Id) ON DELETE CASCADE
);
GO

-- Endereços de entrega do revendedor
CREATE TABLE DealerDeliveryAddress (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Address VARCHAR(150) NULL,
    DealerId INT NOT NULL,
    FOREIGN KEY (DealerId) REFERENCES Dealer(Id) ON DELETE CASCADE
);
GO

-- Produtos
CREATE TABLE Product (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL,
    Name VARCHAR(150) NOT NULL,
    Description VARCHAR(150) NULL,
    Price DECIMAL(18,2) NOT NULL,
    SKU VARCHAR(150) NULL,
    CategoryId INT NULL,
    StockQuantity INT NOT NULL DEFAULT 0,
    FOREIGN KEY (CategoryId) REFERENCES ProductCategory(Id)
);
GO

-- Pedidos de clientes
CREATE TABLE ClientOrder (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    ClientCNPJ VARCHAR(14) NULL,
    DealerId INT NOT NULL,
    StatusId INT NOT NULL,
    TotalAmount DECIMAL(18,2)  NULL,
    FOREIGN KEY (DealerId) REFERENCES Dealer(Id),
    FOREIGN KEY (StatusId) REFERENCES OrderStatus(Id)
);
GO

-- Itens do pedido do cliente
CREATE TABLE ClientOrderProduct (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ClientOrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    TotalAmount DECIMAL(18,2)  NULL,
    FOREIGN KEY (ClientOrderId) REFERENCES ClientOrder(Id),
    FOREIGN KEY (ProductId) REFERENCES Product(Id)
);
GO

INSERT INTO OrderStatus (Name) VALUES 
('Pendente'),         
('Aprovado'),         
('Em separação'),     
('Enviado'),          
('Entregue'),         
('Cancelado'); 

INSERT INTO ProductCategory (Name) VALUES 
('Refrigerante'), 
('Cerveja'), 
('Água'), 
('Energético'), 
('Destilado');

INSERT INTO Product (Name, Description, Price, SKU, CategoryId, StockQuantity)
VALUES 
('Coca-Cola 2L', 'Refrigerante Coca-Cola garrafa PET 2L', 7.99, 'REF-COKE-2L', 1, 100),
('Guaraná Antártica 350ml', 'Refrigerante Guaraná lata 350ml', 3.49, 'REF-GUA-350', 1, 200),
('Skol Pilsen 350ml', 'Cerveja Skol lata 350ml', 2.99, 'BEER-SKOL-350', 2, 300),
('Água Mineral 500ml', 'Água mineral sem gás 500ml', 1.50, 'WAT-MIN-500', 3, 500),
('Red Bull 250ml', 'Energético Red Bull lata 250ml', 6.99, 'ENERG-RB-250', 4, 150);
