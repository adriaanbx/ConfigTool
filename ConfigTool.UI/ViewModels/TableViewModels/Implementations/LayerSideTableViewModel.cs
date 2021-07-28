using ConfigTool.Models;
using ConfigTool.UI.Events;
using ConfigTool.UI.Repositories;
using Prism.Events;
using System;
using System.Threading.Tasks;
using ConfigTool.UI.ViewModels.TableViewModels;
using ConfigTool.UI.Wrappers;
using ConfigTool.UI.Views.Services;

namespace ConfigTool.UI.ViewModels
{
    public class LayerSideTableViewModel : TableViewModelBase<LayerSide, short, LayerSideWrapper>, ILayerSideTableViewModel
    {
        public LayerSideTableViewModel(ILayerSideRepository layerSideRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService) : base(layerSideRepository, eventAggregator, messageDialogService)
        {

        }


        protected override bool FilterTags(object obj)
        {
            var boolResult = false;

            if (obj is LayerSideTableItem tableItem)
            {
                //Show all tags when filter is empty
                if (string.IsNullOrEmpty(TableFilter))
                {
                    return true;
                }

                //Check all columns with 'number' datatype
                if (Int16.TryParse(TableFilter, out short result))
                {
                    return tableItem.Table.Id.Equals(result);
                }

                //Check all other columns
                return tableItem.Table.Desc.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        public override async Task LoadAsync(EventParameters? eventParameters)
        {
            await base.LoadAsync(eventParameters);

        }

    }
}
