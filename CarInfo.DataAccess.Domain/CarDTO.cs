using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInfo.DataAccess.Domain
{
    public class CarDTO
    {
        public int Id { get; set; }
        public string carname { get; set; }
        public string carImage { get; set; }
        public string carModel { get; set; }
        public int manifactureId { get; set; }
        public int typeId { get; set; }
        public string engine { get; set; }//unique
        public int BHP { get; set; }
        public int carTransmissionId { get; set; }
        public int mileage { get; set; }
        public int seat { get; set; }
        public string airBagDetails { get; set; }
        public string bootspace { get; set; }
        public decimal price { get; set; }
        public CarType? CarType { get; set; }
        public CarReferenceViewModel? carReferenceView { get; set; }
        public CarTransmissionType? carTransmissionType { get; set; }
        public Manufacturer? manufacturer { get; set; }
    }
}
