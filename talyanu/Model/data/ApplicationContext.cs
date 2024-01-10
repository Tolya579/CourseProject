using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System;


namespace talyanu.Model.data
{
    public class ApplicationContext: DbContext
    {
        public List <Bus> buses { get; set; }
        public List <Station> stations { get; set; }

        public SqlConnection connection;

        public ApplicationContext()
        {
            connection = new SqlConnection(@"Data Source=DESKTOP-OCFJL3U\SQLEXPRESS06;Initial Catalog=BusRoutes;Integrated Security=True");
            connection.Open();
        }

        public List<Station> GetStops()
        {
            stations = new List<Station>();

            using (connection)
            {
                

                SqlCommand command = new SqlCommand("SELECT stop_id, stop_name FROM stops", connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Station stop = new Station();
                    stop.id_Station = int.Parse(reader["stop_id"].ToString());
                    stop.Name = reader["stop_name"].ToString();
                    stations.Add(stop);
                }

                reader.Close();
            }

            return stations;
        }
        public string FindOneBus(string start, string end)
        {
            using (connection)
            {
                // Получаем список автобусов, останавливающихся на найденных остановках
                string queryBuses = @"
            SELECT TOP 1 bus_name
            FROM (
                SELECT bus_name, COUNT(*) AS num_stops
                FROM buses
                JOIN bus_stops ON buses.bus_id = bus_stops.bus_id
                JOIN stops ON bus_stops.stop_id = stops.stop_id
                WHERE stops.stop_name IN (@start, @end)
                GROUP BY bus_name
            ) AS stops_on_buses
            WHERE num_stops = 2";
                using (SqlCommand commandBuses = new SqlCommand(queryBuses, connection))
                {
                    commandBuses.Parameters.AddWithValue("@start", start);
                    commandBuses.Parameters.AddWithValue("@end", end);

                    using (SqlDataReader readerBuses = commandBuses.ExecuteReader())
                    {
                        if (readerBuses.Read())
                        {
                            return readerBuses.GetString(0);
                        }
                        readerBuses.Close();
                    }
                }
            }
            return null;
        }

        public List<string> FindBuses(string start, string end)
        {
            List <string> Buses = new List<String>();
            using (connection)
            {
                

                // Получаем список автобусов, останавливающихся на найденных остановках
                string queryBuses = @"
            SELECT bus_name
            FROM (
                SELECT bus_name, COUNT(*) AS num_stops
                FROM buses
                JOIN bus_stops ON buses.bus_id = bus_stops.bus_id
                JOIN stops ON bus_stops.stop_id = stops.stop_id
                WHERE stops.stop_name IN (@start, @end)
                GROUP BY bus_name
            ) AS stops_on_buses
            WHERE num_stops = 2";
                using (SqlCommand commandBuses = new SqlCommand(queryBuses, connection))
                {
                    commandBuses.Parameters.AddWithValue("@start", start);
                    commandBuses.Parameters.AddWithValue("@end", end);

                    using (SqlDataReader readerBuses = commandBuses.ExecuteReader())
                    {
                        while (readerBuses.Read())
                        {
                            string busName = readerBuses.GetString(0);
                            Buses.Add(busName);
                        }
                        readerBuses.Close();
                    }
                }
            }
            return Buses;
        }

        public bool AddBD(string stops, string Num)
        {
            string NumBus = "Автобус " + Num;
            bool check = true;
            using (connection)
            {
                try
                {
                   

                    int newBusId = GetNewBusId(NumBus);

                    if (newBusId == -1)
                    {
                        check = false;
                    }
                    else
                    {
                        string[] stopNames = stops.Split(',');

                        foreach (string stopName in stopNames)
                        {
                            int stopId = GetStopId(stopName.Trim());

                            if (stopId == -1)
                            {
                                check = false;
                                break;
                            }
                            else
                            {
                                InsertBusStop(newBusId, stopId);
                            }
                        }
                    }
                }
                catch
                {
                    check = false;
                }
            }

            return check;
        }
        public int GetNewBusId(string numBus)
        {
            int busExists = CheckBusExistence(numBus);

            if (busExists == 0)
            {
                return InsertNewBus(numBus);
            }
            else if (busExists == 1)
            {
                return GetExistingBusId(numBus);
            }
            else
            {
                return -1;
            }
        }

        public int CheckBusExistence(string numBus)
        {
            SqlCommand checkBusCmd = new SqlCommand("SELECT COUNT(*) FROM buses WHERE bus_name = @busName", connection);
            checkBusCmd.Parameters.AddWithValue("busName", numBus);
            return (int)checkBusCmd.ExecuteScalar();
        }

        public int InsertNewBus(string numBus)
        {
            SqlCommand insertBusCmd = new SqlCommand("INSERT INTO buses (bus_name) VALUES (@busName); SELECT CAST(scope_identity() AS int)", connection);
            insertBusCmd.Parameters.AddWithValue("busName", numBus);
            return (int)insertBusCmd.ExecuteScalar();
        }

        public int GetExistingBusId(string numBus)
        {
            SqlCommand getBusIdCmd = new SqlCommand("SELECT bus_id FROM buses WHERE bus_name = @busName", connection);
            getBusIdCmd.Parameters.AddWithValue("busName", numBus);
            return (int)getBusIdCmd.ExecuteScalar();
        }

        public int GetStopId(string stopName)
        {
            int stopExists = CheckStopExistence(stopName);

            if (stopExists == 0)
            {
                return InsertNewStop(stopName);
            }
            else if (stopExists == 1)
            {
                return GetExistingStopId(stopName);
            }
            else
            {
                return -1;
            }
        }

        public int CheckStopExistence(string stopName)
        {
            SqlCommand checkStopCmd = new SqlCommand("SELECT COUNT(*) FROM stops WHERE stop_name = @stopName", connection);
            checkStopCmd.Parameters.AddWithValue("stopName", stopName.Trim());
            return (int)checkStopCmd.ExecuteScalar();
        }

        public int InsertNewStop(string stopName)
        {
            SqlCommand insertStopCmd = new SqlCommand("INSERT INTO stops (stop_name) OUTPUT INSERTED.stop_id VALUES (@stopName)", connection);
            insertStopCmd.Parameters.AddWithValue("stopName", stopName.Trim());
            return (int)insertStopCmd.ExecuteScalar();
        }

        public int GetExistingStopId(string stopName)
        {
            SqlCommand getStopIdCmd = new SqlCommand("SELECT stop_id FROM stops WHERE stop_name = @stopName", connection);
            getStopIdCmd.Parameters.AddWithValue("stopName", stopName.Trim());
            return (int)getStopIdCmd.ExecuteScalar();
        }

        public void InsertBusStop(int newBusId, int stopId)
        {
            SqlCommand insertBusStopCmd = new SqlCommand("INSERT INTO bus_stops (bus_id, stop_id) VALUES (@busId, @stopId)", connection);
            insertBusStopCmd.Parameters.AddWithValue("busId", newBusId);
            insertBusStopCmd.Parameters.AddWithValue("stopId", stopId);
            insertBusStopCmd.ExecuteNonQuery();
        }

    }
}
