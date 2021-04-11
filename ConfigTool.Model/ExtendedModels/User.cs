namespace ConfigTool.Models
{
    public partial class User
    {
      // public Group Group { get; set; }
        public bool LockedOut { get; set; }
        public bool Disabled { get; set; }
        public bool AutoLogOff { get; set; }
        public bool DefaultUser { get; set; }
        public bool SuperUser { get; set; }
        public bool PasswordNeverExpires { get; set; }
        public bool MustChangePasswordAtNextLogon { get; set; }
        
    }
}
