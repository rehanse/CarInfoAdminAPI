using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInfo.DataAccess.Domain.Identity
{
    public class ResponseStatus
    {
        public int statusCode { get; set; }
        public string? message { get; set; }
        public string? role { get; set; }
        public string? status {  get; set; }
        public string? token { get; set; }
    }
}
