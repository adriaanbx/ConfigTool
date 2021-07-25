using ConfigTool.Models;
using ConfigTool.UI.Events;
using ConfigTool.UI.Repositories;
using Prism.Events;
using System;
using System.Threading.Tasks;
using ConfigTool.UI.ViewModels.TableViewModels;
using ConfigTool.UI.Wrappers;

namespace ConfigTool.UI.ViewModels
{
    public class PlcMappingTableViewModel : TableViewModelBase<Plcmapping, int, PlcMappingWrapper>, IPlcMappingTableViewModel
    {
        private readonly IPlctagRepository _plctagRepository;
        private readonly ILayerSideRepository _layerSideRepository;


        public RangeObservableCollection<LookupItem<int>> Plctags { get; }
        public RangeObservableCollection<LookupItem<short>> LayerSides { get; }

        public PlcMappingTableViewModel(IPlcMappingRepository plcMappingRepository, IPlctagRepository plctagRepository,
                                        ILayerSideRepository layerSideRepository, IEventAggregator eventAggregator) : base(plcMappingRepository, eventAggregator)
        {
            _plctagRepository = plctagRepository;
            _layerSideRepository = layerSideRepository;

            Plctags = new RangeObservableCollection<LookupItem<int>>();
            LayerSides = new RangeObservableCollection<LookupItem<short>>();
        }


        protected override bool FilterTags(object obj)
        {
            var boolResult = false;

            if (obj is PlcMappingTableItem tableItem)
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

                if (Double.TryParse(TableFilter, out double resultDouble))
                {
                    return tableItem.Table.Value.Equals(resultDouble);
                }

                //Check all other columns
                return tableItem.Table.Name.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       (!string.IsNullOrEmpty(tableItem.Plctag) && tableItem.Plctag.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase)) ||
                       (!string.IsNullOrEmpty(tableItem.LayerSide) && tableItem.LayerSide.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase)) ||
                       (!string.IsNullOrEmpty(tableItem.Table.ElDesingCode) && tableItem.Table.ElDesingCode.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase));

            }
            return false;
        }

        public override async Task LoadAsync(EventParameters? eventParameters)
        {
            await base.LoadAsync(eventParameters);

            //Load Plctags
            var lookup3 = await _plctagRepository.GetAllPlcMappingLookupAsync();
            Plctags.Clear();
            Plctags.AddRange(lookup3);

            //Load LayerSide
            var lookup6 = await _layerSideRepository.GetAllLookupAsync();
            LayerSides.Clear();
            LayerSides.AddRange(lookup6);
        }

    }
}
