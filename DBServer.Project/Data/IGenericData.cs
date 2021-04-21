using System.Collections.Generic;

namespace DBServer.Project.Data
{
    public interface IGenericData<T>
    {
        public List<T> GetAll();
        public T GetById(int id);
        public bool Exists(int id);
    }
}
