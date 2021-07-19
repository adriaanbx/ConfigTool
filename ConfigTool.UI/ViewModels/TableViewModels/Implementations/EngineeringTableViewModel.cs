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
    public class EngineeringTableViewModel : TableViewModelBase<Engineering, int, EngineeringWrapper>, IEngineeringTableViewModel
    {
        private readonly IValueTypeRepository _valueTypeRepository;
        private readonly IPlctagRepository _plctagRepository;
        private readonly IReadWriteTypeRepository _readWriteTypeRepository;
        private readonly ITextLanguageRepository _textLanguageRepository;
        private readonly ITextLanguageRepository _groupTextLanguageRepository;


        public RangeObservableCollection<LookupItem<short>> ValueTypes { get; }
        public RangeObservableCollection<LookupItem<int>> Plctags { get; }
        public RangeObservableCollection<LookupItem<short>> ReadWriteTypes { get; }
        public RangeObservableCollection<LookupItem<int>> TextLanguages { get; }
        public RangeObservableCollection<LookupItem<int>> GroupTextLanguages { get; }

        public EngineeringTableViewModel(IEngineeringRepository engineeringRepository, IPlctagRepository plctagRepository,
                                   IValueTypeRepository valueTypeRepository, IReadWriteTypeRepository readWriteTypeRepository,
                                   ITextLanguageRepository textLanguageRepository, ITextLanguageRepository groupTextLanguageRepository, IEventAggregator eventAggregator) : base(engineeringRepository, eventAggregator)
        {
            _valueTypeRepository = valueTypeRepository;
            _plctagRepository = plctagRepository;
            _readWriteTypeRepository = readWriteTypeRepository;
            _textLanguageRepository = textLanguageRepository;
            _groupTextLanguageRepository = groupTextLanguageRepository;


            ValueTypes = new RangeObservableCollection<LookupItem<short>>();
            Plctags = new RangeObservableCollection<LookupItem<int>>();
            ReadWriteTypes = new RangeObservableCollection<LookupItem<short>>();
            TextLanguages = new RangeObservableCollection<LookupItem<int>>();
            GroupTextLanguages = new RangeObservableCollection<LookupItem<int>>();
        }


        protected override bool FilterTags(object obj)
        {
            var boolResult = false;

            if (obj is EngineeringTableItem tableItem)
            {
                //Show all tags when filter is empty
                if (string.IsNullOrEmpty(TableFilter))
                {
                    return true;
                }

                //Check all columns with 'number' datatype
                if (Int32.TryParse(TableFilter, out int result))
                {
                    return tableItem.Table.Id.Equals(result) ||
                           tableItem.Table.SortPosition.Equals(result);
                }

                //Check all other columns
                return tableItem.Table.Enable.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       tableItem.Table.Value.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       tableItem.Table.Visible.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       tableItem.Plctag.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       tableItem.ValueType.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       tableItem.ReadWriteType.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       tableItem.Text.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       tableItem.GroupText.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       (!string.IsNullOrEmpty(tableItem.Table.Min) && tableItem.Table.Min.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase)) ||
                       (!string.IsNullOrEmpty(tableItem.Table.Max) && tableItem.Table.Max.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase));
            }
            return false;
        }

        public override async Task LoadAsync(EventParameters? eventParameters)
        {
            await base.LoadAsync(eventParameters);

            //Load ValueTypes
            var lookup2 = await _valueTypeRepository.GetAllLookupAsync();
            ValueTypes.Clear();
            ValueTypes.AddRange(lookup2);

            //Load Plctags
            var lookup3 = await _plctagRepository.GetAllParametersLookupAsync();
            Plctags.Clear();
            Plctags.AddRange(lookup3);

            //Load ReadWriteTypes
            var lookup4 = await _readWriteTypeRepository.GetAllLookupAsync();
            ReadWriteTypes.Clear();
            ReadWriteTypes.AddRange(lookup4);

            //Load TextLanguages
            var lookup5 = await _textLanguageRepository.GetAllLookupAsync();
            TextLanguages.Clear();
            TextLanguages.AddRange(lookup5);

            //Load GroupTextLanguages
            var lookup6 = await _groupTextLanguageRepository.GetAllLookupAsync();
            GroupTextLanguages.Clear();
            GroupTextLanguages.AddRange(lookup6);
        }

        protected override void GetTextId(string columnName)
        {
            var TextId = SelectedCell.Table.GetType().GetProperty(columnName + "Id").GetValue(SelectedCell.Table);
            //var TextId = SelectedCell.Table.TextId;

            //Publish event to subscribers
            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                .Publish(new EventParameters() { Id = Convert.ToInt32(TextId), TableName = columnName });
        }
    }
}
