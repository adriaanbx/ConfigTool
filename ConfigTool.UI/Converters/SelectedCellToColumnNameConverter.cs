using ConfigTool.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace ConfigTool.UI.Converters
{
    public class SelectedCellToColumnNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var cellInfo = (DataGridCellInfo)value;
                TableItemPlctag tableItemPlctag = new TableItemPlctag();

                if (cellInfo.Column != null && cellInfo.Item != null)
                {
                    //get selected cell info
                    tableItemPlctag = cellInfo.Item as TableItemPlctag;

                    //get selected column name
                    tableItemPlctag.ColumnName = cellInfo.Column.Header.ToString();
                }
                return tableItemPlctag;
            }
            return null;
        }
    }
}
