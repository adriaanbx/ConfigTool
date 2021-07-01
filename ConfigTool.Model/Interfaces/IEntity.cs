using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.Models.Interfaces
{
   public interface IEntity<TId>
    {
        public TId Id { get; set; }        
    }
}
