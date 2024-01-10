
USE BusRoutes;
GO

CREATE TABLE stops (
stop_id INT IDENTITY PRIMARY KEY,
stop_name VARCHAR(50) NOT NULL
);

CREATE TABLE buses (
bus_id INT IDENTITY PRIMARY KEY,
bus_name VARCHAR(50) NOT NULL
);

CREATE TABLE bus_stops (
bus_id INT,
stop_id INT,
PRIMARY KEY (bus_id, stop_id),
FOREIGN KEY (bus_id) REFERENCES buses(bus_id),
FOREIGN KEY (stop_id) REFERENCES stops(stop_id)
);

SET IDENTITY_INSERT stops ON;

INSERT INTO stops(stop_id, stop_name) VALUES
(1, 'Пушкина'),
(2, 'Селезнева'),
(3, 'Некрасова'),
(4, 'Красина'),
(5, 'Рябиновая'),
(6, 'Забалуево'),
(7, 'Ленина');

SET IDENTITY_INSERT stops OFF;

INSERT INTO buses(bus_name) VALUES
('Автобус 1'),
('Автобус 2'),
('Автобус 3');

INSERT INTO bus_stops(bus_id, stop_id) VALUES
(1, 1),
(1, 2),
(1, 3),
(1, 4),
(2, 5),
(2, 1),
(2, 6),
(2, 7),
(3, 2),
(3, 3),
(3, 7);