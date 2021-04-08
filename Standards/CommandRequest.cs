using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standards
{
    public class CommandRequest
    {
        public object Client { get; set; }
        public string Message { get; set; }
        public DateTime RequestTime { get; set; }
        public CommandRequest()
            => RequestTime = DateTime.Now;
    }
}
