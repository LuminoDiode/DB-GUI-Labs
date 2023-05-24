-- 4. 
-- Увеличить число мест самолетов «ТУ» на 5 человек. 
-- Пусть ТУ = Boening

UPDATE Airplanes SET 
    SeatsCount = SeatsCount + 5
    WHERE 
        Airplanes.Manufacturer = 'Boeing';

-- UPDATE 57
-- Query returned successfully in 31 msec.