using ConfigTool.Models.Interfaces;
using ConfigTool.UI.ViewModels;
using ConfigTool.UI.Wrappers;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace ConfigTool.UI.Converters
{
    public class SelectedCellToColumnNameConverterBase<TEntity, TId, TWrapper> : IValueConverter
        where TWrapper : ModelWrapper<TEntity, TId>
        where TEntity : IEntity<TId>
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && targetType != null)
            {
                var cellInfo = (DataGridCellInfo)value;
                TableItem<TEntity, TId, TWrapper> tableItem = new TableItem<TEntity, TId, TWrapper>();

                if (cellInfo.Column != null && cellInfo.Item != null)
                {
                    //get selected cell info
                    tableItem = cellInfo.Item as TableItem<TEntity, TId, TWrapper>;

                    //get selected column name
                    tableItem.ColumnName = cellInfo.Column.Header.ToString();
                }
                return tableItem;
            }
            return null;
        }
    }
}
