using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Services.Models
{
    public class GeneralResponse
    {
        public bool IsSuccess { get; set; }

        public dynamic Data { get; set; }
    }
}
