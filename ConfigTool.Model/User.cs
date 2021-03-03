using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class User
    {
        public string Name { get; set; }
        public string Password { get; set; } = "";
        public string PreviousPassword1 { get; set; }
        public string PreviousPassword2 { get; set; }
        public string PreviousPassword3 { get; set; }
        public string PreviousPassword4 { get; set; }
        public string PreviousPassword5 { get; set; }
        public string Comments { get; set; }
        public int? NrOfMinsBeforeAutomaticLogOff { get; set; }
                    
        public int? NrOfDaysBeforePasswordExpired { get; set; }
        public long? PasswordCreationDate { get; set; }
        public int Id { get; set; }
        public int GroupId { get; set; }

    }
}
