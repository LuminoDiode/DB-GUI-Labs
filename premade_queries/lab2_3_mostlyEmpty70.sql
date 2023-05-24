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