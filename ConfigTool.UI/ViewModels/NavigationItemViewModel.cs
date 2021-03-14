using ConfigTool.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModels
{
    public class NavigationItemViewModel : ViewModelBase
    {
        private string _displayMember;

        public int Id { get; }
        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                if (_displayMember != value)
                {
                    _displayMember = value;
                    OnPropertyChanged();
                }
            }
        }

        public NavigationItemViewModel(int id, string displayMember)
        {
            Id = id;
            DisplayMember = displayMember;
        }

    }
}
