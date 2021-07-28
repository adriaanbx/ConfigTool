namespace ConfigTool.UI.Views.Services
{
    public interface IMessageDialogService
    {
        public MessageDialogResult ShowOkCancelDialog(string text, string title);
        public void ShowErrorDialog(string text, string title);
    }
}