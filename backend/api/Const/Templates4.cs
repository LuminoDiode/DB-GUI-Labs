namespace api.Const;

public static class Templates4
{
	/// <summary>
	/// p1 - Departure<br/>
	/// p2 - Destination
	/// </summary>
	public const string CreateTimeTable_2p = """
		-- 3. 
		-- Создать таблицу расписания самолетов по 
		-- маршруту «Москва»-«Васюки».

		-- Пусть '«Москва»' - '«Васюки»'   
		-- == 'Padbergport' - 'Hilllshire'

		SELECT 
		    Routes.DeparturePoint, 
		    Routes.DestinationPoint,
			depT as DepartureTime,
			arrT as ArrivalTime
		    FROM Routes
		    JOIN (SELECT 
		        Flights.RouteId as flId,
		        Flights.DepartureTime as depT,
		        Flights.ArrivalTime as arrT
		        FROM Flights) as foo
		    ON Routes.Id = flId
		    WHERE 
		        Routes.DeparturePoint = @p1 AND
		        Routes.DestinationPoint = @p2
		    ORDER BY
		        depT;

		-- Successfully run. Total query runtime: 38 msec.
		-- 29 rows affected.
	""";

	/// <summary>
	/// p1 - Brand<br/>
	/// p2 - Delta
	/// </summary>
	public const string ChangeCapacity_2p = """
		-- 4. 
		-- Увеличить число мест самолетов «ТУ» на 5 человек. 
		-- Пусть ТУ = Boening

		UPDATE Airplanes SET 
			SeatsCount = SeatsCount + @p2
			WHERE 
				Airplanes.Manufacturer = @p1;

		-- UPDATE 57
		-- Query returned successfully in 31 msec.
	""";

	/// <summary>
	/// p1 - Brand<br/>
	/// p2 - Delta
	/// </summary>
	public const string FlightsOnRoute_0p = """
		-- 5. 
		-- Создать сводную таблицу количества 
		-- вылетов самолетов по маршрутам. 

		SELECT
			Routes.Id as RouteId,
			Routes.DeparturePoint, 
			Routes.DestinationPoint,
			routeIdCount as FlightsCound
		--  INTO RoutesToFlightsCountTable
			FROM Routes
			JOIN (SELECT 
				Flights.RouteId as routeId,
				count(Flights.RouteId) as routeIdCount
				FROM Flights GROUP BY Flights.RouteId) as foo
			ON Routes.Id = routeId
			ORDER BY Routes.Id;
	""";

	/// <summary>
	/// p1 - Coeff<br/>
	/// p2 - Brand
	/// </summary>
	public const string UpdateSpeed_2p = """
		UPDATE Airplanes SET 
		CruiseSpeedKmh = CruiseSpeedKmh * @p1
		WHERE 
		    Airplanes.Manufacturer = @p2;
		""";
}
