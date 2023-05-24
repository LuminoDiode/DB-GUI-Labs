using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    CREATE TABLE Airplanes(
        Id SERIAL PRIMARY KEY,
        BoardIdentifier VARCHAR(16), -- Бортовой номер (напр. СССР-45917)
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
*/

class Airplane
{
	public int Id { get; set; }
	public string BoardIdentifier { get; set; }
	public string Manufacturer { get; set; }
	public int CruiseSpeedKmh { get; set; }
	public int SeatsCount { get; set; }
}

class Route
{
	public int Id { get; set; }
	public int DistanceKm { get; set; }
	public string DestinationPoint { get; set; }
	public string DeparturePoint { get; set; }
}

class Flight
{
	public int Id { get; set; }
	public DateTimeOffset DepartureTime { get; set; }
	public DateTimeOffset ArrivalTime { get; set; }
	public int SoldCount { get; set; }

	public int AirplaneId { get; set; }
	public int RouteId { get; set; }
}
