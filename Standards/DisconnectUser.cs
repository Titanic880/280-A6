using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standards
{
    [Serializable()]
    //Object that is passed to the server to disconnect the user
    public class DisconnectUser
    { 
        public string User { get; set; }
        public DisconnectUser(string User)
        => this.User = User;
    }
}
