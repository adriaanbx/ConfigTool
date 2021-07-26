using ConfigTool.Models;
using ConfigTool.UI.Wrappers;

namespace ConfigTool.UI.ViewModels.TableViewModels
{
    public class LanguageTableItem : TableItem<Language, int, LanguageWrapper>
    {
        public string? Text { get; set; }
    }
}
