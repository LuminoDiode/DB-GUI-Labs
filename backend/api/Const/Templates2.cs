namespace api.Const;

public static class Templates2
{
	/// <summary>
	/// p1 - Manufacturer<br/>
	/// p2 - Departure<br/>
	/// p3 - Destination
	/// </summary>
	public const string AvgFlyTime_3p = """
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
	""";

	public const string MostlySameRoute_0p = """
		-- 2.	
		-- Выбрать марку самолета, которая чаще всего 
		-- летает по тому же маршруту. 

		-- для хранения промежуточных решений
		DROP TABLE IF EXISTS BrandToRoute;
		DROP TABLE IF EXISTS BrandToRouteDisctinct;
		DROP TABLE IF EXISTS BrandToRouteToCount;

		-- сначала получим brand-to-routeId
		SELECT plainMan AS Manufacturer, rid AS RouteId 
			INTO BrandToRoute FROM Flights
			JOIN (SELECT Routes.Id AS rid FROM Routes) as fooR
				ON Flights.RouteId = rid
			JOIN (SELECT 
				Airplanes.Id AS planeId,
				Airplanes.Manufacturer AS plainMan 
				FROM Airplanes) AS fooP
				ON Flights.AirplaneId = planeId;

		-- определим коллекцию, по которой будет проходить во внешнем цикле
		SELECT DISTINCT * INTO BrandToRouteDisctinct FROM BrandToRoute;

		-- теперь получим brand-to-count(routeId)
		SELECT DISTINCT
			outerBrandToRoute.Manufacturer,
			outerBrandToRoute.RouteId, 
			(SELECT count(*) FROM (SELECT 
				innerBrandToRoute.RouteId
				FROM BrandToRoute innerBrandToRoute WHERE
					innerBrandToRoute.Manufacturer = outerBrandToRoute.Manufacturer AND
					innerBrandToRoute.RouteId = outerBrandToRoute.RouteId
			) as foo1) as RouteCount INTO BrandToRouteToCount
			FROM BrandToRouteDisctinct AS outerBrandToRoute;

		-- теперь получим строку с максимальным routeCount
		SELECT * FROM BrandToRouteToCount WHERE
			BrandToRouteToCount.RouteCount IN (SELECT 
			max(BrandToRouteToCount.RouteCount) FROM BrandToRouteToCount);

		-- Successfully run. Total query runtime: 18 secs 648 msec.
		-- 1 rows affected.
		-- manafacturer     routeid     routecount
		--       Sukhoi         647             19

		-- для хранения промежуточных решений
		DROP TABLE IF EXISTS BrandToRoute;
		DROP TABLE IF EXISTS BrandToRouteDisctinct;
		DROP TABLE IF EXISTS BrandToRouteToCount;
	""";

	public const string MostlyEmpty70_0p = """
		-- 3.  
		-- Выбрать маршрут/маршруты, по которым чаще 
		-- всего летают рейсы, заполненные менее, чем на 70%. 


		-- для хранения промежуточных решений
		DROP TABLE IF EXISTS RouteToSoldPercent;
		DROP TABLE IF EXISTS RouteToLess70Count;

		-- сначала получим routeId-to-soldPercent
		SELECT
			Flights.RouteId as RouteId,
			(Flights.SoldCount/foo1.TotalCount::numeric) as SoldPercent
			INTO RouteToSoldPercent FROM Flights
				JOIN (SELECT 
						Airplanes.SeatsCount as TotalCount,
						Airplanes.Id as airplaneId1
					FROM Airplanes) AS foo1
				ON Flights.AirplaneId = airplaneId1;

		-- посчитаем число рейсов с заполнением <0.7 для каждого маршрута
		SELECT 
			RouteToSoldPercent.RouteId as RouteId,
			count(RouteToSoldPercent.SoldPercent) as Less70Count
			INTO RouteToLess70Count FROM RouteToSoldPercent
			WHERE RouteToSoldPercent.SoldPercent<0.7
			GROUP BY RouteToSoldPercent.RouteId;

		SELECT * FROM RouteToLess70Count WHERE
			RouteToLess70Count.Less70Count IN (SELECT 
			max( RouteToLess70Count.Less70Count) FROM RouteToLess70Count);

		-- Successfully run. Total query runtime: 90 msec.
		-- 4 rows affected.
		-- routeid      less70count
		--     863               34
		--     557               34
		--     566               34
		--      23               34

		-- для хранения промежуточных решений
		DROP TABLE IF EXISTS RouteToSoldPercent;
		DROP TABLE IF EXISTS RouteToLess70Count;
	""";


	/// <summary>
	/// p1 - flight id
	/// p2 - date ISO8601
	/// </summary>
	public const string FreeSeats_2p = """
		-- 4.
		-- Определить наличие свободных мест на рейс №870 31 декабря 2000г. 

		-- Маршрут 870, дату возьмем из существующих:
		-- 2000-08-03T18:08:36+04 (ISO 8601)
		SELECT 
		    Flights.SoldCount AS SeatsSold, 
		    SeatsTotal, 
		    (Flights.SoldCount<SeatsTotal) as HaveFreeSeats 
		        FROM Flights
		            JOIN (SELECT
		                Airplanes.SeatsCount AS SeatsTotal, 
		                Airplanes.Id as plId FROM Airplanes) as foo
		            ON plId = Flights.AirplaneId
		            WHERE 
		            Flights.RouteId = @p1 AND
		            DATE(Flights.DepartureTime) = @p2

		-- Successfully run. Total query runtime: 38 msec.
		-- 1 rows affected.
		-- seats_sold   seats_total     have_free_seats
		--         26           130                true
	""";

	public const string PlaneToDistanceOnRoute = """
		SELECT DISTINCT Routes.Id as routeId, Airplanes.Id as airplaneId, sum(Routes.DistanceKm), count(Flights.Id) FROM (
			Flights 
			JOIN Routes
			ON Flights.RouteId = Routes.Id
			JOIN Airplanes 
			ON Flights.AirplaneId = Airplanes.Id)
			GROUP BY Airplanes.Id, Routes.Id;
	""";

}
