using ConfigTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTool.UI.Repositories
{
    class PlctagRepositoryFake : IPlcTagRepository
    {
        private List<Plctag> _plctags;

        public PlctagRepositoryFake()
        {
            _plctags = new List<Plctag>();
            _plctags.Add(new Plctag { Id = 1, Name = "Plctag1", Number = "DB3078.1" });
            _plctags.Add(new Plctag { Id = 2, Name = "Plctag2", Number = "DB3078.2" });
            _plctags.Add(new Plctag { Id = 3, Name = "Plctag3", Number = "DB3078.3" });

        }

        public Task<IEnumerable<Plctag>> GetAll()
        {
            return Task.FromResult(_plctags as IEnumerable<Plctag>);           
        }
    }
}
