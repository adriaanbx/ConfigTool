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
    public class DatablockTableViewModel : TableViewModelBase<DataBlock, int, DatablockWrapper>, IDatablockTableViewModel
    {
        public DatablockTableViewModel(IDatablockRepository datablockRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService) : base(datablockRepository, eventAggregator, messageDialogService)
        {

        }


        protected override bool FilterTags(object obj)
        {
            var boolResult = false;

            if (obj is DatablockTableItem tableItem)
            {
                //Show all tags when filter is empty
                if (string.IsNullOrEmpty(TableFilter))
                {
                    return true;
                }

                //Check all columns with 'number' datatype
                if (Int32.TryParse(TableFilter, out int result))
                {
                    return tableItem.Table.Id.Equals(result);
                }

                //Check all other columns
                return tableItem.Table.Number.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       (!string.IsNullOrEmpty(tableItem.Table.Name) && tableItem.Table.Name.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase)) ||
                       (!string.IsNullOrEmpty(tableItem.Table.Number) && tableItem.Table.Number.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase));
            }
            return false;
        }

        public override async Task LoadAsync(EventParameters? eventParameters)
        {
            await base.LoadAsync(eventParameters);

        }

    }
}
