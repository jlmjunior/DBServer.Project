using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBServer.Project.Data
{
    public interface IGenericData<T>
    {
        public List<T> GetAll();
        public T GetById(int id);
        public bool Exists(int id);
    }
}
