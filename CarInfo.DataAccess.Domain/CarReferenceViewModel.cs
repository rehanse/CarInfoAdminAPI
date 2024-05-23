using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInfo.DataAccess.Domain
{
    public class CarReferenceViewModel
    {
        public IQueryable<CarTransmissionType> cartransmisionTypes { get; set; }
        public IQueryable<Manufacturer> manufacturer { get; set; }
        public IQueryable<CarType> carType { get; set; }
    }
}
