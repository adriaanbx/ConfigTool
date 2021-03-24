using ConfigTool.Models;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Wrappers
{
    public class PlctagWrapper : NotifyErrorInfobase
    {
        public Plctag Model { get; }

        public int Id
        {
            get { return Model.Id; }
            set { Model.Id = value; }
        }

        public string Name
        {
            get { return Model.Name; }
            set
            {
                Model.Name = value;
                OnPropertyChanged();
                ValidateProperty(nameof(Name));
            }
        }

        private void ValidateProperty(string propertyName)
        {
            ClearErrors(propertyName);
            switch (propertyName)
            {
                case nameof(Name):
                    if (string.Equals(Name, "Robot", StringComparison.OrdinalIgnoreCase))
                    {
                        AddError(propertyName, "Robot is not a valid Plctag");
                    }
                    break;
            }
        }

        public string Number
        {
            get { return Model.Number; }
            set
            {
                Model.Number = value;
                OnPropertyChanged();
            }
        }

        public PlctagWrapper(Plctag model)
        {
            Model = model;
        }
    }
}
