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
                NavigationItemPlctag navigationItemPlctag = new NavigationItemPlctag();

                if (cellInfo.Column != null && cellInfo.Item != null)
                {
                    //get selected cell info
                    navigationItemPlctag = cellInfo.Item as NavigationItemPlctag;

                    //get selected column name
                    navigationItemPlctag.ColumnName = cellInfo.Column.Header.ToString();
                }
                return navigationItemPlctag;
            }
            return null;
        }
    }
}
