namespace api.Const;

public class PremadeAirportRequests
{
	public class Lab1
	{
		public const string CreateDb = """
			CREATE DATABASE AirportLabs;
		""";

		public const string DropDb = """
			DROP DATABASE AirportLabs WITH (FORCE);
		""";

		public const string CreateTables = """
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
		""";

#if DEBUG
		/// <summary>
		/// local usage only
		/// </summary>
		public const string CopyStaticData = """
			COPY Airplanes FROM 'C:\t\airplanes.csv' DELIMITER ';' CSV;
			COPY Routes FROM 'C:\t\routes.csv' DELIMITER ';' CSV;
			COPY Flights FROM 'C:\t\flights.csv' DELIMITER ';' CSV;

			-- для лучшей демонстрации далее, время ISO 8601
			INSERT INTO Flights VALUES
				((select max(Flights.Id) from Flights)+1,'2007-09-05T02:27:17+04','2007-09-05T06:09:38+04', 10, 50,12),
				((select max(Flights.Id) from Flights)+2,'2008-09-05T02:27:17+04','2008-09-05T06:09:38+04', 20, 50,12)
		""";
#endif
	}

	public class Lab2
	{
		public const string AverageFlyTime_Brand_Dept_Dest = """
			-- 1.	
			-- Определить среднее расчетное время полета 
			-- для самолета 'ТУ-154' по маршруту 'Чугуев' - 'Мерефа'. 

			-- Пусть ТУ-154                 ==  Boeing
			-- Пусть 'Чугуев' - 'Мерефа'    == 'Padbergport' - 'Hilllshire'

			SELECT avg(ArrivalTime-DepartureTime) FROM Flights
			    WHERE Flights.AirplaneId IN(
			        SELECT Airplanes.Id FROM Airplanes 
			            WHERE Airplanes.Manufacturer
			            = @p1
			    ) AND Flights.RouteId IN(
			        SELECT Routes.Id FROM Routes
			            WHERE Routes.DeparturePoint
			            = @p2
			            AND Routes.DestinationPoint
			            = @p3
			    )

			-- Successfully run. Total query runtime: 37 msec.
			-- 1 rows affected.
			-- 11:39:03
		""";
	}

}
