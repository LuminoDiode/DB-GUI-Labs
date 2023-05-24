-- Создать БД "Аэропорт"
-- Сущности: 
--  Самолет, Рейс, Маршрут
--  (рейс, иначе говоря - перелет)
-- Отношения:
--  Самолет-Рейс -- One-To-Many
--  Маршрут-Рейс -- One-To-Many

CREATE TABLE Airplanes(
    Id SERIAL PRIMARY KEY,
    BoardIdentifier VARCHAR(16) UNIQUE, -- Бортовой номер (напр. СССР-45917)
    Manufacturer VARCHAR(32), -- Марка
    CruiseSpeedKmh INTEGER, -- Крейсерская скорость
    SeatsCount INTEGER -- Число мест
);

CREATE TABLE Routes(
    Id SERIAL PRIMARY KEY,
    DistanceKm INTEGER, -- Расстоние км
    DestinationPoint VARCHAR(32), -- пункт назначения
    DeparturePoint VARCHAR(32) -- пункт отправления
);

CREATE TABLE Flights(
    Id SERIAL PRIMARY KEY,
    DepartureTime TIMESTAMP WITH TIME ZONE, -- Время отправления
    ArrivalTime TIMESTAMP WITH TIME ZONE, -- Время прибытия
    SoldCount INTEGER, -- Число проданных билетов

    AirplaneId INTEGER REFERENCES Airplanes,
    RouteId INTEGER REFERENCES Routes
);