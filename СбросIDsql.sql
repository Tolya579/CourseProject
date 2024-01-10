-- —брос текущего значени€ identity дл€ stops до максимального значени€
DBCC CHECKIDENT ('stops', RESEED, 0);

-- ѕолучение максимального значени€ дл€ stop_id из таблицы stops
DECLARE @maxStopId AS INT;
SELECT @maxStopId = MAX(stop_id) FROM stops;

-- ”становка нового значени€ identity дл€ stops
DBCC CHECKIDENT ('stops', RESEED, @maxStopId);

-- —брос текущего значени€ identity дл€ buses до максимального значени€
DBCC CHECKIDENT ('buses', RESEED, 0);

-- ѕолучение максимального значени€ дл€ bus_id из таблицы buses
DECLARE @maxBusId AS INT;
SELECT @maxBusId = MAX(bus_id) FROM buses;

-- ”становка нового значени€ identity дл€ buses
DBCC CHECKIDENT ('buses', RESEED, @maxBusId);