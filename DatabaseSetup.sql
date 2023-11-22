CREATE DATABASE IF NOT EXISTS UserData;
USE UserData;

CREATE TABLE Users (
    UserID INT AUTO_INCREMENT PRIMARY KEY,
    Surname VARCHAR(255),
    Forenames VARCHAR(255) NOT NULL,
    Title VARCHAR(255)
);

CREATE TABLE Logins (
    LoginID VARCHAR(255) PRIMARY KEY,
    UserID INT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

CREATE TABLE Positions (
    PositionID INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT,
    Position VARCHAR(255) NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

CREATE TABLE Phones (
    PhoneID INT AUTO_INCREMENT PRIMARY KEY,
    Phone VARCHAR(20) UNIQUE NOT NULL CHECK (Phone LIKE '+%' AND Phone NOT LIKE '%[^0-9+]%')
);

CREATE TABLE UserPhones (
    UserID INT,
    PhoneID INT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (PhoneID) REFERENCES Phones(PhoneID),
    PRIMARY KEY (UserID, PhoneID)
);

CREATE TABLE Emails (
    EmailID INT AUTO_INCREMENT PRIMARY KEY,
    Email VARCHAR(255) UNIQUE NOT NULL
);

CREATE TABLE UserEmails (
    UserID INT,
    EmailID INT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (EmailID) REFERENCES Emails(EmailID),
    PRIMARY KEY (UserID, EmailID)
);

CREATE TABLE Locations (
    UserID INT PRIMARY KEY,
    Location VARCHAR(255) NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);