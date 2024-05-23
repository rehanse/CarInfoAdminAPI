using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInfo.DataAccess.Domain
{
    public class Manufacturer
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }//unique
        public string contactPerson { get; set; }//unique
        [Required]
        public string registeredOffice { get; set; }
    }
}
