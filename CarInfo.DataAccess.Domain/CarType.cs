using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInfo.DataAccess.Domain
{
    public class CarType
    {
        [Key]
        public int id { get; set; }
        public string type { get; set; }
    }
}
