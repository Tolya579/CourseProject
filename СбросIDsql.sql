-- ����� �������� �������� identity ��� stops �� ������������� ��������
DBCC CHECKIDENT ('stops', RESEED, 0);

-- ��������� ������������� �������� ��� stop_id �� ������� stops
DECLARE @maxStopId AS INT;
SELECT @maxStopId = MAX(stop_id) FROM stops;

-- ��������� ������ �������� identity ��� stops
DBCC CHECKIDENT ('stops', RESEED, @maxStopId);

-- ����� �������� �������� identity ��� buses �� ������������� ��������
DBCC CHECKIDENT ('buses', RESEED, 0);

-- ��������� ������������� �������� ��� bus_id �� ������� buses
DECLARE @maxBusId AS INT;
SELECT @maxBusId = MAX(bus_id) FROM buses;

-- ��������� ������ �������� identity ��� buses
DBCC CHECKIDENT ('buses', RESEED, @maxBusId);