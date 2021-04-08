using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standards
{
    public class CommandResult
    {
        public string User { get; set; }
        public object Contents { get; set; }
        public DateTime Created { get; set; }

        public CommandResult() => Created = DateTime.Now;
        
    }
}
