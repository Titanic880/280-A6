using System;

namespace Standards
{
    /// <summary>
    /// Main object for passing around files
    /// </summary>
    [Serializable()]
    public class FileStandard
    {
        public string Name { get; private set; }
        public byte[] File { get; private set; }
        public string Sender { get; set; }
        public FileStandard(string Name, byte[] File)
        {
            this.Name = Name;
            this.File = File;
        }
    }
}
