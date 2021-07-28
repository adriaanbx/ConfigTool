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
    public class PlctagTableViewModel : TableViewModelBase<Plctag, int, PlctagWrapper>, IPlctagTableViewModel
    {
        private readonly IValueTypeRepository _valueTypeRepository;
        private readonly IDatablockRepository _datablockRepository;
        private readonly IUnitCategoryRepository _unitCategoryRepository;
        private readonly ITextLanguageRepository _textLanguageRepository;


        public RangeObservableCollection<LookupItem<short>> ValueTypes { get; }
        public RangeObservableCollection<LookupItem<int>> Datablocks { get; }
        public RangeObservableCollection<LookupItem<int>> UnitCategories { get; }
        public RangeObservableCollection<LookupItem<int>> TextLanguages { get; } 

        public PlctagTableViewModel(IPlctagRepository plctagRepository, IValueTypeRepository valueTypeRepository,
                                   IDatablockRepository datablockRepository, IUnitCategoryRepository unitCategoryRepository,
                                   ITextLanguageRepository textLanguageRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService) : base(plctagRepository, eventAggregator, messageDialogService)
        {
            _valueTypeRepository = valueTypeRepository;
            _datablockRepository = datablockRepository;
            _unitCategoryRepository = unitCategoryRepository;
            _textLanguageRepository = textLanguageRepository;


            ValueTypes = new RangeObservableCollection<LookupItem<short>>();
            Datablocks = new RangeObservableCollection<LookupItem<int>>();
            UnitCategories = new RangeObservableCollection<LookupItem<int>>();
            TextLanguages = new RangeObservableCollection<LookupItem<int>>();
        }


        protected override bool FilterTags(object obj)
        {
            var boolResult = false;

            if (obj is PlctagTableItem tableItemPlctag)
            {
                //Show all tags when filter is empty
                if (string.IsNullOrEmpty(TableFilter))
                {
                    return true;
                }

                //Check all columns with 'number' datatype
                if (Int32.TryParse(TableFilter, out int result))
                {
                    return tableItemPlctag.Table.Id.Equals(result) ||
                           //tableItemPlctag.Plctag.ArraySize.Equals(result) ||
                           tableItemPlctag.Table.CycleTime.Equals(result);
                }

                //Check all other columns
                return tableItemPlctag.Table.DefaultValue.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       tableItemPlctag.Table.Number.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       tableItemPlctag.DataBlock.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       tableItemPlctag.ValueType.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       tableItemPlctag.UnitCategory.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       (!string.IsNullOrEmpty(tableItemPlctag.Table.Name) && tableItemPlctag.Table.Name.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase)) ||
                       (!string.IsNullOrEmpty(tableItemPlctag.Text) && tableItemPlctag.Text.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase)) ||
                       tableItemPlctag.Table.Log.ToString().Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase);
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

            //Load Datablocks
            var lookup3 = await _datablockRepository.GetAllLookupAsync();
            Datablocks.Clear();
            Datablocks.AddRange(lookup3);

            //Load Unitcategories
            var lookup4 = await _unitCategoryRepository.GetAllLookupAsync();
            UnitCategories.Clear();
            UnitCategories.AddRange(lookup4);

            //Load TextLanguages
            var lookup5 = await _textLanguageRepository.GetAllLookupAsync();
            TextLanguages.Clear();
            TextLanguages.AddRange(lookup5);
        }

        protected override void GetTextId(string columnName)
        {
            var TextId = SelectedCell.Table.TextId;

            //Publish event to subscribers
            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                .Publish(new EventParameters() { Id = Convert.ToInt32(TextId), TableName = columnName });
        }
    }
}
