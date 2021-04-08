namespace Standards
{
    /// <summary>
    /// Main object for passing around files
    /// </summary>
    public class FileStandard
    {
        public string Name { get; private set; }
        public byte[] File { get; private set; }

        public FileStandard(string Name, byte[] File)
        {
            this.Name = Name;
            this.File = File;
        }

        #region Encryption
        public void Decrypt()
        {

        }
        public void Encrypt()
        {

        }
        #endregion Encryption
    }
}
