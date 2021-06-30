using ConfigTool.UI.Events;
using ConfigTool.UI.ViewModels;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConfigTool.UI.ViewModels
{
    public class TableItemViewModel : ViewModelBase
    {
        private string _displayMember;
        private readonly IEventAggregator _eventAggregator;

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

        public TableItemViewModel(int id, string displayMember, IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            _eventAggregator = eventAggregator;
            OpenPlctagDetailViewCommand = new DelegateCommand(OnOpenPlctagDetailView);
        }

        public ICommand OpenPlctagDetailViewCommand { get; }


        private void OnOpenPlctagDetailView()
        {
            //_eventAggregator.GetEvent<OpenPlctagDetailViewEvent>()
            //    .Publish(Id);
        }

    }
}
