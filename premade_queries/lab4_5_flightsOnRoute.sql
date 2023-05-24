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