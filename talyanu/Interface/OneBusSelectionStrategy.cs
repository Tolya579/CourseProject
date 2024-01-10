using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talyanu.Model;

namespace talyanu.Interface
{
    public class OneBusSelectionStrategy : IRouteSelectionStrategy
    {
        public List<string> SelectRoute(string startPoint, string endPoint)
        {
            return DataWorker.OneBusSelection(startPoint, endPoint);
        }
    }
}
