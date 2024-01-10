USE BusRoutes;
DELETE FROM bus_stops WHERE bus_id = (SELECT MAX(bus_id) FROM bus_stops);
DELETE FROM buses WHERE bus_id = (SELECT MAX(bus_id) FROM buses);
DELETE FROM stops WHERE stop_id = (SELECT MAX(stop_id) FROM stops);