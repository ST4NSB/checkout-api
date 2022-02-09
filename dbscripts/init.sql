DROP DATABASE IF EXISTS checkoutdb;    

CREATE DATABASE checkoutdb;
GRANT ALL PRIVILEGES ON DATABASE checkoutdb TO postgres;

\c checkoutdb

CREATE TABLE Customers
(
    CustomerId serial PRIMARY KEY,
    Name  VARCHAR (50)  NOT NULL,
    PaysVat  BOOLEAN NOT NULL,
    ClosedBasket BOOLEAN NOT NULL DEFAULT FALSE,
    PaidBasket BOOLEAN NOT NULL DEFAULT FALSE,
    CreatedDate DATE NOT NULL DEFAULT CURRENT_DATE
);

CREATE TABLE Products
(
    ProductId serial PRIMARY KEY,
    Name  VARCHAR (80)  NOT NULL,
    Price  DECIMAL NOT NULL,
    CreatedDate DATE NOT NULL DEFAULT CURRENT_DATE
);

CREATE TABLE BasketHistory
(
    BasketHistoryId serial PRIMARY KEY,
    CustomerId  INTEGER REFERENCES Customers (CustomerId),
    ProductId  INTEGER REFERENCES Products (ProductId),
    Quantity  INTEGER NOT NULL,
    CreatedDate DATE NOT NULL DEFAULT CURRENT_DATE,
    UpdatedDate DATE NOT NULL DEFAULT CURRENT_DATE
);

