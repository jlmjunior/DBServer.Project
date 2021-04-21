using System;

namespace DBServer.Project.Models
{
    public class VoteModel
    {
        public int IdUser { get; set; }
        public int IdRestaurant { get; set; }
        public DateTime DateVote { get; set; } = DateTime.Now;
    }
}
