UPDATE Airplanes SET 
    CruiseSpeedKmh = CruiseSpeedKmh/10
    WHERE 
        Airplanes.Manufacturer = 'Boeing';