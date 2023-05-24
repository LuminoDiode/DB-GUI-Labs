COPY Airplanes FROM 'C:\t\airplanes.csv' DELIMITER ';' CSV;
COPY Routes FROM 'C:\t\routes.csv' DELIMITER ';' CSV;
COPY Flights FROM 'C:\t\flights.csv' DELIMITER ';' CSV;

-- для лучшей демонстрации далее, время ISO 8601
INSERT INTO Flights VALUES
    ((select max(Flights.Id) from Flights)+1,'2007-09-05T02:27:17+04','2007-09-05T06:09:38+04', 10, 50,12),
    ((select max(Flights.Id) from Flights)+2,'2008-09-05T02:27:17+04','2008-09-05T06:09:38+04', 20, 50,12)