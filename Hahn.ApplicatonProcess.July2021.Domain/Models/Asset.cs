using System.Collections.Generic;

namespace Hahn.ApplicatonProcess.July2021.Domain.Models
{
    public class Asset
    {
        public Asset()
        {
            Users = new List<User>();
        }
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
