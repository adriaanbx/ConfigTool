using ConfigTool.DataAccess;
using ConfigTool.Models;
using ConfigTool.UI.ViewModels;
using ConfigTool.UI.ViewModels.TableViewModels;
using ConfigTool.UI.Wrappers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    public class TextRepository : GenericRepository<Text, ModelContext, int, TextWrapper>, ITextRepository
    {

        public TextRepository(ModelContext modelContext) : base(modelContext)
        {
        }

        public async override Task<IEnumerable<LookupItem<int>>> GetAllLookupAsync()
        {
            return await _context.Text.OrderBy(t => t.Id).Select(t =>
                new LookupItem<int>
                {
                    Id = t.Id,
                    DisplayMember = t.Id.ToString()
                }).ToListAsync();
        }

        public async override Task<IEnumerable<TableItem<Text, int, TextWrapper>>> GetTableLookupAsync()
        {
            return await _context.Text.OrderBy(t => t.Id).Select(e =>
          new TextTableItem
          {
              Table = new Wrappers.TextWrapper(e),
          }).ToListAsync();
        }
    }
}
