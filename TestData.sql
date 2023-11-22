USE UserData;

INSERT INTO Users (Surname, Forenames, Title) VALUES
('Tompsett', 'Brian C', 'Eur Ing'),
('Smith', 'John David', 'Mr'),
('Doe', 'Jane R', 'Dr'),
('Brown', 'Charlie', 'Prof'),
('Davis', 'Alice', 'Mrs'),
('Johnson', 'Emily', 'Ms'),
('Wilson', 'George', 'Mr'),
('Moore', 'Grace', 'Dr'),
('Taylor', 'Michael', 'Prof'),
('Anderson', 'Lily', 'Ms');



INSERT INTO Logins (LoginID, UserID) VALUES
('cssbct', 1),
('jsmith', 2),
('emjohnson', 3),
('swilliams', 4),
('rjones', 5),
('jbrown', 6),
('mdavis', 7),
('smiller', 8),
('jwilson', 9),
('ltaylor', 10);

INSERT INTO Positions (UserID, Position) VALUES
(1, 'Lecturer of Computer Science'),
(2, 'Software Developer'),
(3, 'Head Lecturer of Computer Science'),
(4, 'Lecturer of Physics'),
(5, 'Lecturer of Biology'),
(6, 'Lecturer of Law '),
(7, 'Database Administrator'),
(8, 'Principle'),
(9, 'Head of School'),
(10, 'Student');

INSERT INTO Phones (Phone) VALUES
('+441234567890'),
('+442345678901'),
('+443456789012'),
('+444567890123');

INSERT INTO UserPhones (UserID, PhoneID) VALUES
(1, 1),
(2, 2),
(3, 1),
(4, 3),
(5, 4),
(6, 2),
(7, 4),
(8, 3),
(9, 1),
(10, 2);

INSERT INTO Emails (Email) VALUES
('brian.t@example.com'),
('john.smith@example.com'),
('emily.j@example.com'),
('sarah.w@example.com'),
('robert.j@example.com'),
('jessica.b@example.com'),
('michael.d@example.com'),
('susan.m@example.com'),
('james.w@example.com'),
('laura.t@example.com');

INSERT INTO UserEmails (UserID, EmailID) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10);

INSERT INTO Locations (UserID, Location) VALUES
(1, 'In RB-336'),
(2, 'Fenner Lab A'),
(3, 'Fenner Lab B'),
(4, 'Fenner Lab C'),
(5, 'In RB-337'),
(6, 'Wilberforce Lecture Theater 2'),
(7, 'In RB-335'),
(8, 'In RB-339'),
(9, 'In RB-236'),
(10, 'Wilberforce Leacture Theater 15');


