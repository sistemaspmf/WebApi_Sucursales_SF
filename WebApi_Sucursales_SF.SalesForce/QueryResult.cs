using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi_Sucursales_SF.SalesForce
{
    public class QueryResult<T>
    {
        public int totalSize { get; set; }
        public bool done { get; set; }
        public string nextRecordsUrl { get; set; }
        public T records { get; set; }
    }
}
