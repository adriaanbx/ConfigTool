using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConfigTool.UI.Views.Services
{
    public class MessageDialogService : IMessageDialogService
    {
        public MessageDialogResult ShowOkCancelDialog(string text, string title)
        {
            var result = MessageBox.Show(text, title, MessageBoxButton.OKCancel);
            return result == MessageBoxResult.OK
              ? MessageDialogResult.OK
              : MessageDialogResult.Cancel;
        }

        public void ShowErrorDialog(string text, string title)
        {
            MessageBox.Show(text, title, MessageBoxButton.OK);
        }
    }
    public enum MessageDialogResult
    {
        OK,
        Cancel
    }
}
