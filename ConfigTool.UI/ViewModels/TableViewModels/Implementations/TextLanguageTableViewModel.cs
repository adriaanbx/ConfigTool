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
    public class TextLanguageTableViewModel : TableViewModelBase<TextLanguage, int, TextLanguageWrapper>, ITextLanguageTableViewModel
    {
        private readonly ITextRepository _textRepository;
        private readonly ILanguageRepository _languageRepository;


        public RangeObservableCollection<LookupItem<int>> Texts { get; }
        public RangeObservableCollection<LookupItem<int>> Languages { get; }

        public TextLanguageTableViewModel(ITextLanguageRepository textLanguageRepository, ITextRepository textRepository,
                                   ILanguageRepository languageRepository, IEventAggregator eventAggregator) : base(textLanguageRepository, eventAggregator)
        {
            _textRepository = textRepository;
            _languageRepository = languageRepository;

            Texts = new RangeObservableCollection<LookupItem<int>>();
            Languages = new RangeObservableCollection<LookupItem<int>>();
        }


        protected override bool FilterTags(object obj)
        {
            var boolResult = false;

            if (obj is TextLanguageTableItem tableItem)
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
                return tableItem.Table.Desc.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       tableItem.Text.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase) ||
                       tableItem.Language.Contains(TableFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        public override async Task LoadAsync(EventParameters? eventParameters)
        {
            await base.LoadAsync(eventParameters);

            //Load Texts
            var lookup2 = await _textRepository.GetAllLookupAsync();
            Texts.Clear();
            Texts.AddRange(lookup2);

            //Load Languages
            var lookup3 = await _languageRepository.GetAllLookupAsync();
            Languages.Clear();
            Languages.AddRange(lookup3);

        }
        protected override void GetTextId(string columnName)
        {
            //var TextId = SelectedCell.Table.TextId;
            var TextId = SelectedCell.Table.GetType().GetProperty(columnName + "Id").GetValue(SelectedCell.Table);

            //Publish event to subscribers
            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                .Publish(new EventParameters() { Id = Convert.ToInt32(TextId), TableName = columnName });
        }


    }
}
