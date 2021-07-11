using ConfigTool.Models;
using ConfigTool.Models.Interfaces;
using ConfigTool.UI.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.ViewModels
{
    public class TableItem<TEntity, TId, TWrapper> : ViewModelBase
        where TWrapper : ModelWrapper<TEntity, TId>
        where TEntity : IEntity<TId>
    {
        public TWrapper Table { get; set; }

        public string ColumnName { get; set; }
    }
}
