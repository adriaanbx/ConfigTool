using ConfigTool.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Wrappers
{
    public class TextWrapper : ModelWrapper<Text, int>
    {

        public TextWrapper(Text model) : base(model)
        {
        }

        //protected override IEnumerable<string> ValidateProperty(string propertyName)
        //{
        //    switch (propertyName)
        //    {
        //        case nameof(Text):
        //            if (string.Equals(Text, "Test", StringComparison.OrdinalIgnoreCase))
        //            {
        //                yield return "Test is not a valid Text";
        //            }
        //            break;
        //    }
    }
}
