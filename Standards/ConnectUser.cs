using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standards
{
    [Serializable()]
    public class ConnectUser
    {
        public string Name { get; set; }
        public ConnectUser(string Name)
        {
            this.Name = Name;
        }
    }
}
