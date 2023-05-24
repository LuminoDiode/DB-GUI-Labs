-- 1.	
-- Определить среднее расчетное время полета 
-- для самолета 'ТУ-154' по маршруту 'Чугуев' - 'Мерефа'. 

-- Пусть ТУ-154                 ==  Boeing
-- Пусть 'Чугуев' - 'Мерефа'    == 'Padbergport' - 'Hilllshire'

SELECT avg(ArrivalTime-DepartureTime) FROM Flights
    WHERE Flights.AirplaneId IN(
        SELECT Airplanes.Id FROM Airplanes 
            WHERE Airplanes.Manufacturer
            = 'Boeing'
    ) AND Flights.RouteId IN(
        SELECT Routes.Id FROM Routes
            WHERE Routes.DeparturePoint
            = 'Padbergport'
            AND Routes.DestinationPoint
            = 'Hilllshire'
    )

-- Successfully run. Total query runtime: 37 msec.
-- 1 rows affected.
-- 11:39:03