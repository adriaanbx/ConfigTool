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
    public class PressParameterTableViewModel : TableViewModelBase<PressParameter, int, PressParameterWrapper>, IPressParameterTableViewModel
    {
        private readonly IPlctagRepository _plctagRepository;
        private readonly ILayerSideRepository _layerSideRepository;
        private readonly IPressParameterTypeRepository _pressParameterTypeRepository;


        public RangeObservableCollection<LookupItem<int>> Plctags { get; }
        public RangeObservableCollection<LookupItem<short>> LayerSides { get; }
        public RangeObservableCollection<LookupItem<int>> PressParameterTypes { get; }

        public PressParameterTableViewModel(IPressParameterRepository pressParameterRepository, IPlctagRepository plctagRepository,
                                   ILayerSideRepository layerSideRepository, IPressParameterTypeRepository pressParameterTypeRepository,
                                   IEventAggregator eventAggregator) : base(pressParameterRepository, eventAggregator)
        {
            _plctagRepository = plctagRepository;
            _layerSideRepository = layerSideRepository;
            _pressParameterTypeRepository = pressParameterTypeRepository;


            Plctags = new RangeObservableCollection<LookupItem<int>>();
            LayerSides = new RangeObservableCollection<LookupItem<short>>();
            PressParameterTypes = new RangeObservableCollection<LookupItem<int>>();
        }


        protected override bool FilterTags(object obj)
        {
            var boolResult = false;

            if (obj is PressParameterTableItem tableItem)
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
                return tableItem.LayerSide.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       tableItem.PressParameterType.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       (!string.IsNullOrEmpty(tableItem.Table.Name) && tableItem.Table.Name.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase)) ||
                       (!string.IsNullOrEmpty(tableItem.Plctag) && tableItem.Plctag.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase)) ||
                       tableItem.Table.Value.ToString().Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        public override async Task LoadAsync(EventParameters? eventParameters)
        {
            await base.LoadAsync(eventParameters);

            //Load Plctags
            var lookup2 = await _plctagRepository.GetAllPressParametersLookupAsync();
            Plctags.Clear();
            Plctags.AddRange(lookup2);

            //Load LayerSides
            var lookup3 = await _layerSideRepository.GetAllLookupAsync();
            LayerSides.Clear();
            LayerSides.AddRange(lookup3);

            //Load PressParameterTypes
            var lookup4 = await _pressParameterTypeRepository.GetAllLookupAsync();
            PressParameterTypes.Clear();
            PressParameterTypes.AddRange(lookup4);

        }

    }
}
