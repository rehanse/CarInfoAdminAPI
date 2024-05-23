using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInfo.DataAccess.Domain
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string carname { get; set; }
        public string carImage { get; set; }
        public string model { get; set; }
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
    }
}
