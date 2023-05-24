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
