CREATE DATABASE ClinicSystemDb;
GO

USE ClinicSystemDb;
GO

CREATE TABLE [User] (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(255) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender BIT NOT NULL DEFAULT 1,
    Address NVARCHAR(500) NULL,
    PhoneNumber NVARCHAR(10) NOT NULL UNIQUE,
    Status INT NOT NULL,
    Role NVARCHAR(50) NOT NULL CHECK (Role IN ('admin', 'doctor', 'patient'))
);

CREATE TABLE Appointment (
    AppointmentId INT IDENTITY(1,1) PRIMARY KEY,
    PatientId INT NOT NULL,
    DoctorId INT NOT NULL,
    AppointmentDate DATETIME NOT NULL,
    Diagnosis TEXT NULL,
    Status INT NOT NULL,
    Description NVARCHAR(1000) NULL,
    FOREIGN KEY (PatientId) REFERENCES [User](UserId) ON DELETE NO ACTION,
    FOREIGN KEY (DoctorId) REFERENCES [User](UserId) ON DELETE NO ACTION
);

CREATE TABLE Prescription (
    PrescriptionId INT IDENTITY(1,1) PRIMARY KEY,
    PrescriptionName NVARCHAR(255) NOT NULL,
    Description NVARCHAR(1000) NULL,
    AppointmentId INT NOT NULL,
    FOREIGN KEY (AppointmentId) REFERENCES Appointment(AppointmentId) ON DELETE CASCADE
);

CREATE TABLE PrescriptionDetail (
    PrescriptionDetailId INT IDENTITY(1,1) PRIMARY KEY,
    PrescriptionId INT NOT NULL,
    Medication NVARCHAR(255) NOT NULL,
    Dosage NVARCHAR(255) NOT NULL,
    FOREIGN KEY (PrescriptionId) REFERENCES Prescription(PrescriptionId) ON DELETE CASCADE
);
