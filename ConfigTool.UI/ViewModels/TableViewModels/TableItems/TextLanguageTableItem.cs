using ConfigTool.Models;
using ConfigTool.UI.Wrappers;

namespace ConfigTool.UI.ViewModels.TableViewModels
{
    public class TextLanguageTableItem : TableItem<TextLanguage, int, TextLanguageWrapper>
    {
        public string? Text { get; set; }
        public string Language { get; set; }
    }
}
