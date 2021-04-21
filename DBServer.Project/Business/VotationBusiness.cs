using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBServer.Project.Business
{
    public class VotationBusiness : IVotationBusiness
    {
        public bool AvailableThisWeek()
        {
            var startDate = DateTime.Today.AddDays((int)DayOfWeek.Sunday - (int)DateTime.Today.DayOfWeek);
            var endDate = startDate.AddDays(6);

            return false;
        }
    }
}
