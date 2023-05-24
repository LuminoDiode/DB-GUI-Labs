-- 1.
-- Напишите запрос, который выводит все 
-- заказы, сделанные 3 и 4 октября 2003 
-- с использование оператора IN и оператора BETWEEN.
-- (адаптировать под свою тематику)

-- Пусть:
-- Вывести самолеты брендов Airbus и Sukhoi,
-- количество мест в которых от 200 до 300.

SELECT * FROM Airplanes WHERE
    Airplanes.Manufacturer IN ('Airbus','Sukhoi') AND
    Airplanes.SeatsCount BETWEEN 200 AND 300;

-- Successfully run. Total query runtime: 62 msec.
-- 25 rows affected.