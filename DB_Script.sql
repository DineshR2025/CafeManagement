-- Create Database
CREATE DATABASE CafeManagement;

-- Switch to CafeManagement database
USE CafeManagement;

-- Create Cafe Table
CREATE TABLE Cafe (
    id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), -- UUID
    name VARCHAR(10) NOT NULL,
    description VARCHAR(256) NOT NULL,
    logo VARBINARY(MAX), -- Storing binary data for the logo
    location VARCHAR(60) NOT NULL,
    PRIMARY KEY (id)
);
GO
-- Create Employee Table
CREATE TABLE Employee (
    id VARCHAR(9) NOT NULL UNIQUE CHECK (id LIKE 'UI%'),
    [name] VARCHAR(10) NOT NULL,
    email_address VARCHAR(255) NOT NULL UNIQUE CHECK (email_address LIKE '%@%.%'),
    phone_number CHAR(8) NOT NULL UNIQUE CHECK (phone_number LIKE '[89]%' AND LEN(phone_number) = 8),
    gender VARCHAR(6) NOT NULL CHECK (gender IN ('Male', 'Female')), -- Using CHAR with a CHECK constraint,
	cafe_id UNIQUEIDENTIFIER NOT NULL,
    [start_date] DATE NOT NULL,
    PRIMARY KEY (id),
	FOREIGN KEY (cafe_id) REFERENCES Cafe(id) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

-- Create a SEQUENCE for generating the numeric part of EmployeeID
CREATE SEQUENCE EmployeeSequence
START WITH 1000001 -- Starts after 'UI1000000'
INCREMENT BY 1;
GO

-- Create a trigger to generate EmployeeID on insert
CREATE TRIGGER trg_GenerateEmployeeID
ON Employee
INSTEAD OF INSERT
AS
BEGIN
    INSERT INTO Employee (id, Name, email_address, phone_number, gender,start_date, cafe_id)
    SELECT 
        CONCAT('UI', RIGHT('0000000' + CAST(NEXT VALUE FOR EmployeeSequence AS NVARCHAR), 7)), -- Generate EmployeeID
        Name,
        email_address,
        phone_number,
        Gender,
		start_date,
        cafe_id
    FROM inserted;
END;
Go