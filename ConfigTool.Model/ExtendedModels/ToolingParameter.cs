using System.ComponentModel.DataAnnotations.Schema;

namespace ConfigTool.Models
{
    public partial class ToolingParameter
    {
        public ValueType ValueType { get; set; }
        public ReadWriteType ReadWriteType { get; set; }

        [Column("TagId")]
        public Plctag Tag { get; set; }


        [Column("TextId")]
        public Text Text { get; set; }

 

        public new string ToString()
        {
            return $"{Id} {Tag.Name}";
        }



    }
}
