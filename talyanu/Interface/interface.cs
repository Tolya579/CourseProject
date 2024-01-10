using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talyanu.Interface
{
    public interface IRouteSelectionStrategy
    {
        List<string> SelectRoute(string startPoint, string endPoint);
    }
}
