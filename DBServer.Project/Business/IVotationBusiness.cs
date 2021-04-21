using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBServer.Project.Business
{
    public interface IVotationBusiness
    {
        public bool AvailableThisWeek();
    }
}
