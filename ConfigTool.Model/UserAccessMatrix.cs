using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class UserAccessMatrix
    {
        public int Id { get; set; }
        public int ActionId { get; set; }
        public int GroupId { get; set; }
        public int? FirstSignatureGroupId { get; set; }

    }
}
