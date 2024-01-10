using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talyanu.Model.data;

namespace talyanu.Model
{
    public static class DataWorker
    {
        public static string CreateNewBus (String Number)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.buses.Any(el => el.NumBus == Number);
                if (!checkIsExist)
                {
                    Bus newbuses = new Bus { NumBus = Number };
                    db.buses.Add(newbuses);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }
        public static List<Bus> GetAllBuses()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.buses.ToList();
                return result;
            }
        }
        public static List<Station> GetAllStations()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.GetStops();
                return result;
            }
        }

        public static List<String> OneBusSelection(string start, string end)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                string bus = db.FindOneBus(start, end);
                List <String> result = new List<string>();
                result.Add(bus);
                return result;
            }
        }
        public static List<String> BusSelection(string start, string end)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<string> result = db.FindBuses(start, end);
                return result;
            }
        }

        public static bool AddData(string stops, string NumBus)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.AddBD(stops, NumBus);
                return result;
            }
        }

    }
}
