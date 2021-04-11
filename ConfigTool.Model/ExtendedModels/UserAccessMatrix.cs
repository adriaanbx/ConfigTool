using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class UserAccessMatrix
    {

        public bool Visible { get; set; }
        public bool Enabled { get; set; }

        public bool ConfirmationRequired { get; set; }

        public Group Group { get; set; }

        public Action Action { get; set; }
    }
}
