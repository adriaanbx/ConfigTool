using System.ComponentModel.DataAnnotations.Schema;

namespace ConfigTool.Models
{
    public partial class Ecmparameter
    { 
 

        public ValueType ValueType { get; set; }


        public ReadWriteType ReadWriteType { get; set; }

        [Column("TagId")]
        public Plctag Tag { get; set; }

        //   public Ecm ECM { get; set; }

        [Column("TextId")]
        public Text Text { get; set; }



        //[Column("GroupTextId")]
        public Text GroupText { get; set; }

    
        public new string ToString()
        {
            return $"{Id} {Tag.Name}";
        }


        //public string Name()
        //{
        //    if (Text != null) { 
        //       return Text.CurrentDisplayLanguage;
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}

        public string Type()
        {
            if (ValueType != null)
            {
                return ValueType.Name;
            }
            else
            {
                return "";
            }
        }

    }
}
